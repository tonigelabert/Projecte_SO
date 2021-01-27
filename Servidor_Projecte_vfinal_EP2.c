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
int port = 50074;
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
	int totalpistes;
	int ous;
}Partida;
Partida misPartidas[100];

pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;

ListaConectados miLista;//Definim la nostra llista de connectats
int sockets[100];

int num_pistes = 3;//assignem el número de pistes
int i = 0;
//FUNCIONS DE LA TAULA DE PARTIDES

int PartidaLliure(Partida *partidas)
{
	//FUnció que troba la primera partida lliure. Retorna i, que s la partida, o -1 si no hi ha cap lliure
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
	//FUnció que afegeix un jugador a la partida. Retorna -1 si no hi ha partides, 0 si no hi ha lloc a la partida i 1 si s'ha afegit
	int numjugadors;
	if(partida>100)
		return -1;
	else{
		int i=0;
		int found=0;
		if (strcmp(partidas[partida].nombre1,"")==0){
			strcpy(partidas[partida].nombre1,nombre);
			partidas[partida].socket1 = socket;
			numjugadors = numjugadors + 1;
			return 1;
		}
		else if ((strcmp(partidas[partida].nombre2,"")==0)){
			strcpy(partidas[partida].nombre2,nombre);
			partidas[partida].socket2 = socket;
			numjugadors = numjugadors + 1;
			return 1;
		}
		else if (strcmp(partidas[partida].nombre3,"")==0){
			strcpy(partidas[partida].nombre3,nombre);
			partidas[partida].socket3 = socket;
			numjugadors = numjugadors + 1;
			return 1;
		}
		else if (strcmp(partidas[partida].nombre4,"")==0){
			strcpy(partidas[partida].nombre4,nombre);
			partidas[partida].socket4 = socket;
			numjugadors = numjugadors + 1;
			return 1;
		}
		else
				 return 0;
	}
}
int DameNumJugadores(Partida *partidas, int partida){
	//Funció que retorna el num de jugadors, i -1 si la partida n existeix
	int numjugadors=0;
	if(partida>100)
		return -1;
	else{
		int i=0;
		int found=0;
		if (strcmp(partidas[partida].nombre1,"")!=0){
			numjugadors = numjugadors + 1;
		}
		if ((strcmp(partidas[partida].nombre2,"")!=0)){
			
			numjugadors = numjugadors + 1;
		}
		if (strcmp(partidas[partida].nombre3,"")!=0){
			
			numjugadors = numjugadors + 1;
		}
		if (strcmp(partidas[partida].nombre4,"")!=0){
			numjugadors = numjugadors + 1;
			
		}
		return numjugadors;
	}
}
void AugmentarPistes(Partida *partidas,int partida)
{
	//Funció que augmenta el número de pistes
	partidas[partida].totalpistes=partidas[partida].totalpistes+1;
}
void AugmentarOus(Partida *partidas,int partida)
{
	//Funció que augmenta el número d'ous
	partidas[partida].ous=partidas[partida].ous+1;
}
int EliminarPartida(Partida *partidas, int partida){
	//FUnció que elimina la partida. Retorna -1 si no existeix la partida i 0 si s'ha eliminat correctament
	printf("Entrar dins eliminar partida\n");
	if(partida>100)
		return -1;
	else
	{
		
		strcpy(partidas[partida].nombre1,"");
		memset(partidas[partida].nombre1,0,sizeof(partidas[partida].nombre1));
		partidas[partida].socket1 = NULL;
		strcpy(partidas[partida].nombre2,"");
		memset(partidas[partida].nombre2,0,sizeof(partidas[partida].nombre2));
		partidas[partida].socket2 = NULL;
		strcpy(partidas[partida].nombre3,"");
		memset(partidas[partida].nombre3,0,sizeof(partidas[partida].nombre3));
		partidas[partida].socket3 = NULL;
		strcpy(partidas[partida].nombre4,"");
		memset(partidas[partida].nombre4,0,sizeof(partidas[partida].nombre4));
		partidas[partida].socket4 = NULL;
		partidas[partida].totalpistes=0;
		partidas[partida].ous=0;
		printf("Partida eliminada\n");
		return 0;
	}
}
void DameJugadoresTabla(Partida *partidas, char jugadors[300],int partida){
	//FUnció que afegeix a jugadors la llista de jugadors d'una partida
	sprintf(jugadors,"%s/%s/%s/%s",partidas[partida].nombre1,partidas[partida].nombre2,partidas[partida].nombre3,partidas[partida].nombre4);
}

int DamePartida(Partida *partidas, int socket){
	//Funció que et retorna la partida d'un socket
	int i=0;
	int found = 0;
	while (i<100){
		if(partidas[i].socket1==socket){
			found=1;
			return i;
		}
		else if(partidas[i].socket2==socket){
			found=1;
			return i;
		}
		else if(partidas[i].socket3==socket){
			found=1;
			return i;
		}
		else if(partidas[i].socket4==socket){
			found=1;
			return i;
		}
		else
				i=i+1;
	}
}
void CopiaPartida(Partida *partidas, Partida *copia,int partida){
	//FUnció que fa una copia de la partida donada
	strcpy(copia[partida].nombre1,misPartidas[partida].nombre1);
	strcpy(copia[partida].nombre2,misPartidas[partida].nombre2);
	strcpy(copia[partida].nombre3,misPartidas[partida].nombre3);
	strcpy(copia[partida].nombre4,misPartidas[partida].nombre4);
	copia[partida].socket1=misPartidas[partida].socket1;
	copia[partida].socket2=misPartidas[partida].socket2;
	copia[partida].socket3=misPartidas[partida].socket3;
	copia[partida].socket4=misPartidas[partida].socket4;
	copia[partida].totalpistes=misPartidas[partida].totalpistes;
	copia[partida].ous=misPartidas[partida].ous;
}
void EliminarCopia(Partida *copia, int partida){
	//FUnció que elimina la copia donada
	copia[partida].socket1=NULL;
	copia[partida].socket2=NULL;
	copia[partida].socket3=NULL;
	copia[partida].socket4=NULL;
	copia[partida].totalpistes=NULL;
	copia[partida].ous=NULL;
	memset(copia[partida].nombre1,0,sizeof(copia[partida].nombre1));
	memset(copia[partida].nombre2,0,sizeof(copia[partida].nombre2));
	memset(copia[partida].nombre3,0,sizeof(copia[partida].nombre3));
	memset(copia[partida].nombre4,0,sizeof(copia[partida].nombre4));
}
int Elimina_Jugador (Partida *partidas, char nom_desc[20], int partida)
{
	//FUnció que elimina un jugador. Retorna 1 si ha eliminat com a mínim 1 jugador	
	if(strcmp(partidas[partida].nombre1,nom_desc)==0){
		
		strcpy(partidas[partida].nombre1, "");
		memset(partidas[partida].nombre1,0,sizeof(partidas[partida].nombre1));
		partidas[partida].socket1 = NULL;
		return 1;
	}
	if(strcmp(partidas[partida].nombre2,nom_desc)==0){
		
		strcpy(partidas[partida].nombre2, "");
		memset(partidas[partida].nombre2,0,sizeof(partidas[partida].nombre2));
		partidas[partida].socket2 = NULL;
	}
	if(strcmp(partidas[partida].nombre3,nom_desc)==0){
		
		strcpy(partidas[partida].nombre3, "");
		memset(partidas[partida].nombre3,0,sizeof(partidas[partida].nombre3));
		partidas[partida].socket3= NULL;
	}
	if(strcmp(partidas[partida].nombre4,nom_desc)==0){
		
		strcpy(partidas[partida].nombre4, "");
		memset(partidas[partida].nombre4,0,sizeof(partidas[partida].nombre4));
		partidas[partida].socket4= NULL;
		
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
	//Función que añade nuevo conectado. Retorna -1 si la lista está llena y 0 si se puede añadir el usuario a la lista de conectados
	if(lista->num == 100)
		return -1;
	else{
		int found=0;
		int j=0;
		for (j; j<lista->num && found==0;j++){
			if (lista->conectados[j].socket == socket)
				found=1;
		}
		strcpy(lista->conectados[j-1].nombre,nombre);
		printf("dins la llista hi ha %d usuaris\n",lista->num);
		return 0;
	}
}
int PonSocket (ListaConectados *lista, int socket){
	//Añade un socket a la lista de connectados, retorna 1 si correctamente y 0 si no
	if(lista->num == 100)
		return 0;
	else{
		lista->conectados[lista->num].socket = socket;
		printf("dins la llista hi ha %d usuaris\n",lista->num);
		lista->num = lista->num +1;
		return 1;
	}
}

int DameSocketRef(ListaConectados *lista, int socket){
	//TE da la posición de referencia del socket, si no existe retorna -1
	int j;
	int found=0;
	for (j=0; j<lista->num & found==0; j++)
	{
		if (lista->conectados[j].socket == socket)
			found=1;
	}
	if (found==1)
		return j-1;
	else
		return -1;
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
	//Función que te da el nombre con el socket. Devuelve 1, o -1 si no está en la lista
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
	//FUnción que elimina un conectado. Retorna 0 si elimina y -1 si el usuario no está en la lista
	int pos = DamePosicion(lista, nombre);
	if (pos == -1)
		return -1;
	else{
		
		lista->conectados[pos].socket=NULL;
		memset(lista->conectados[pos].nombre,0,sizeof(lista->conectados[pos].nombre));
		lista->num--;
		for (int i=pos; i < lista->num; i++)
			lista->conectados[i] = lista->conectados[i+1];
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
int BuscaConectado(ListaConectados *lista,char nombre[20]){
	//Función que busca si existe un conectado o no. REtorna 1 si lo encuentra o 0 si no.
	int i;
	int found=0;
	for(i=0;i<lista->num && found==0;i++){
		if(strcmp(nombre,lista->conectados[i].nombre)==0)
			found=1;
	}
	if (found==1)
		return 1;
	else
		return 0;
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
	int num_pistesMAX;
	int TotalPistes;
	int ous;
	int numjugadors;
	if (conn==NULL) {
		printf ("Error al crear la conexion: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	//inicializar la conexion
	//onn = mysql_real_connect (conn, "localhost","root", "mysql", "BD_JOC",0, NULL, 0);
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
	
	Partida partidaactual[100];
	
	
	
	
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
			/*			char buff3[300];*/
			/*			DameConectados(&miLista, buff3);*/
			/*			sprintf(buff3, "0/%s", buff3);*/
			/*			printf("%s\n",&miLista.conectados[0].nombre);*/
			/*			write (sock_conn,buff3, strlen(buff3));*/
			/*			printf("Misatge enviat al servei 0, desconnexió %s\n", buff3);*/
			char notificacio[20];
			memset(notificacio,0,sizeof(notificacio));
			DameConectados(&miLista, notificacio);
			printf("Missatge enviat al servei 6, notificacio %s\n", notificacio);
			if(miLista.num!=0){
				int j = 0;
				for (j = 0; j < miLista.num; j++)
					write (miLista.conectados[j].socket,notificacio, strlen(notificacio));
			}
			
		}
		//si el codi es 1 feim la consulta de la partida de menor duració
		else if (codigo ==1) {
			memset(buff2, 0, sizeof(buff2));
			char resp[512];
			Consulta_DuracioMin(conn, buff2);
			sprintf(resp,"1/%s",buff2);
			printf("Missatge enviat al servei 1, partida de menor duració %s\n", resp);
			write (sock_conn,resp, strlen(resp));
		}
		//si el codi es 2 feim la consulta de retornar la contrasenya amb la paraula de seguretat i el nom de l'usuari
		else if (codigo == 2){
			memset(buff2, 0, sizeof(buff2));
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
			p=strtok(NULL,"/");
			int partidaseleccionada=atoi(p);
			char llistajugadors[200];
			int error = Consulta_JugadorsPartida(conn,partidaseleccionada,llistajugadors);
			char res [512];
			sprintf(res, "3/%d/%d/%s",partidaseleccionada,error,llistajugadors);
			printf("Missatge enviat al servei 3: %s",res);
			write (sock_conn,res, strlen(res));
		}
		//si el codi es 4 executarem la func que ens permet entrar i fer consultes
		else if (codigo == 4)
		{
			memset(buff2, 0, sizeof(buff2));
			p = strtok(NULL, "/");
			char nom[20];
			strcpy(nom, p);
			p = strtok(NULL, "/");
			char contrasenya[20];
			strcpy(contrasenya, p);
			int connectat=BuscaConectado(&miLista,nom);
			if(connectat==0){
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
					for (j = 0; j< miLista.num; j++){
						write(miLista.conectados[j].socket,notificacio, strlen(notificacio));
						printf("Missatge enviat al servei 6, notificacio: %s\n", notificacio);
					}	
					
				}
				else
				{
					memset(buff2, 0, sizeof(buff2));
					sprintf(buff2, "4/0");
					printf("Missatge enviat al servei 4, login: %s\n", buff2);
					write (sock_conn,buff2, strlen(buff2));
				}
			}
			else{
				memset(buff2, 0, sizeof(buff2));
				sprintf(buff2, "4/2");
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
		//Si el codi es 7, creem la partida amb el seu anfitrió
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
					char buff5[512];
					sprintf(buff5,"7/%s/%d",nomanfitrio,partida);
					write(sock_conn,buff5, strlen(buff5));
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
		//Si el codi és 8 afegim els jugadors a la partida
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
				int err2 = PonJugador(&misPartidas,nomconvidat,sock_conn,partida);
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
		//SI el codi s 9, donem tots els jugadors de certa partida
		else if(codigo==9){
			int partida=DamePartida(&misPartidas,sock_conn);
			char jugadors[100];
			DameJugadoresTabla(&misPartidas,jugadors,partida);
			p=strtok(jugadors,"/");
			
			while (p!=NULL){
				char nomconvidat[20];
				strcpy(nomconvidat,p);
				char jugadors2[100];
				DameJugadoresTabla(&misPartidas,jugadors2,partida);
				sprintf(buff2,"9/%s",jugadors2);
				int socketconvidat=DameSocket(&miLista,nomconvidat);
				write(socketconvidat,buff2, strlen(buff2));
				p=strtok(NULL,"/");
			}
			
		}
		//Afegim una partida acabada a la base de dades
		else if (codigo==10){
			printf("p = %s\n",p);
			p=strtok(NULL,"/");
			int partida=atoi(p);
			printf("partida %d\n",partida);
			p=strtok(NULL,"/");
			int opcio=atoi(p);
			if (opcio==1){
				p=strtok(NULL,"/");
				int partida = atoi(p);
				p=strtok(NULL,"/");
				char guanyador[20];
				strcpy(guanyador,p);
				p=strtok(NULL,"/");
				char data[20];
				strcpy(data,p);
				p=strtok(NULL,"/");
				char hora[20];
				strcpy(hora,p);
				p=strtok(NULL,"/");
				int duracio = atoi(p);
				int ID = AfegirPartida(conn,data,hora,duracio,guanyador);
				int participacions=0;
				if(strcmp(misPartidas[partida].nombre1,"")!=0){
					int ID1=Consulta_ID_Jugador(conn,misPartidas[partida].nombre1);
					AfegirEnllac(conn,ID,ID1,participacions);
				}
				if(strcmp(misPartidas[partida].nombre2,"")!=0){
					int ID2=Consulta_ID_Jugador(conn,misPartidas[partida].nombre2);
					AfegirEnllac(conn,ID,ID2,participacions);
				}
				if(strcmp(misPartidas[partida].nombre3,"")!=0){
					int ID3=Consulta_ID_Jugador(conn,misPartidas[partida].nombre3);
					AfegirEnllac(conn,ID,ID3,participacions);
				}
				if(strcmp(misPartidas[partida].nombre3,"")!=0){
					int ID3=Consulta_ID_Jugador(conn,misPartidas[partida].nombre3);
					AfegirEnllac(conn,ID,ID3,participacions);
				}
				if(strcmp(partidaactual[partida].nombre4,"")!=0){
					int ID4=Consulta_ID_Jugador(conn,partidaactual[partida].nombre4);
					AfegirEnllac(conn,ID,ID4,participacions);
				}
				pthread_mutex_lock(&mutex);
				int err=EliminarPartida(&misPartidas,partida);
				TotalPistes=0;
				pthread_mutex_unlock(&mutex);
				
				
			}
			
			
			if (err == -1)
				printf("La partida per eliminar no existeix");
			else
				printf("partida eliminada correctament");
			printf("Primer nom de la partida eliminada: %s",misPartidas[partida].nombre1);
		}
		//Rebem una posició d'un jugador i enviem la posició a la resta
		else if (codigo == 11)
		{
			p=strtok(NULL,"/");
			int x=atoi(p);
			p=strtok(NULL,"/");
			int y=atoi(p);
			p=strtok(NULL,"/");
			/*			int partida=atoi(p);*/
			int partida=DamePartida(&misPartidas,sock_conn);
			char jugadors[100];
			DameJugadoresTabla(&misPartidas,jugadors,partida);
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
					write(socket,buff2, strlen(buff2));
				}
				p=strtok(NULL,"/");
			}
		}
		//Eliminem un jugador que ha estat eliminat 
		else if (codigo==12){
			p = strtok(NULL, "/");
			char nom_desconectat[20];
			strcpy(nom_desconectat, p);
			
			p = strtok(NULL, "/");
			int partida;
			partida = atoi(p);
			pthread_mutex_lock(&mutex);
			Elimina_Jugador (&misPartidas,nom_desconectat, partida);
			pthread_mutex_unlock(&mutex);
			char jugadors[100];
			DameJugadoresTabla(&misPartidas,jugadors,partida);
			p=strtok(jugadors,"/");
			
			while (p!=NULL){
				char nom[20];
				strcpy(nom,p);
				char nom_conn[100];
				int err=DameNombre(&miLista,sock_conn,nom_conn);
				if( strcmp(nom_conn,nom)!=0)
				{
					sprintf(buff2,"12/%s",nom_desconectat);
					int socket=DameSocket(&miLista,nom);
					write(socket,buff2, strlen(buff2));
				}
				p=strtok(NULL,"/");
			}
			
		}
		//REcibimos un mensaje del chat y lo enviamos a todos
		else if (codigo == 13){
			p = strtok(NULL, "/");
			int partida = atoi(p);
			printf("Partida: %d", partida);
			char usuari[20];
			p = strtok(NULL, "/");
			strcpy(usuari,p);
			char missatge[512];
			p = strtok(NULL, "/");
			strcpy(missatge,p);
			sprintf(buff2,"13/%s/%s",usuari,missatge);
			printf("Enviar a Nombre 1: %s\n",misPartidas[partida].nombre1);
			if (misPartidas[partida].socket1!=NULL){
				printf("Enviant missatge a 1: %s\n",buff2);
				write(misPartidas[partida].socket1,buff2, strlen(buff2));
			}
			printf("Enviar a Nombre 2: %s\n",misPartidas[partida].nombre2);
			if (misPartidas[partida].socket2!=NULL){
				printf("Enviant missatge a 2: %s\n",buff2);
				write(misPartidas[partida].socket2,buff2, strlen(buff2));
			}
			printf("Enviar a Nombre 3: %s\n",misPartidas[partida].nombre3);
			if (misPartidas[partida].socket3!=NULL){
				printf("Enviant missatge a 3: %s\n",buff2);
				write(misPartidas[partida].socket3,buff2, strlen(buff2));
			}
			printf("Enviar a Nombre 4: %s\n",misPartidas[partida].nombre4);
			if (misPartidas[partida].socket4!=NULL){
				printf("Enviant missatge  4: %s\n",buff2);
				write(misPartidas[partida].socket4,buff2, strlen(buff2));
			}
		}
		//Codigo relacionado con asignar las variables de la pista morse (saber si ya se ha abierto el audio, la solución...)
		else if(codigo==16)
		{
			p=strtok(NULL, "/");
			int partida = atoi(p);
			
			p=strtok(NULL,"/");
			int opcio=atoi(p);
			
			p=strtok(NULL,"/");
			char booleano[20];
			strcpy(booleano,p);
			if(strcmp(booleano,"true")==0)
				sprintf(buff2,"16/%d/%d/true",partida,opcio);
			else
				sprintf(buff2,"16/%d/%d/false",partida,opcio);
			if (misPartidas[partida].socket1!=NULL){
				printf("Enviant missatge a 1: %s\n",buff2);
				write(misPartidas[partida].socket1,buff2, strlen(buff2));
			}
			printf("Enviar a Nombre 2: %s\n",misPartidas[partida].nombre2);
			if (misPartidas[partida].socket2!=NULL){
				printf("Enviant missatge a 2: %s\n",buff2);
				write(misPartidas[partida].socket2,buff2, strlen(buff2));
			}
			printf("Enviar a Nombre 3: %s\n",misPartidas[partida].nombre3);
			if (misPartidas[partida].socket3!=NULL){
				printf("Enviant missatge a 3: %s\n",buff2);
				write(misPartidas[partida].socket3,buff2, strlen(buff2));
			}
			printf("Enviar a Nombre 4: %s\n",misPartidas[partida].nombre4);
			if (misPartidas[partida].socket4!=NULL){
				printf("Enviant missatge  4: %s\n",buff2);
				write(misPartidas[partida].socket4,buff2, strlen(buff2));
			}
		}
		//COdigo para saber si la pista es correcta
		else if(codigo == 17)
		{
			p=strtok(NULL, "/");
			int partida = atoi(p);
			sprintf(buff2,"17/%d",partida);
			if (misPartidas[partida].socket1!=NULL){
				printf("Enviant missatge a 1: %s\n",buff2);
				write(misPartidas[partida].socket1,buff2, strlen(buff2));
			}
			printf("Enviar a Nombre 2: %s\n",misPartidas[partida].nombre2);
			if (misPartidas[partida].socket2!=NULL){
				printf("Enviant missatge a 2: %s\n",buff2);
				write(misPartidas[partida].socket2,buff2, strlen(buff2));
			}
			printf("Enviar a Nombre 3: %s\n",misPartidas[partida].nombre3);
			if (misPartidas[partida].socket3!=NULL){
				printf("Enviant missatge a 3: %s\n",buff2);
				write(misPartidas[partida].socket3,buff2, strlen(buff2));
			}
			printf("Enviar a Nombre 4: %s\n",misPartidas[partida].nombre4);
			if (misPartidas[partida].socket4!=NULL){
				printf("Enviant missatge  4: %s\n",buff2);
				write(misPartidas[partida].socket4,buff2, strlen(buff2));
			}
			
		}
		else if (codigo == 14){ //codi que que reb el servidor quan algú ha trobat una pista
			p = strtok(NULL, "/");
			int partida2 = atoi(p);
			int partida = DamePartida(&misPartidas, sock_conn);
			pthread_mutex_lock(&mutex);
			TotalPistes = TotalPistes + 1;
			AugmentarPistes(&misPartidas,partida);
			numjugadors= DameNumJugadores(&misPartidas,partida);
			if (numjugadors == 1)
			{	//la pista morse només es pot resoldre si hi ha +1 jugadors
				num_pistes = 2;
			}
			num_pistesMAX = num_pistes*numjugadors-(numjugadors-1);
			printf("Numero de jugadors %d\n",numjugadors);
			pthread_mutex_unlock(&mutex);
			sprintf(buff2, "14/%d/%d", misPartidas[partida].totalpistes, num_pistesMAX);	
			printf("Duim %d pistes recol·lectades i en necessitem %d\n",  misPartidas[partida].totalpistes, num_pistesMAX);
			printf("Hem entrat dins 14: %s\n",buff2);
			printf("socket1: %d\n",misPartidas[partida].socket1);
			
			if (misPartidas[partida].totalpistes < num_pistesMAX){
				//notificam als altres jugadors que hem trobat una partida nova
				if (misPartidas[partida].socket1!=NULL || misPartidas[partida].socket1 == 0 ){
					printf("Hem entrat dins socket1: %d\n",misPartidas[partida].socket1);
					write(misPartidas[partida].socket1,buff2, strlen(buff2));
				}
				
				if (misPartidas[partida].socket2!=NULL){
					write(misPartidas[partida].socket2,buff2, strlen(buff2));
				}
				
				if (misPartidas[partida].socket3!=NULL ){
					write(misPartidas[partida].socket3,buff2, strlen(buff2));
				}
				
				if (misPartidas[partida].socket4!=NULL ){
					write(misPartidas[partida].socket4,buff2, strlen(buff2));
				}
			}
			//quan hem trobat totes les pistes comença la cerca de l'ou daura
			
			else if (num_pistesMAX == misPartidas[partida].totalpistes)
			{
				printf("Hem entrat dins 15 socket1: %d\n",misPartidas[partida].socket1);
				
				int posicio_pistaFinal1_x = 339;
				int posicio_pistaFinal1_y = 512;
				int posicio_pistaFinal2_x = 181;
				int posicio_pistaFinal2_y = 498;
				int posicio_pistaFinal3_x = 270;
				int posicio_pistaFinal3_y = 150;
				int posicio_pistaFinal4_x = 166;
				int posicio_pistaFinal4_y = 128;
				
				char buff1[100];
				char buff2[100];
				char buff3[100];
				char buff4[100];
				
				sprintf(buff1,"15/%d/%d",posicio_pistaFinal1_x,posicio_pistaFinal1_y);
				sprintf(buff2,"15/%d/%d",posicio_pistaFinal2_x,posicio_pistaFinal2_y);
				sprintf(buff3,"15/%d/%d",posicio_pistaFinal3_x,posicio_pistaFinal3_y);
				sprintf(buff4,"15/%d/%d",posicio_pistaFinal4_x,posicio_pistaFinal4_y);
				
				if (misPartidas[partida].socket1!=NULL || misPartidas[partida].socket1 == 0){
					printf("Hem entrat dins 15 socket 1 (%d) write: %s\n",misPartidas[partida].socket1,buff1);
					write(misPartidas[partida].socket1,buff1, strlen(buff1));
				}
				printf("soket 2 %d\n",misPartidas[partida].socket2);
				if (misPartidas[partida].socket2!=NULL){
					printf("Hem entrat dins 15 socket 2 (%d) write: %s\n",misPartidas[partida].socket2, buff2);
					write(misPartidas[partida].socket2,buff2, strlen(buff2));
				}
				printf("soket 2 %d\n",misPartidas[partida].socket3);
				if (misPartidas[partida].socket3!=NULL ){
					write(misPartidas[partida].socket3,buff3, strlen(buff3));
				}
				printf("soket 2 %d\n",misPartidas[partida].socket4);
				if (misPartidas[partida].socket4!=NULL ){
					write(misPartidas[partida].socket4,buff4, strlen(buff4));
				}
			}
			
		}
		//Codi que es rep quan algú agafa l'ou
		else if (codigo ==18){
			p = strtok(NULL, "/");
			int partida = atoi(p);
			pthread_mutex_lock(&mutex);
			AugmentarOus(&misPartidas,partida);
			pthread_mutex_unlock(&mutex);
			printf("jugadors a la partida: %d i número d'ous: %d",numjugadors,misPartidas[partida].ous);
			if (numjugadors == misPartidas[partida].ous)
			{
				
				char buff1[100];
				
				sprintf(buff1,"18/");
				
				if (misPartidas[partida].socket1!=NULL || misPartidas[partida].socket1 == 0){
					printf("Hem entrat dins 18 socket 1 write: %s\n",buff1);
					write(misPartidas[partida].socket1,buff1, strlen(buff1));
				}
				
				if (misPartidas[partida].socket2!=NULL){
					write(misPartidas[partida].socket2,buff1, strlen(buff1));
				}
				
				if (misPartidas[partida].socket3!=NULL ){
					write(misPartidas[partida].socket3,buff1, strlen(buff1));
				}
				
				if (misPartidas[partida].socket4!=NULL ){
					write(misPartidas[partida].socket4,buff1, strlen(buff1));
				}
			}
		}
		//Codi quan algú és pillat
		else if (codigo == 19)
		{
			p = strtok(NULL, "/");
			int partida = atoi(p);
			p = strtok(NULL, "/");
			char pillat[20];
			strcpy(pillat,p);
			int socket_pillat = DameSocket(&miLista, pillat);
			char buff1[100];
			sprintf(buff1,"19/");
			write(socket_pillat,buff1, strlen(buff1));
		}
		//Codi per enviar qui guanya la partida
		else if(codigo==20)
		{
			p = strtok(NULL, "/");
			int partida = atoi(p);
			p = strtok(NULL, "/");
			char guanyador[20];
			strcpy(guanyador,p);
			char buff1[100];
			
			sprintf(buff1,"20/%s",guanyador);
			
			
			
			if (misPartidas[partida].socket1!=NULL || misPartidas[partida].socket1 == 0){
				printf("Hem entrat dins 15 socket 1 (%d) write: %s\n",misPartidas[partida].socket1,buff1);
				write(misPartidas[partida].socket1,buff1, strlen(buff1));
			}
			
			if (misPartidas[partida].socket2!=NULL){
				printf("Hem entrat dins 15 socket 2 (%d) write: %s\n",misPartidas[partida].socket2, buff2);
				write(misPartidas[partida].socket2,buff1, strlen(buff1));
			}
			
			if (misPartidas[partida].socket3!=NULL ){
				write(misPartidas[partida].socket3,buff1, strlen(buff1));
			}
			
			if (misPartidas[partida].socket4!=NULL ){
				write(misPartidas[partida].socket4,buff1, strlen(buff1));
			}
			
		}
		//Codi per consultar les partides d'un jugador
		else if (codigo==21){
			p = strtok(NULL, "/");
			int partida = atoi(p);
			p = strtok(NULL, "/");
			char jugador[20];
			strcpy(jugador,p);
			
			char llistapartides[512];
			Consulta_PartidesJugador(conn,jugador,llistapartides);
			char res [1000];
			sprintf(res, "21/%d/%s",partida,llistapartides);
			printf("Missatge enviat al servei 21: %s",res);
			write (sock_conn,res, strlen(res));
			
		}
		//Codi per eliminar un usuari de la BD
		else if (codigo==22){
			p=strtok(NULL,"/");
			char nomeliminat[20];
			strcpy(nomeliminat,p);
			EliminarUsuari(conn,nomeliminat);
			
			char resp[20];
			strcpy(resp,"22/Usuari Eliminat");
			write(sock_conn,resp,strlen(resp));
			pthread_mutex_lock(&mutex);
			Elimina(&miLista, nomeliminat);
			pthread_mutex_unlock(&mutex);
		}
		else{
			/*			p=strtok(NULL,"/");*/
			/*			int partida = atoi(p);*/
			/*			p=strtok(NULL,"/");*/
			/*			char guanyador[20];*/
			/*			strcpy(guanyador,p);*/
			/*			p=strtok(NULL,"/");*/
			/*			char data[20];*/
			/*			strcpy(data,p);*/
			/*			p=strtok(NULL,"/");*/
			/*			char hora[20];*/
			/*			strcpy(hora,p);*/
			/*			p=strtok(NULL,"/");*/
			/*			int duracio = atoi(p);*/
			/*			int ID = AfegirPartida(conn,data,hora,duracio,guanyador);*/
			/*			int participacions=0;*/
			/*			if(strcmp(partidaactual[partida].nombre1,"")!=0){*/
			/*				int ID1=Consulta_ID_Jugador(conn,partidaactual[partida].nombre1);*/
			/*				AfegirEnllac(conn,ID,ID1,participacions);*/
			/*			}*/
			/*			if(strcmp(partidaactual[partida].nombre2,"")!=0){*/
			/*				int ID2=Consulta_ID_Jugador(conn,partidaactual[partida].nombre2);*/
			/*				AfegirEnllac(conn,ID,ID2,participacions);*/
			/*			}*/
			/*			if(strcmp(partidaactual[partida].nombre3,"")!=0){*/
			/*				int ID3=Consulta_ID_Jugador(conn,partidaactual[partida].nombre3);*/
			/*				AfegirEnllac(conn,ID,ID3,participacions);*/
			/*			}*/
			/*			if(strcmp(partidaactual[partida].nombre3,"")!=0){*/
			/*				int ID3=Consulta_ID_Jugador(conn,partidaactual[partida].nombre3);*/
			/*				AfegirEnllac(conn,ID,ID3,participacions);*/
			/*			}*/
			/*			if(strcmp(partidaactual[partida].nombre4,"")!=0){*/
			/*				int ID4=Consulta_ID_Jugador(conn,partidaactual[partida].nombre4);*/
			/*				AfegirEnllac(conn,ID,ID4,participacions);*/
			/*			}*/
			/*			EliminarCopia(&partidaactual,partida);*/
			/*			if(strcmp(misPartidas[partida].nombre1,"")!=0){*/
			/*				int ID1=Consulta_ID_Jugador(conn,misPartidas[partida].nombre1);*/
			/*				AfegirEnllac(conn,ID,ID1,participacions);*/
			/*			}*/
			/*			if(strcmp(misPartidas[partida].nombre2,"")!=0){*/
			/*				int ID2=Consulta_ID_Jugador(conn,misPartidas[partida].nombre2);*/
			/*				AfegirEnllac(conn,ID,ID2,participacions);*/
			/*			}*/
			/*			if(strcmp(misPartidas[partida].nombre3,"")!=0){*/
			/*				int ID3=Consulta_ID_Jugador(conn,misPartidas[partida].nombre3);*/
			/*				AfegirEnllac(conn,ID,ID3,participacions);*/
			/*			}*/
			/*			if(strcmp(misPartidas[partida].nombre3,"")!=0){*/
			/*				int ID3=Consulta_ID_Jugador(conn,misPartidas[partida].nombre3);*/
			/*				AfegirEnllac(conn,ID,ID3,participacions);*/
			/*			}*/
			/*			if(strcmp(misPartidas[partida].nombre4,"")!=0){*/
			/*				int ID4=Consulta_ID_Jugador(conn,misPartidas[partida].nombre4);*/
			/*				AfegirEnllac(conn,ID,ID4,participacions);*/
			/*			}*/
			
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
		int err = PonSocket(&miLista,sock_conn);
		if (err==1)
			printf("Socket añadido\n");
		else
			printf("Socket no añadido\n");
		
		int w = DameSocketRef(&miLista, sock_conn); //DOnem el socket per referencia
		pthread_create(&thread, NULL, AtenderCliente, &miLista.conectados[w].socket);
		printf("He sortit del atender clientes\n");
		i = i + 1;
	}
}


void Consulta_DuracioMin(MYSQL *conn,char buff2[512])
{
	//FUnció que consulta a la BD la duració mínima d'alguna partida
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
	//FUnció que retorna la contrasenya mitjançant una paraula de seguretat
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
		strcpy(buff2,"2/error");
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
	//FUnció que consulta el jugador amb màximes participacions
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
	//FUnció per a realitzar el login, retorna 1 si s correcte i 0 si no
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
	
	if (row==NULL)
	{
		return 0;
	}
	else{
		
		if(strcmp(contrasenya, row[0])==0)
		{
			return 1; //torna 1 si hem trobat la contrasenya
			
		}
		else 
		   return 0;//no hem trobat la contrasenya
	}
	
	
	
	
}
int Registrar(MYSQL *conn, char buff2[20], char* nom[20], char contrasenya[20], char paraulaSeguretat[20])
{
	//FUnció per registrar un usuari. REtorna si s'ha pogut registrar bé o no.
	int err;
	// Estructura especial para almacenar resultados de consultas 
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	
	char consulta [80];
	
	if(CercarUsuari(conn,nom) == 0)//el nom de l'usuari no existeix, per tant el podem crear
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
		sprintf(buff2, "5/%s",r);
		
	}
}
int CercarUsuari(MYSQL *conn, char* nom[20])
{
	//FUnció per retornar un usuari. REtorna 1 si el torba i 0 si no
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
	//FUnció que busca el ID mxim de la base de dades retorna el ID
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

void Consulta_PartidesJugador(MYSQL *conn, char nom[20], char resultat[512])
{
	//Funció que rep el nom d'un jugador i un buffer, i en el buffer escriu la llista de les partides amb el ID, la data i l'hora, en forma de numpartides/ID1/data1/hora1/ID2/data2/hora2
	int err;
	// Estructura especial para almacenar resultados de consultas 
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	int cont=0;
	char consulta[200];
	strcpy(consulta,"SELECT PARTIDA.ID, PARTIDA.DATA, PARTIDA.HORA FROM (JUGADOR,ENLLAÇ,PARTIDA) WHERE JUGADOR.NOM = '");//comprovam que l'usuari no existeix
	strcat(consulta, nom);
	strcat(consulta, "' AND JUGADOR.ID=ENLLAÇ.ID_J AND ENLLAÇ.ID_P=PARTIDA.ID");
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
		resultat[0] = NULL;
		char llista[512];
		strcpy(llista,"");
		sprintf (llista,"%s/%s/%s/%s", llista, row[0], row[1], row[2]);
		cont=cont+1;
		// obtenim la següent fila ja que dos o més jugadors poden 
		
		row = mysql_fetch_row (resultado);
		while (row !=NULL) {
			// la columna 0 conté la partida, la 1 la data i la 2 l'hora
			sprintf (llista,"%s/%s/%s/%s", llista, row[0], row[1], row[2]);
			cont=cont+1;
			// obtenim la següent fila ja que dos o més jugadors poden 
			
			row = mysql_fetch_row (resultado);
		}
		sprintf(resultat,"%d/%s",cont,llista);
		printf(resultat);
	}
	// cerrar la conexion con el servidor MYSQL 
	
	
}

int Consulta_JugadorsPartida(MYSQL *conn, int partida, char buff2[512])
{
	//Funció que rep una partida i consulta els seus jugadors (inclou el que pregunta). Retorna la llista en forma de numJUgadors/nom1/nom2/nm3/...
	int err;
	// Estructura especial para almacenar resultados de consultas 
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	
	char consulta[200];
	strcpy(consulta,"SELECT JUGADOR.NOM FROM (JUGADOR, ENLLAÇ,PARTIDA) WHERE PARTIDA.ID = ");//comprovam que l'usuari no existeix
	sprintf(consulta, "%s%d", consulta, partida);
	strcat(consulta, " AND PARTIDA.ID=ENLLAÇ.ID_P AND ENLLAÇ.ID_J=JUGADOR.ID");
	err = mysql_query (conn, consulta);//feim la consulta a la BD_JOC
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	if (row == NULL){
		printf ("No se han obtenido datos en la consulta\n");
		return -1;
	}
	else{
		buff2[0] = NULL;
		char respuesta[200];
		sprintf(respuesta,"%d",partida);
		int cont=0;
		while (row !=NULL) {
			// la columna 0 conté el nom dels jugadors
			sprintf (respuesta,"%s/%s", respuesta, row[0]);
			cont=cont+1;
			// obtenim la següent fila ja que dos o més jugadors poden 
			
			row = mysql_fetch_row (resultado);
		}
		
		sprintf(buff2,"%d/%s",cont,respuesta);
		printf(buff2);
		return 0;
	}
	// cerrar la conexion con el servidor MYSQL 
	
	
}

void EliminarUsuari(MYSQL *conn, char nom[20])
{
	//FUnció que rep un nom d'usuari i l'elimina de la BD
	int err;
	// Estructura especial para almacenar resultados de consultas 
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	
	char consulta[200];
	strcpy(consulta,"DELETE FROM JUGADOR WHERE JUGADOR.NOM = '");//eliminem l'usuari
	strcat(consulta,nom);
	strcat(consulta, "'");
	err = mysql_query (conn, consulta);//feim la consulta a la BD_JOC
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
}

void EliminarPartidaBD(MYSQL *conn, int partida)
{
	//funció que rep una partida i l'elimina de la BD
	int err;
	// Estructura especial para almacenar resultados de consultas 
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	
	char consulta[200];
	char partidas[20];
	sprintf(partidas,"%d",partida);
	strcpy(consulta,"DELETE FROM (PARTIDA) WHERE PARTIDA.ID = ");//eliminem la partida
	strcat(consulta,partidas);
	err = mysql_query (conn, consulta);//feim la consulta a la BD_JOC
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
}

int AfegirPartida(MYSQL *conn, char data[20], char hora[20], int duracio, char guanyador[20])
{
	//FUnció que rep la data, hora i duració de la partida i la crea. REtorna el ID  de la partida
	int err;
	// Estructura especial para almacenar resultados de consultas 
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	
	int ID = MaxID_Partida(conn);
	
	char IDs[10];
	sprintf(IDs,"%d", ID);
	
	char consulta[200];
	strcpy(consulta,"INSERT INTO PARTIDA VALUES (");//afegim partida
	strcat(consulta,IDs);
	strcat(consulta, ",'");
	char duraciochar[20];
	sprintf(duraciochar, "%d", duracio);
	strcat(consulta,data);
	strcat(consulta, "','");
	strcat(consulta,hora);
	strcat(consulta, "',");
	strcat(consulta,duraciochar);
	strcat(consulta, ",'");
	strcat(consulta,guanyador);
	strcat(consulta, "');");
	
	err = mysql_query (conn, consulta);//feim la consulta a la BD_JOC
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	return ID;
}

int MaxID_Partida(MYSQL *conn)//retorna la ID mÃ¯Â¿Â©s gran
{
	//FUnció que busca el ID màxim de les partides
	int err;
	// Estructura especial para almacenar resultados de consultas 
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	
	int ID;
	char consulta [80];
	
	strcpy(consulta, "SELECT MAX(PARTIDA.ID) FROM PARTIDA");//ID s'assigna automaticament, cerca el ID mÃ¯Â¿Â©s gran i li suma 1
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

void AfegirEnllac(MYSQL *conn, int partida, int jugador, int participacions)
{
	//FUnció per afegir l'enllaçentre partida i jugador a la BD
	int err;
	
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	
	char jugadorS[20];
	sprintf(jugadorS,"%d",jugador);
	char partidaS[20];
	sprintf(partidaS,"%d",partida);
	char participacionsS[20];
	sprintf(participacionsS,"%d",participacions);
	
	char consulta[200];
	strcpy(consulta,"INSERT INTO ENLLAÇ VALUES (");
	strcat(consulta,partidaS);
	strcat(consulta,",");
	strcat(consulta,jugadorS);
	strcat(consulta,",");
	strcat(consulta,participacionsS);
	strcat(consulta,");");
	
	err = mysql_query (conn, consulta);//feim la consulta a la BD_JOC
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
}

/*void AfegirEnllaç(MYSQL *conn, int partida, int jugador, int participacions)*/
/*{*/
//FUnció que rep el ID de la partida i del jugador i les participacions(0 en cas de perdre i 10 en cas de guanyar) i crea l'enllaç entre la partida i el jugador
/*	int err;*/
// Estructura especial para almacenar resultados de consultas
/*	MYSQL_RES *resultado;*/
/*	MYSQL_ROW row;*/

/*	char jugadors[20];*/
/*	sprintf(jugadors,"%d",jugador);*/
/*	char partidas[20];*/
/*	sprintf(partidas,"%d",partida);*/
/*	char participacionss[20];*/
/*	sprintf(participacionss,"%d",participacions);*/

/*	char consulta[200];*/
/*	strcpy(consulta,"INSERT INTO ENLLAÇ VALUES(");*/
/*	strcat(consulta,partidas);*/
/*	strcat(consulta,",");*/
/*	strcat(consulta, jugadors);*/
/*	strcat(consulta,",");*/
/*	strcat(consulta,participacionss);*/
/*	strcat(consulta,");");*/

/*	err = mysql_query (conn, consulta);*///feim la consulta a la BD_JOC
/*	if (err!=0) {*/
/*		printf ("Error al consultar datos de la base %u %s\n",*/
/*				mysql_errno(conn), mysql_error(conn));*/
/*		exit (1);*/
/*	}*/
/*}*/


int Consulta_ID_Jugador(MYSQL *conn,char nom[20])//retorna la ID mÃ¯Â¿Â©s gran
{
	//Funció per consultar el ID d'un jugador. REtorna el ID i -1 si no el troba
	int err;
	// Estructura especial para almacenar resultados de consultas 
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	
	int ID=-1;
	char consulta [80];
	
	strcpy(consulta, "SELECT JUGADOR.ID FROM JUGADOR WHERE JUGADOR.NOM = '");//ID s'assigna automaticament, cerca el ID mÃ¯Â¿Â©s gran i li suma 1
	strcat(consulta, nom);
	strcat(consulta,"'");
	err=mysql_query (conn, consulta);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	ID=atoi(row[0]);
	return ID;
	
}

