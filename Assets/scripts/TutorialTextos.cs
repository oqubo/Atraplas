using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialTextos : MonoBehaviour {

	public Text cajadetexto;
	public Text[] textos_esp;
	public Text[] textos_eng;
	private Text[] textos;
	public int elementoActual = 0;

	// Cargamos la partida
	void Start(){
		
		GameManager.instancia.estadoActual = TiposDeEstado.TUTORIAL;

		if(GameManager.instancia.idiomaActual == 0) { textos = textos_esp; }
		else { textos = textos_eng; }

		cajadetexto.text = textos[elementoActual].text;

	}
		


	// OPCIONES DEL MENU
	public void siguiente(){


		if(elementoActual >= textos.Length-1){

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
			elementoActual++;
			cajadetexto.text = textos[elementoActual].text;
		}

	}





}
