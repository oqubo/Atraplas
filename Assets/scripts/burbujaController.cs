using UnityEngine;
using System.Collections;

public class burbujaController : MonoBehaviour {

	public float speed;
	public float escala;
	private Vector2 newPosition;    


	void Start () {
		newPosition = transform.position;

		speed = Random.Range(GameManager.instancia.velocidadParallax, GameManager.instancia.tiempoAvanceRapido);

		escala = Random.Range( transform.localScale.x * 0.2f, transform.localScale.x);
		transform.localScale = new Vector2 (escala, escala);
	}


	void Update () {

		newPosition.x = transform.position.x - Time.deltaTime * speed;
		transform.position = newPosition;   

	}


	void OnTriggerEnter2D(Collider2D c) {

		// si el elemento sale de la pantalla
		if (c.CompareTag("fin")){

			Destroy(this.gameObject);

		}

	}




}

