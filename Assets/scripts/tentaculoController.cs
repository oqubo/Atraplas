using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class tentaculoController : MonoBehaviour {

	private Vector2 originalPosition, newPosition; 

	[Header("PANEL INFORMATIVO")]
	public Canvas panel;
	public Image imagen;
	public Text nombre, textoaniosdegeneracion, aniosdegeneracion;



	void Start () {
		originalPosition = transform.position;
		newPosition = transform.position;
	}
	


	void Update () {

		// direccion 0=quito, -1=abajo, 1=arriba

		// mover abajo
		if(GameManager.instancia.tentaculoDireccion == -1){
			newPosition.y = transform.position.y - Time.deltaTime * GameManager.instancia.tentaculoVelocidad;
			transform.position = newPosition;  
		}

		// mover arriba
		if(GameManager.instancia.tentaculoDireccion == 1 &&
			transform.position.y < originalPosition.y){
			newPosition.y = transform.position.y + Time.deltaTime * GameManager.instancia.tentaculoVelocidad * 3;
			transform.position = newPosition;  
		}

		// llego al tope
		if(transform.position.y >= originalPosition.y){
			GameManager.instancia.tentaculoDireccion = 0;
			transform.position = originalPosition;
			if(GameManager.instancia.estoyRecogiendoAlgo){
				Destroy(GameManager.instancia.elementoCapturado);
				GameManager.instancia.estoyRecogiendoAlgo = false;
			}
		}

	
	}

	void OnTriggerEnter2D(Collider2D c) {

		// si toco suelo recojo
		if (c.CompareTag("suelo")){
			GameManager.instancia.tentaculoDireccion = 1;
		}
			

		// si toco un objeto lo recojo
		else if ((c.CompareTag("basura") || c.CompareTag("pez")) && !GameManager.instancia.estoyRecogiendoAlgo){

			// recojo el tentaculo
			GameManager.instancia.tentaculoDireccion = 1;			
			GameManager.instancia.estoyRecogiendoAlgo = true;

			// tengo que subir el objeto a la vez
			GameManager.instancia.elementoCapturado = c.gameObject;
			c.gameObject.GetComponent<elementoController>().capturado = true;

			if(c.CompareTag("basura")){
				GameManager.instancia.sumarPuntos();



				if(PlayerPrefs.GetInt(c.name) != 1){
					mostrarFichaInformativa();
					PlayerPrefs.SetInt(c.name, 1);
				}
			}

			else if(c.CompareTag("pez")){
				GameManager.instancia.generarMancha();
				GameManager.instancia.quitarVida(1);

			}
				
		}
			
	}


	public void mostrarFichaInformativa(){

		// pausar juego
		GameManager.instancia.pausar();

		// rellenar datos
		imagen.sprite = GameManager.instancia.elementoCapturado.GetComponent<SpriteRenderer>().sprite;

		InfoElemento elemento = GameManager.instancia.elementoCapturado.GetComponent<InfoElemento>();

		if(GameManager.instancia.idiomaActual == 0){
			nombre.text = elemento.nombreesp;
			textoaniosdegeneracion.text = "años para biodegradarse";
		}

		else if(GameManager.instancia.idiomaActual == 1){
			nombre.text = elemento.nombreeng;
			textoaniosdegeneracion.text = "years for biodegradation";
		}

		aniosdegeneracion.text = elemento.tiempodegeneracion;

		// mostrar
		panel.gameObject.SetActive(true);
	}



	public void ocultarFichaInformativa(){
		
		// quitar pausa
		GameManager.instancia.quitarPausa();

		// ocultar formulario
		GameObject.FindGameObjectWithTag("panelinformativo").SetActive(false);
	}

}
