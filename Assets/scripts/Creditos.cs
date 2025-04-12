using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Creditos : MonoBehaviour {

	public Text textocentral;

	[Header("TRADUCCION ESPANOL")]
	public string texto_esp;

	[Header("TRADUCCION INGLES")]
	public string texto_eng;



	// Cargamos la partida
	void Start(){
		
		pintarTextos();

	}




	// OPCIONES DEL MENU

	public void OpcionContribuir(){


	}
		

	public void OpcionMenu(){
		GameManager.instancia.estadoActual = TiposDeEstado.MENU;
		GameManager.instancia.cambiarEscena("menu"); //este metodo del gamemanager ya guarda la partida
	}
		


	private void pintarTextos(){
		GameManager.instancia.cargarPreferencias();

		if( GameManager.instancia.idiomaActual == 0 ){
			textocentral.text = texto_esp;

		}
		else if( GameManager.instancia.idiomaActual == 1 ){
			textocentral.text = texto_eng;
		}


	}






}
