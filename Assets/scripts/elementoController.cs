using UnityEngine;
using System.Collections;

public class elementoController : MonoBehaviour {

	public float speed;
	public bool capturado;
	private Vector2 newPosition;  


	void Start () {
		capturado = false;

		speed = Random.Range(GameManager.instancia.velocidadElementosMin, GameManager.instancia.velocidadElementosMax);
		newPosition = transform.position;

	}


	void Update () {

		// si no esta capturado se mueve para la izquierda
		if(!capturado){
			newPosition.x = transform.position.x - Time.deltaTime * speed;
			transform.position = newPosition;   
		}

		// si esta capturado sube con el tentaculo
		else if(capturado){
			newPosition.y = transform.position.y + Time.deltaTime * GameManager.instancia.tentaculoVelocidad * 3;
			transform.position = newPosition;  
		}


	}


	void OnTriggerEnter2D(Collider2D c) {

		// si el elemento sale de la pantalla
		if (c.CompareTag("fin")){

			Destroy(this.gameObject);

			if(this.tag == "basura"){
				GameManager.instancia.quitarVida(2);
			}

		}

	}




}

