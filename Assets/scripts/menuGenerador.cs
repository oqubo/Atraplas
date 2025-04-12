using UnityEngine;
using System.Collections;

public class menuGenerador : MonoBehaviour {

	public GameObject[] objs;
	public GameObject burbuja;
	private int i;
	private Vector2 posicion;

	void Start () {

		InvokeRepeating("generar", 0f, 6f);

	
	}

	public void generar(){

		// pez
		i = Random.Range(0, objs.Length);
		posicion = new Vector2(transform.position.x,
			Random.Range(transform.position.y - GameManager.instancia.profundidad,
				transform.position.y + GameManager.instancia.profundidad/2));
		Instantiate(objs[i], posicion, Quaternion.identity);



		// burbujas

		posicion = new Vector2(transform.position.x,
			Random.Range(transform.position.y - GameManager.instancia.profundidad,
				transform.position.y + GameManager.instancia.profundidad/2));
		Instantiate(burbuja, posicion, Quaternion.identity);


		posicion = new Vector2(transform.position.x,
			Random.Range(transform.position.y - GameManager.instancia.profundidad,
				transform.position.y + GameManager.instancia.profundidad/2));
		Instantiate(burbuja, posicion, Quaternion.identity);


	}

}
