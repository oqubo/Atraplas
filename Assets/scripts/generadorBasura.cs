using UnityEngine;
using System.Collections;

public class generadorBasura : MonoBehaviour {

	public GameObject[] elementosBuenos;
	public GameObject[] elementosMalos;
	public GameObject[] elementosBuenosEspeciales;
	public GameObject[] elementosMalosEspeciales;
	public GameObject burbuja;

	private bool corrutinaLanzada;


	void Start () {

		corrutinaLanzada = false;
	}
	

	void Update () {

		if(!corrutinaLanzada) StartCoroutine(basureador());
	
	}
		

	IEnumerator basureador()
	{
		corrutinaLanzada = true;
		yield return new WaitForSeconds(GameManager.instancia.espera);
		generarObjeto();
		corrutinaLanzada = false;
	}


	/*
	 * Genera objetos
	 * 5% especiales buenos
	 * 5% especiales malos
	 * ratioMalos % de objetos malos
	 * el restante de buenos
	 * */
	void generarObjeto(){

		// si no esta activo no genero nada
		if(!GameManager.instancia.generadorBasuraActivo) return;

		// generar elemento bueno o malo segun el ratio
		GameObject elemento = null;
		int aleatorio = Random.Range(0, 100);

	
		//5% de especiales buenos
		if(aleatorio >= 0 && aleatorio < 5){
			elemento = elementosBuenosEspeciales[Random.Range(0, elementosBuenosEspeciales.Length)];
		}
		//5% de especiales malos
		else if(aleatorio >= 5 && aleatorio < 10){
			elemento = elementosMalosEspeciales[Random.Range(0, elementosMalosEspeciales.Length)];
		}
		//ratiomalos% de obj malos
		else if(aleatorio >= 10 && aleatorio < 10 + GameManager.instancia.ratioMalos){
			elemento = elementosMalos[Random.Range(0, elementosMalos.Length)];
		}
		//en caso contrario genero buenos
		else{
			elemento = elementosBuenos[Random.Range(0, elementosBuenos.Length)];
		}



		// posicion e instanciar
		Vector2 posicion = new Vector2(transform.position.x,
			Random.Range(transform.position.y - GameManager.instancia.profundidad,
				transform.position.y - GameManager.instancia.offsetSuperficie));
		Instantiate(elemento, posicion, Quaternion.identity);



		// a cada elemento genero tambien burbujas
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