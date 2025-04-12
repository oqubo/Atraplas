using UnityEngine;
using System.Collections;

public class generadorManchas : MonoBehaviour {

	public GameObject[] manchas;
	public GameObject manchaIzq, manchaDer;
	public float separacion;

	private bool manchaActiva;
	int i, j;
	Vector3 posicion;



	void Start () {

		manchaIzq = null;
		manchaDer = null;
		manchaActiva = false;
		i = 0;
		j = 0;
		separacion = 4f;

	}
	

	void Update () {


	
	}
		




	public void generarMancha(){

		if(!manchaActiva) StartCoroutine(corrutinaCrearMancha());

	}



	IEnumerator corrutinaCrearMancha()
	{

		// genero mancha
		manchaActiva = true;

		i = Random.Range(0, manchas.Length);
		do{
			j = Random.Range(0, manchas.Length);
		}while(i == j);


		posicion = new Vector3(transform.position.x - separacion, transform.position.y, manchas[i].transform.position.z); 
		manchaIzq = (GameObject) Instantiate(manchas[i], posicion, Quaternion.identity);

		posicion = new Vector3(transform.position.x + separacion, transform.position.y, manchas[j].transform.position.z); 
		manchaDer = (GameObject) Instantiate(manchas[j], posicion, Quaternion.identity);

		// espero
		yield return new WaitForSeconds(GameManager.instancia.tiempoMancha);

		// quitar mancha
		if(manchaActiva) borrarMancha();

	}




	public void borrarMancha(){

		if(manchaIzq != null){
			Destroy(manchaIzq);
			manchaIzq = null;
		}
		if(manchaDer != null){
			Destroy(manchaDer);
			manchaDer = null;
		}
		manchaActiva = false;

	}



}