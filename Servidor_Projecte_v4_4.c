#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <ctype.h>
#include <mysql.h>
#include <pthread.h>
int port = 50072;//OPCIONS: 50072, 50073 o 50074
//DEFINICIO LLISTA CONNECTATS
typedef struct {
	char nombre[20];
	int  socket;
}Conectado;

typedef struct {
	Conectado conectados[100];
	int num;
}ListaConectados;

//DEFINICIÓ TAULA
typedef struct{
	char nombre1[20];
	int socket1;
	char nombre2[20];
	int socket2;
	char nombre3[20];
	int socket3;
	char nombre4[20];
	char socket4;
}Partida;
Partida misPartidas[100];

pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;

ListaConectados miLista;
int sockets[100];
int i = 0;
//FUNCIONS DE LA TAULA DE PARTIDES

int PartidaLliure(Partida *partidas)
{
	//Retorna i, que s la partida, o -1 si no hi ha cap lliure
	int i=0;
	int found=0;
	while (i<100 && !found)
	{
		if(strcmp(partidas[i].nombre1,"")==0){
			found=1;
			printf("Partida lliure trobada:%d\n",i);
		}
		i=i+1;
	}
	if (found)
		return i-1;
	else
		return 0;
}
int PonJugador (Partida *partidas, char nombre[20], int socket, int partida){
	//Retorna -1 si no hi ha partides, 0 si no hi ha lloc a la partida i 1 si s'ha afegit
	if(partida>100)
		return -1;
	else{
		int i=0;
		int found=0;
		if (strcmp(partidas[partida].nombre1,"")==0){
			strcpy(partidas[partida].nombre1,nombre);
			partidas[partida].socket1 = socket;
			return 1;
		}
		else if ((strcmp(partidas[partida].nombre2,"")==0)){
			strcpy(partidas[partida].nombre2,nombre);
			partidas[partida].socket2 = socket;
			return 1;
		}
		else if (strcmp(partidas[partida].nombre3,"")==0){
			strcpy(partidas[partida].nombre3,nombre);
			partidas[partida].socket3 = socket;
			return 1;
		}
		else if (strcmp(partidas[partida].nombre4,"")==0){
			strcpy(partidas[partida].nombre4,nombre);
			partidas[partida].socket4 = socket;
			return 1;
		}
		else
				 return 0;
	}
}
int EliminarPartida(Partida *partidas, int partida){
	//retorna -1 si no existeix la partida i 0 si s'ha eliminat correctament
	printf("Entrar dins eliminar partida\n");
	if(partida>100)
		return -1;
	else
	{
		
		strcpy(partidas[partida].nombre1,"");
		partidas[partida].socket1 = NULL;
		strcpy(partidas[partida].nombre2,"");
		partidas[partida].socket2 = NULL;
		strcpy(partidas[partida].nombre3,"");
		partidas[partida].socket3 = NULL;
		strcpy(partidas[partida].nombre4,"");
		partidas[partida].socket4 = NULL;
		printf("Partida eliminada\n");
		return 0;
	}
}
void DameJugadoresTabla(Partida *partidas, char jugadors[300],int partida){
	
	sprintf(jugadors,"%s/%s/%s/%s",partidas[partida].nombre1,partidas[partida].nombre2,partidas[partida].nombre3,partidas[partida].nombre4);
}

int DamePartida(Partida *partidas, int socket){
	int i=0;
	int found = 0;
	while (i<100){
		if(partidas[i].socket1==socket)
			found=1;
			return i;
		if(partidas[i].socket2==socket)
			found=1;
			return i;
		if(partidas[i].socket3==socket)
			found=1;
			return i;
		if(partidas[i].socket4==socket)
			found=1;
			return i;
	}
}
/*void crear_Partida(ListaConectados *lista, int sock_conn, char nom_anfitrio[20])*/
/*{*/
/*	int k = 0;*/
/*	int found  = 0;*/
/*	nom_anfitrio;*/
/*	while(k<miLista.num && found == 0)*/
/*	{*/
/*		if(miLista.conectados[k].socket == sock_conn)*/
/*		{*/
/*			strcpy(nom_anfitrio, miLista.conectados[k].nombre);*/
/*			found = 1;*/
/*		}*/
/*		k++;*/
/*	}*/
/*	Inicialitzar_Partida(miTabla, nom_anfitrio, sock_conn);*/
	
	
/*}*/
//FUNCIONS DE LA LLISTA DE CONNECTATS
int Pon  (ListaConectados *lista, char nombre[20], int socket){
	//Añade nuevo conectado. Retorna -1 si la lista está llena y 0 si se puede añadir el usuario a la lista de conectados
	if(lista->num == 100)
		return -1;
	else{
		strcpy(lista->conectados[lista->num].nombre,nombre);
		lista->conectados[lista->num].socket = socket;
		lista->num = lista->num + 1;
		printf("dins la llista hi ha %d usuaris\n",lista->num);
		return 0;
	}
}
int DameSocket(ListaConectados *lista, char nombre[20]){
	//Devuelve el socket, o -1 si no está en la lista
	int i=0;
	int found = 0;
	while((i< lista->num) && (!found)){
		if (strcmp(lista->conectados[i].nombre,nombre)==0)
			found = 1;
		if (!found)
			i=i+1;
	}
	if(found)
		return lista->conectados[i].socket;
	else
		return -1;
}
int DameNombre(ListaConectados *lista, int socket,char nombre[20]){
	//Devuelve 1, o -1 si no está en la lista
	int i=0;
	int found = 0;
	while((i< lista->num) && (!found)){
		if (lista->conectados[i].socket==socket)
			found = 1;
		if (!found)
			i=i+1;
	}
	if(found)
	{
		strcpy(nombre,lista->conectados[i].nombre);
		return 1;
	}
	else
		return -1;
}
int DamePosicion(ListaConectados *lista, char nombre[20]){
	//Devuelve la posicion en la lista, o -1 si no está en la lista
	int i=0;
	int found = 0;
	while((i< lista->num) && (!found)){
		if (strcmp(lista->conectados[i].nombre,nombre)==0)
			found = 1;
		if (!found)
			i=i+1;
	}
	if(found)
							  return i;
	else
		return -1;
}

int Elimina(ListaConectados *lista, char nombre[20]){
	//retorna 0 si elimina y -1 si el usuario no está en la lista
	int pos = DamePosicion(lista, nombre);
	if (pos == -1)
		return -1;
	else{
		for (int i=pos; i < lista->num-1; i++)
		{
			lista->conectados[i] = lista->conectados[i+1];
			/*			strcpy(lista->conectados[i].nombre,lista->conectados[i+1].nombre);*/
			/*			lista->conectados[i].socket,lista->conectados[i+1].socket;*/
		}
		lista->num--;
		return 0;
	}
}

void DameConectados (ListaConectados *lista, char conectados[300]){
	//Pone en conectados los nombres de todos los conectados separados por /. Primero pone el número de conectados
	// Por ejemplo 3/Juan/Maria/Pedro
	sprintf(conectados,"6/%d",lista->num);
	int i;
	for (i=0; i< lista->num; i++)
	{
		sprintf(conectados,"%s/%s", conectados, lista->conectados[i].nombre);
	}
}


void *AtenderCliente(void *socket)
{

	
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
	conn = mysql_real_connect (conn, "shiva2.upc.es","root", "mysql", "T8_BD_JOC",0, NULL, 0);
	if (conn==NULL) {
		printf ("Error al inicializar la conexiÃ³n: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	int sock_conn;
	int *s;
	s=(int *) socket;
	sock_conn=*s;
	
	char buff[512]; //petició
	char buff2[512];//resposta
	
	int ret;
	
	int terminar = 0;
	

	
	
	while (terminar == 0)
	{// Rebem la petició del client i la posam dins buff

		
		ret=read(sock_conn,buff, sizeof(buff)); //el entero es el numero de bytes leidos (tamaÃ±o mensaje)
		printf ("Recibido\n");
		
		// Tenemos que a?adirle la marca de fin de string 
		// para que no escriba lo que hay despues en el buffer
		buff[ret]='\0';
		
		//Escriem la petició en pantalla--> codi/nom/contrasenya/paraulaSeguretat per exemple
		printf ("Se ha conectado: %s\n",buff);
		
		
		char *p = strtok( buff, "/"); //coge el buffer y corta por donde hay una barra
		int codigo =  atoi (p); //coge lo que hay entre el inicio y la barra y lo convierte a entero
		
		//si el codi es 0 ens desconnectam
		if (codigo == 0)
		{
			p = strtok(NULL, "/");
			char nom[20];
			strcpy(nom, p);
			terminar = 1;
			pthread_mutex_lock(&mutex);
			Elimina(&miLista, nom);
			pthread_mutex_unlock(&mutex);
			printf("Desconnectat\n");
			char buff3[300];
			DameConectados(&miLista, buff3);
			sprintf(buff3, "0/%s", buff3);
			printf("%s\n",&miLista.conectados[0].nombre);
			write (sock_conn,buff3, strlen(buff3));
			printf("Misatge enviat al servei 0, desconnexió %s\n", buff3);
			char notificacio[20];
			DameConectados(&miLista, notificacio);
			printf("Missatge enviat al servei 6, notificacio %s\n", notificacio);
			int j = 0;
			for (j = 0; j < i; j++)
				write (miLista.conectados[j].socket,notificacio, strlen(notificacio));
			
		}
		//si el codi es 1 feim la consulta de la partida de menor duració
		else if (codigo ==1) {
			buff2[0]=NULL;
			char resp[512];
			Consulta_DuracioMin(conn, buff2);
			sprintf(resp,"1/%s",buff2);
			printf("Missatge enviat al servei 1, partida de menor duració %s\n", resp);
			write (sock_conn,resp, strlen(resp));
		}
		//si el codi es 2 feim la consulta de retornar la contrasenya amb la paraula de seguretat i el nom de l'usuari
		else if (codigo == 2){
			buff2[0] = NULL;
			printf("%s\n",buff2);
			p = strtok(NULL, "/");
			char paraula[20];
			strcpy(paraula, p);
			p = strtok(NULL, "/");
			char nom[20];
			strcpy(nom, p);
			// quieren saber si el nombre es bonito
			Consulta_RetornarContrasenya(conn, buff2, nom, paraula);
			printf("Missatge enviat al servei 2, retornar conntrasenya, %s\n", buff2);
			write (sock_conn,buff2, strlen(buff2));
		}
		//si el codi es 3 ens consulta el nom del judador amb més participacions
		else if (codigo == 3){
			buff2[0] = NULL;
			Consulta_MaxParticipacions(conn,buff2);
			char res [512];
			sprintf(res, "3/%s",buff2);
			printf("Missatge enviat al servei 3, partida amb més participacions, %s\n", res);
			write (sock_conn,res, strlen(res));
		}
		//si el codi es 4 executarem la func que ens permet entrar i fer consultes
		else if (codigo == 4)
		{
			buff2[0] = NULL;
			p = strtok(NULL, "/");
			char nom[20];
			strcpy(nom, p);
			p = strtok(NULL, "/");
			char contrasenya[20];
			strcpy(contrasenya, p);
			int  respuesta_int = LogIn(conn, nom, contrasenya);//ens retorna 1 si hem entrat correctament, 0 altrament
			if(respuesta_int == 1)
			{ 
				
				pthread_mutex_lock(&mutex);
				Pon(&miLista, nom, sock_conn);
				pthread_mutex_unlock(&mutex);
				sprintf(buff2, "4/1");
				
				char notificacio[20];
				DameConectados(&miLista, notificacio);
				printf("Missatge enviat al servei 4, login: %s\n", buff2);
				write (sock_conn,buff2, strlen(buff2));
				
				int j = 0;
				for (j = 0; j< i; j++){
					write(miLista.conectados[j].socket,notificacio, strlen(notificacio));
					printf("Missatge enviat al servei 6, notificacio: %s\n", notificacio);
				}	
				
			}
			else
			{
				buff2[0] = NULL;
				sprintf(buff2, "4/0");
				printf("Missatge enviat al servei 4, login: %s\n", buff2);
				write (sock_conn,buff2, strlen(buff2));
			}
		}
		//per registra-se el codi es 5
		else if (codigo == 5)
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
			
			Registrar(conn, buff2, nom, contrasenya, paraula);
			write (sock_conn,buff2, strlen(buff2));
			printf("Missatge Registrar cap a client: %s\n", buff2);
		}
		else if (codigo == 7)
		{	
			int partida=PartidaLliure(&misPartidas);
			if(partida==-1){
				sprintf(buff2,"7/No hi ha partides disponibles");
				printf("No hi ha partides disponibles:%d\n",&partida);
				write (sock_conn,buff2, strlen(buff2));
			}
			else{
				char nomanfitrio[20];
				int err=DameNombre(&miLista,sock_conn,nomanfitrio);
				int err2=PonJugador(&misPartidas,nomanfitrio,sock_conn,partida);
				printf("L'anfitrio es %s\n",nomanfitrio);
				if(err==-1){
					sprintf(buff2,"7/Usuari no existent");
					printf("Usuari no existent:%s\n",nomanfitrio);
					write (sock_conn,buff2, strlen(buff2));
				}
				else{
					int j=0;
					p=strtok(NULL,"/");
					while (p!=NULL){
						char nomconvidat[20];
						strcpy(nomconvidat,p);
						int socketconvidat = DameSocket(&miLista,nomconvidat);
						sprintf(buff2,"7/%s/%d",nomanfitrio,partida);
						printf("Convidat %s ha rebut la invitacio\n",nomconvidat);
						printf("La invitació és: %s\n",buff2);
						write(socketconvidat,buff2, strlen(buff2));
						p=strtok(NULL,"/");
					}

				}
			}

		}
		else if(codigo==8){
			p=strtok(NULL,"/");
			char nomanfitrio[20];
			strcpy(nomanfitrio,p);
			p=strtok(NULL,"/");
			int partida = atoi(p);
			p=strtok(NULL,"/");
			char respostainvitacio[20];
			strcpy(respostainvitacio,p);
			printf("respostainvitacio: %s",respostainvitacio);
			if(strcmp(respostainvitacio,"SI")==0){
				char nomconvidat[20];
				int err = DameNombre(&miLista,sock_conn,nomconvidat);
				int err2 = PonJugador(misPartidas,nomconvidat,sock_conn,partida);
				sprintf(buff2,"8/%s/SI",nomconvidat);
				int socketanfitrio=DameSocket(&miLista,nomanfitrio);
				write(socketanfitrio,buff2, strlen(buff2));
			}
			else{
				
				char nomconvidat[20];
				int err = DameNombre(&miLista,sock_conn,nomconvidat);
				sprintf(buff2,"8/%s/NO",nomconvidat);
				printf("entra al else. El buffer es: %s\n",buff2);
				int socketanfitrio=DameSocket(&miLista,nomanfitrio);
				write(socketanfitrio,buff2, strlen(buff2));
			}
		}
		else if(codigo==9){
			int partida=DamePartida(misPartidas,sock_conn);
			char jugadors[100];
			DameJugadoresTabla(misPartidas,jugadors,partida);
			p=strtok(jugadors,"/");
			
			while (p!=NULL){
				char nomconvidat[20];
				strcpy(nomconvidat,p);
				char jugadors2[100];
				DameJugadoresTabla(misPartidas,jugadors2,partida);
				sprintf(buff2,"9/%s",jugadors2);
				int socketconvidat=DameSocket(&miLista,nomconvidat);
				write(socketconvidat,buff2, strlen(buff2));
				p=strtok(NULL,"/");
			}
			
		}
		else if (codigo==10){
			printf("p = %s\n",p);
			p=strtok(NULL,"/");
			int partida=atoi(p);
			printf("partida %d\n",partida);
			int err=EliminarPartida(misPartidas,partida);
			if (err == -1)
				printf("La partida per eliminar no existeix");
			else
				printf("partida eliminada correctament");
			printf("Primer nom de la partida eliminada: %s",misPartidas[partida].nombre1);
		}
		else
		{
			printf("codi 11\n");
			p=strtok(NULL,"/");
			int x=atoi(p);
			p=strtok(NULL,"/");
			int y=atoi(p);
			p=strtok(NULL,"/");
			int partida=atoi(p);
			char jugadors[100];
			DameJugadoresTabla(misPartidas,jugadors,partida);
			p=strtok(jugadors,"/");
			
			while (p!=NULL){
				char nom[20];
				strcpy(nom,p);
				char nom_conn[100];
				int err=DameNombre(&miLista,sock_conn,nom_conn);
				if( strcmp(nom_conn,nom)!=0)
				{
					sprintf(buff2,"11/%d/%d/%s",x,y,nom_conn);
					int socket=DameSocket(&miLista,nom);
					printf("Enviem als convidats: %s\n",buff2);
					write(socket,buff2, strlen(buff2));
				}
				
				p=strtok(NULL,"/");
			}
		}
		

		
	}
	// Se acabo el servicio para este cliente
	close(sock_conn);
	printf("Sockets desconnectats");
	

}

int main(int argc, char *argv[]) {
	int sock_conn, sock_listen, ret;
	struct sockaddr_in serv_adr;
	
	miLista.num = 0;
	
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
	serv_adr.sin_port = htons(port); //9070 en que puerto vamos a escuchar
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0) //asociamos al socket las esecificaciones anteriores
		printf ("Error al bind");
	//La cola de peticiones pendientes no podr? ser superior a 4
	if (listen(sock_listen, 2) < 0) //lo ponemos en pasivo. EL 2 es el numero de objetos en cola
		printf("Error en el Listen");

	int sockets[100];
	pthread_t thread;
	

	for(;;)
	{
		printf ("Escoltant\n");
		sock_conn = accept(sock_listen, NULL, NULL); //espera una conexion a traves del socket de escucha y cuando lo hace, crea un socket de conexion
		printf ("He rebut conexió\n");
		//sock_conn es el socket que usaremos para este cliente
		
		//BUcle de atenciÃ³n al cliente
		sockets[i] = sock_conn;
		
		
		pthread_create(&thread, NULL, AtenderCliente, &sockets[i]);
		printf("He sortit del atender clientes\n");
		i = i + 1;
	}
}
		

void Consulta_DuracioMin(MYSQL *conn,char buff2[512])
{
	int ID;
	
	int err;
	// Estructura especial para almacenar resultados de consultas 
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	
	
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
		buff2[0] = NULL;//inicialitzam el buff2 a null
		while (row != NULL) {
			
			
			sprintf (buff2,"%s%s,",buff2, row[0]); //copiam la resposta de la consulta dins el buff2
			// obtenemos la siguiente fila ya que dos o mas partidas 
			
			//pueden haber durado lo mismo
			row = mysql_fetch_row (resultado);
		}
		printf("%s\n",buff2);
		
		
		
		/*sprintf(buff2, "1/%s", buff2);*/
	}
	

}
void Consulta_RetornarContrasenya(MYSQL *conn, char buff2[512], char nom[20], char paraulaSeguretat[20])
{
	int err;
	// Estructura especial para almacenar resultados de consultas 
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	printf("entra dins consulta\n");
	char consulta [200];
	//cream la consulta concatenant la paraula de seguretat i el nom amb la sentència mysql
	strcpy(consulta, "SELECT JUGADOR.CONTRASENYA FROM JUGADOR WHERE JUGADOR.PARAULA_SEGURETAT = '");
	strcat(consulta, paraulaSeguretat);
	strcat(consulta, "' AND JUGADOR.NOM = '");
	strcat(consulta, nom);
	strcat(consulta, "'");
	err=mysql_query (conn, consulta);
	
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	if (row == NULL)
	{
		printf("Paraula de seguretat incorrecta\n");
	}
	else{
		buff2[0] = NULL;
		while (row !=NULL) {
			sprintf(buff2,"2/%s", row[0]); 
			printf ("Això s e buffer2 dins del while: %s\n",buff2);
			row = mysql_fetch_row (resultado);
		}
		/*sprintf(buff2, "%s", buff2);*/
		printf("Això és e buffer2: %s\n",buff2);
	}
	

}

void Consulta_MaxParticipacions(MYSQL *conn, char buff2[512])
{
	int err;
	// Estructura especial para almacenar resultados de consultas 
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	
	char consulta[200];
	strcpy(consulta,"SELECT JUGADOR.NOM FROM (JUGADOR, ENLLAÇ) WHERE ENLLAÇ.PARTICIPACIONS = (SELECT MAX(ENLLAÇ.PARTICIPACIONS) FROM ENLLAÇ) AND ENLLAÇ.ID_J=JUGADOR.ID");
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
	else{
		buff2[0] = NULL;
		while (row !=NULL) {
			// la columna 0 conté el nom amb més participacions
			sprintf (buff2,"%s%s,",buff2, row[0]);
			// obtenim la següent fila ja que dos o més jugadors poden 
			
			row = mysql_fetch_row (resultado);
		}
		printf(buff2);
	}
	// cerrar la conexion con el servidor MYSQL 


}

int LogIn(MYSQL *conn, char nom[20], char contrasenya[20])
{
	int err;
	// Estructura especial para almacenar resultados de consultas 
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	
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
int Registrar(MYSQL *conn, char buff2[20], char* nom[20], char contrasenya[20], char paraulaSeguretat[20])
{
	int err;
	// Estructura especial para almacenar resultados de consultas 
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	
	char consulta [80];
	
	if(CercarUsuari(conn) == 0)//el nom de l'usuari no existeix, per tant el podem crear
	{
		printf("No ha trobat el nom de l'usuari\n");
		int ID = MaxID_Jugador(conn);
		
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
		//Enviam que l'usuari que vol registrar no estava prèviament registrat
		strcpy(r,"NOExistent");	
		sprintf(buff2, "5/%s",r);
	}
	else//si CercarUsuari ens retorna 1 vol dir que el nom d'usuari que es vol
	   //registar ja es troba dins la base de dades
	{	
		char r[20];
		strcpy(r,"Existent");	
		sprintf(buff2, "%s",r);
	}
}
int CercarUsuari(MYSQL *conn, char* nom[20])
{
	int err;
	// Estructura especial para almacenar resultados de consultas 
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	
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
int MaxID_Jugador(MYSQL *conn)//retorna la ID mÃ¯Â¿Â©s gran
{
	int err;
	// Estructura especial para almacenar resultados de consultas 
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	
	int ID;
	char consulta [80];
	
	strcpy(consulta, "SELECT MAX(JUGADOR.ID) FROM JUGADOR");//ID s'assigna automaticament, cerca el ID mÃ¯Â¿Â©s gran i li suma 1
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
