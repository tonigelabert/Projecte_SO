#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <ctype.h>
#include <mysql.h>

int main(int argc, char *argv[]) {
	int sock_conn, sock_listen, ret;
	struct sockaddr_in serv_adr;
	char buff[512]; //petici�
	char buff2[512];//resposta
	
	// INICIALITZACIONS
	// Obrim el socket
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0) //crear socket de'escolta
		printf("Error creant socket");
	// Fem el bind al port
	memset(&serv_adr, 0, sizeof(serv_adr));// inicialitza a zero serv_addr
	serv_adr.sin_family = AF_INET;
	// asocia el socket a cualquiera de las IP de la m?quina. 
	//htonl formatea el numero que recibe al formato necesario
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY); //indicamos a cualquier
	// escucharemos en el port 9050
	serv_adr.sin_port = htons(9080); //9070 en que puerto vamos a escuchar
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0) //asociamos al socket las esecificaciones anteriores
		printf ("Error al bind");
	//La cola de peticiones pendientes no podr? ser superior a 4
	if (listen(sock_listen, 2) < 0) //lo ponemos en pasivo. EL 2 es el numero de objetos en cola
		printf("Error en el Listen");
	
	MYSQL *conn;
	int err;
	// Estructura especial para almacenar resultados de consultas 
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	int e;
	
	char consulta [80];
	//Creamos una conexion al servidor MYSQL 
	conn = mysql_init(NULL);
	if (conn==NULL) {
		printf ("Error al crear la conexion: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	//inicializar la conexion
	conn = mysql_real_connect (conn, "localhost","root", "mysql", "BD_JOC",0, NULL, 0);
	if (conn==NULL) {
		printf ("Error al inicializar la conexión: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	for(;;){
		printf ("Escoltant\n");
		sock_conn = accept(sock_listen, NULL, NULL); //espera una conexion a traves del socket de escucha y cuando lo hace, crea un socket de conexion
		printf ("He rebut conexi�\n");
		//sock_conn es el socket que usaremos para este cliente
		
		//BUcle de atención al cliente
		int terminar = 0;
		while (terminar == 0)
		{// Rebem la petici� del client i la posam dins buff
			ret=read(sock_conn,buff, sizeof(buff)); //el entero es el numero de bytes leidos (tamaño mensaje)
			printf ("Recibido\n");
			
			// Tenemos que a?adirle la marca de fin de string 
			// para que no escriba lo que hay despues en el buffer
			buff[ret]='\0';
			
			//Escriem la petici� en pantalla--> codi/nom/contrasenya/paraulaSeguretat per exemple
			printf ("Se ha conectado: %s\n",buff);
			
			
			char *p = strtok( buff, "/"); //coge el buffer y corta por donde hay una barra
			int codigo =  atoi (p); //coge lo que hay entre el inicio y la barra y lo convierte a entero
			printf("codi= %d\n",codigo);
			
			//si el codi es 0 ens desconnectam
			if (codigo == 0)
				terminar = 1;
			//si el codi es 1 feim la consulta de la partida de menor duraci�
			else if (codigo ==1) {
				Consulta_DuracioMin(conn, resultado, row, err, buff2);
				write (sock_conn,buff2, strlen(buff2));
			}
			//si el codi es 2 feim la consulta de retornar la contrasenya amb la paraula de seguretat i el nom de l'usuari
			else if (codigo == 2){
				p = strtok(NULL, "/");
				char paraula[20];
				strcpy(paraula, p);
				p = strtok(NULL, "/");
				char nom[20];
				strcpy(nom, p);
				// quieren saber si el nombre es bonito
				Consulta_RetornarContrasenya(conn, resultado, row, err, buff2, nom, paraula);
				write (sock_conn,buff2, strlen(buff2));
			}
			//si el codi es 3 ens consulta el nom del judador amb m�s participacions
			else if (codigo == 3){
				Consulta_MaxParticipacions(conn, resultado, row, err, buff2);
				write (sock_conn,buff2, strlen(buff2));
			}
			//si el codi es 4 executarem la func que ens permet entrar i fer consultes
			else if (codigo == 4)
			{
				p = strtok(NULL, "/");
				char nom[20];
				strcpy(nom, p);
				p = strtok(NULL, "/");
				char contrasenya[20];
				strcpy(contrasenya, p);
				int  respuesta_int = LogIn(conn, resultado, row, err, nom, contrasenya);//ens retorna 1 si hem entrat correctament, 0 altrament
				if(respuesta_int == 1)
					sprintf(buff2, "1");
				else
					sprintf(buff2, "0");
				write (sock_conn,buff2, strlen(buff2));
			}
			//per registra-se el codi es 5
			else 
			{
				p = strtok(NULL, "/");
				char nom[20];
				strcpy(nom, p);
				p = strtok(NULL, "/");
				char contrasenya[20];
				strcpy(contrasenya, p);
				p = strtok(NULL, "/");
				char paraula[20];
				strcpy(paraula, p);
				
				Registrar(conn, resultado, row, err, buff2, nom, contrasenya, paraula);
				write (sock_conn,buff2, strlen(buff2));
			}
		}
		// Se acabo el servicio para este cliente
		close(sock_conn); 
	}
}

void Consulta_DuracioMin(MYSQL *conn, MYSQL_RES *resultado, MYSQL_ROW row, int err,char buff2[512])
{
	int ID;

	err = mysql_query (conn, "SELECT PARTIDA.ID FROM (PARTIDA)WHERE PARTIDA.DURACIO = (SELECT MIN(PARTIDA.DURACIO) FROM PARTIDA)"); //feim la consulta 
	printf("err = %d\n",err);
	if (err!=0) {
		
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	//recogemos el resultado de la consulta. El resultado de la
	//consulta se devuelve en una variable del tipo puntero a
	//MYSQL_RES tal y como hemos declarado anteriormente.
	//Se trata de una tabla virtual en memoria que es la copia
	//de la tabla real en disco.
	resultado = mysql_store_result (conn);
	// El resultado es una estructura matricial en memoria
	// en la que cada fila contiene los datos de una persona.
	// Ahora obtenemos la primera fila que se almacena en una
	// variable de tipo MYSQL_ROW
	row = mysql_fetch_row (resultado);
	// En una fila hay tantas columnas como datos tiene una
	// persona. En nuestro caso hay una columna: ID de la partida
	if (row == NULL)
		printf ("No se han obtenido datos en la consulta\n");
	else
	{
		buff2[0] = NULL; //inicialitzam el buff2 a null
		while (row != NULL) {
		
			sprintf (buff2,"%s%s,",buff2, row[0]); //copiam la resposta de la consulta dins el buff2
			// obtenemos la siguiente fila ya que dos o mas partidas 
			//pueden haber durado lo mismo
			row = mysql_fetch_row (resultado);
		}
	}
	

}
void Consulta_RetornarContrasenya(MYSQL *conn, MYSQL_RES *resultado, MYSQL_ROW row, int err, char buff2[512], char nom[20], char paraulaSeguretat[20])
{
	printf("Hem entrat dins la consulta de la contrasenya\n");
	char consulta [200];
	//cream la consulta concatenant la paraula de seguretat i el nom amb la sent�ncia mysql
	strcpy(consulta, "SELECT JUGADOR.CONTRASENYA FROM JUGADOR WHERE JUGADOR.PARAULA_SEGURETAT = '");
	strcat(consulta, paraulaSeguretat);
	strcat(consulta, "' AND JUGADOR.NOM = '");
	strcat(consulta, nom);
	strcat(consulta, "'");
	printf("La consulta a fer es: %s\n",consulta);
	err=mysql_query (conn, consulta);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	//Nom�s hi ha un resultat possible per la consulta
	buff2[0] = NULL;
	sprintf(buff2,"%s", row[0]);

}

void Consulta_MaxParticipacions(MYSQL *conn, MYSQL_RES *resultado, MYSQL_ROW row, int err, char buff2[512])
{
	char consulta[200];
	strcpy(consulta,"SELECT JUGADOR.NOM FROM (JUGADOR, ENLLA�) WHERE ENLLA�.PARTICIPACIONS = (SELECT MAX(ENLLA�.PARTICIPACIONS) FROM ENLLA�) AND ENLLA�.ID_J=JUGADOR.ID");
	err = mysql_query (conn, consulta);//feim la consulta a la BD_JOC
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	if (row == NULL)
		printf ("No se han obtenido datos en la consulta\n");
	else
		buff2[0] = NULL;
		while (row !=NULL) {
			// la columna 0 cont� el nom amb m�s participacions
			sprintf (buff2,"%s%s,",buff2, row[0]);
			// obtenim la seg�ent fila ja que dos o m�s jugadors poden 
			
			row = mysql_fetch_row (resultado);
	}
	// cerrar la conexion con el servidor MYSQL 


}

int LogIn(MYSQL *conn, MYSQL_RES *resultado, MYSQL_ROW row, int err, char nom[20], char contrasenya[20])
{
	
	char consulta [80];
	strcpy(consulta, "SELECT JUGADOR.CONTRASENYA FROM JUGADOR WHERE JUGADOR.NOM = '");
	strcat(consulta, nom);
	strcat(consulta, "'");
	err = mysql_query (conn, consulta);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	
	if(strcmp(contrasenya, row[0]) == 0)
	{
		return 1; //torna 1 si hem trobat la contrasenya
		
	}
	else 
	   return 0;//no hem trobat la contrasenya
	


}
void Registrar(MYSQL *conn, MYSQL_RES *resultado, MYSQL_ROW row, int err, char buff2[20], char* nom[20], char contrasenya[20], char paraulaSeguretat[20])
{
	char consulta [80];
	
	if(CercarUsuari(conn, resultado, row, err, nom) == 0)//el nom de l'usuari no existeix, per tant el podem crear
	{
		printf("No ha trobat el nom de l'usuari\n");
		int ID = MaxID_Jugador(conn, resultado, row, err);
		
		char IDs[10];
		sprintf(IDs,"%d", ID);
		printf("ID = %s\n",IDs);
		strcpy(consulta, "INSERT INTO JUGADOR VALUES(");
		strcat(consulta,IDs);
		printf("%s\n", consulta);
		strcat(consulta, ", '");
		strcat(consulta, nom);
		strcat(consulta, "', '");
		strcat(consulta, contrasenya);
		strcat(consulta,"', '");
		strcat(consulta, paraulaSeguretat);
		strcat(consulta,"');");
		
		err=mysql_query (conn, consulta);
		if (err!=0) {
			printf ("Error al consultar datos de la base %u %s\n",
					mysql_errno(conn), mysql_error(conn));
			exit (1);
		}
		char r[20];
		//Enviam que l'usuari que vol registrar no estava pr�viament registrat
		strcpy(r,"NOExistent");	
		sprintf(buff2, "%s",r);
	}
	else//si CercarUsuari ens retorna 1 vol dir que el nom d'usuari que es vol
	   //registar ja es troba dins la base de dades
	{	
		char r[20];
		strcpy(r,"Existent");	
		sprintf(buff2, "%s",r);
	}


	
}
int CercarUsuari(MYSQL *conn, MYSQL_RES *resultado, MYSQL_ROW row, int err, char* nom[20])
{
	char consulta[80];
	strcpy(consulta, "SELECT JUGADOR.NOM FROM JUGADOR WHERE JUGADOR.NOM = '");//comprovam que l'usuari no existeix
	strcat(consulta, nom);
	strcat(consulta, "'");
	err=mysql_query (conn, consulta);
	if (err!=0) 
	{
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	
	if (row == NULL){
		return 0;//retorna 0 si el nom no existeix a la BBDD
		
	}
	else 
		return 1;//retorna 1 si l'usuari ja existeix


}
int MaxID_Jugador(MYSQL *conn, MYSQL_RES *resultado, MYSQL_ROW row, int err)//retorna la ID mï¿©s gran
{
	int ID;
	char consulta [80];
	
	strcpy(consulta, "SELECT MAX(JUGADOR.ID) FROM JUGADOR");//ID s'assigna automaticament, cerca el ID mï¿©s gran i li suma 1
	err=mysql_query (conn, consulta);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	if (row == NULL)
		ID = 1;
	else
		ID = atoi(row[0]) + 1;
	return ID;

}
