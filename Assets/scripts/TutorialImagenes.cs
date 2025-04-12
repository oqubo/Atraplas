using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour {

	public Image[] imagenes;
	public int imagenActual = 0;

	// Cargamos la partida
	void Start(){
		
		GameManager.instancia.estadoActual = TiposDeEstado.TUTORIAL;
		imagenes[imagenActual].gameObject.SetActive(true);

	}
		


	// OPCIONES DEL MENU
	public void siguiente(){


		if(imagenActual >= imagenes.Length-1){

			if(GameManager.instancia.tutorialVisto == 0)
			{
				GameManager.instancia.tutorialVisto = 1;
				PlayerPrefs.SetInt("tutorialVisto",1);
				GameManager.instancia.estadoActual = TiposDeEstado.JUEGO;
				GameManager.instancia.cambiarEscena("juego");
			}

			else
			{
				GameManager.instancia.estadoActual = TiposDeEstado.MENU;
				GameManager.instancia.cambiarEscena("menu");
			}
		}


		else{
			imagenes[imagenActual].gameObject.SetActive(false);
			imagenActual++;
			imagenes[imagenActual].gameObject.SetActive(true);
		}

	}





}
