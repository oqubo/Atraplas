using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour {

	public Text txtTitulo, btnJugar, btnTutorial, btnCreditos, txtMarca, puntosvalue;

	[Header("TRADUCCION ESPANOL")]
	public string titulo_esp;
	public string jugar_esp, tutorial_esp, creditos_esp, marca_esp;

	[Header("TRADUCCION INGLES")]
	public string titulo_eng;
	public string jugar_eng, tutorial_eng, creditos_eng, marca_eng;


	// Cargamos la partida
	void Start(){
		
		GameManager.instancia.estadoActual = TiposDeEstado.MENU;
		GameManager.instancia.cargarPartida();
		pintarTextos();
		GameManager.instancia.iniciarPartida();


	}




	// OPCIONES DEL MENU
	public void OpcionJuego(){

		GameManager.instancia.numpartidas++;

		if(GameManager.instancia.tutorialVisto == 0)
		{
			GameManager.instancia.estadoActual = TiposDeEstado.TUTORIAL;
			GameManager.instancia.cambiarEscena("tutorial");
		}

		else
		{
			GameManager.instancia.estadoActual = TiposDeEstado.JUEGO;
			GameManager.instancia.cambiarEscena("juego");
		}
	}

	public void OpcionIntro(){
		GameManager.instancia.estadoActual = TiposDeEstado.INTRO;
		GameManager.instancia.cambiarEscena("intro");
	}

	public void OpcionTutorial(){
		GameManager.instancia.estadoActual = TiposDeEstado.TUTORIAL;
		GameManager.instancia.cambiarEscena("tutorial");
	}
		

	public void OpcionCreditos(){
		GameManager.instancia.estadoActual = TiposDeEstado.CREDITOS;
		GameManager.instancia.cambiarEscena("creditos");
	}

	public void OpcionMenu(){
		GameManager.instancia.estadoActual = TiposDeEstado.MENU;
		GameManager.instancia.cambiarEscena("menu"); //este metodo del gamemanager ya guarda la partida
	}
		

	public void idomaEsp(){ 
		GameManager.instancia.idiomaActual = 0;
		GameManager.instancia.guardarPreferencias();
		pintarTextos();
	}

	public void idomaIng(){ 
		GameManager.instancia.idiomaActual = 1;
		GameManager.instancia.guardarPreferencias();
		pintarTextos();
	}

	private void pintarTextos(){
		GameManager.instancia.cargarPreferencias();

		if( GameManager.instancia.idiomaActual == 0 ){
			txtTitulo.text = titulo_esp;
			btnJugar.text = jugar_esp;
			btnTutorial.text = tutorial_esp;
			btnCreditos.text = creditos_esp;
			txtMarca.text = marca_esp;
		}
		else if( GameManager.instancia.idiomaActual == 1 ){
			txtTitulo.text = titulo_eng;
			btnJugar.text = jugar_eng;
			btnTutorial.text = tutorial_eng;
			btnCreditos.text = creditos_eng;
			txtMarca.text = marca_eng;
		}

		puntosvalue.text = PlayerPrefs.GetInt ("puntosMax").ToString();
	}






}
