using UnityEngine;

public class Intro : MonoBehaviour {

	// Al comenzar cambiamos al menu principal después de una espera
	void Start () {
		GameManager.instancia.puntos = PlayerPrefs.GetInt("puntosMax");
		GameManager.instancia.idiomaActual = PlayerPrefs.GetInt("idioma");
		PlayerPrefs.DeleteAll();
		PlayerPrefs.SetInt("puntosMax", GameManager.instancia.puntos);
		PlayerPrefs.SetInt("idioma", GameManager.instancia.idiomaActual);
		PlayerPrefs.SetInt("numpartida", 0);

		Invoke("aux", 2f);
	}

	public void aux(){
		
		GameManager.instancia.estadoActual = TiposDeEstado.MENU;
		GameManager.instancia.cambiarEscena("menu");
	}


}
