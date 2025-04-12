using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityEngine.Advertisements;

public class MenuFin : MonoBehaviour {

	public Text txtTitulo, txtMarca, puntosvalue;

	[Header("TRADUCCION ESPANOL")]
	public string titulo_esp;
	public string marca_esp;

	[Header("TRADUCCION INGLES")]
	public string titulo_eng;
	public string marca_eng;


	// Cargamos la partida
	void Start(){
		
		pintarInformacion();

	}




	// OPCIONES DEL MENU
	public void OpcionMenu(){
		

//		if (Advertisement.IsReady())
//		{
//			Advertisement.Show();
//		}


		GameManager.instancia.estadoActual = TiposDeEstado.MENU;
		GameManager.instancia.cambiarEscena("menu"); //este metodo del gamemanager ya guarda la partida
	}




	private void pintarInformacion(){

		if( GameManager.instancia.idiomaActual == 0 ){
			txtTitulo.text = titulo_esp;
			txtMarca.text = marca_esp;
		}
		else if( GameManager.instancia.idiomaActual == 1 ){
			txtTitulo.text = titulo_eng;
			txtMarca.text = marca_eng;
		}

		puntosvalue.text = GameManager.instancia.puntos.ToString();


	}

}
