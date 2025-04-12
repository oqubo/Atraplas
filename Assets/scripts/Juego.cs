using UnityEngine;
using UnityEngine.UI;
using System.Collections;



public class Juego : MonoBehaviour {


	public Text valorPuntos;
	public Slider valorVida, valorEspecial;
	private bool dia;

	public GameObject movGiratorio;
	Animator animatorGiratorio;
	private bool corrutinaDeAvanceRapido;


	public AudioClip fxperla, fxsalto, fxpulpo, fxlimpiar, fxacierto, fxerror;
	public AudioSource audiosource;


	void Start () {

		dia = true;

		GameManager.instancia.iniciarPartida();

		animatorGiratorio = movGiratorio.GetComponent<Animator>();
		corrutinaDeAvanceRapido = false;
		InvokeRepeating("cambioDiaNoche", 50f, 50f); 

		audiosource = gameObject.GetComponent<AudioSource> ();

	}


	void Update () {

		verInput();
		actualizarInterfaz();

	}


	public void mover(){
		if(GameManager.instancia.tentaculoDireccion == 0){
			GameManager.instancia.tentaculoDireccion = -1;
			audiosource.PlayOneShot (fxpulpo);
		}
		else if(GameManager.instancia.tentaculoDireccion == 1
			&& !GameManager.instancia.estoyRecogiendoAlgo){
			GameManager.instancia.tentaculoDireccion = -1;
		}
	}



	public void verInput(){

		if (Input.GetMouseButtonDown(0))
		{
			Vector2 posicionRaton = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(posicionRaton, Vector2.zero);



			if (hit.collider != null) {

				if(hit.collider.tag == "pulpo"){
					mover();
				}

				else if(hit.collider.tag == "especial"){
					GameManager.instancia.sumarEspecial();
					Destroy(hit.transform.gameObject);
					audiosource.PlayOneShot (fxperla);
				}

				else if(hit.collider.tag == "pez" || hit.collider.tag == "basura"){
					hit.transform.gameObject.transform.position = 
						new Vector2(hit.transform.gameObject.transform.position.x - GameManager.instancia.salto, 
							hit.transform.gameObject.transform.position.y);
					audiosource.PlayOneShot (fxsalto);
				}

				else if(hit.collider.tag == "esponja" ){
					if(GameManager.instancia.especial > 0){
						// ejecutar animacion
						hit.collider.gameObject.GetComponent<Animator>().SetTrigger("limpiar");
						// eliminar manchas
						GameManager.instancia.usarEspecial();
						audiosource.PlayOneShot (fxlimpiar);
					}
				}

			}
		}
			

	}


	public void actualizarInterfaz(){
		valorPuntos.text = GameManager.instancia.puntos.ToString();
		valorVida.value = GameManager.instancia.vida;
		valorEspecial.value = GameManager.instancia.especial;
	}


	//---------------------------------
	// CAMBIOS DE DIA/NOCHE
	//---------------------------------

	public void cambioDiaNoche(){

		//ejecutar animacion
		animatorGiratorio.SetTrigger("cambia");
		if(!corrutinaDeAvanceRapido) StartCoroutine(avanceRapido(GameManager.instancia.tiempoAvanceRapido));

		GameManager.instancia.subirDificultad();

		// cambio dia, noche, dia, etc
		dia = !dia;

	}



	IEnumerator avanceRapido(float seconds)
	{
		corrutinaDeAvanceRapido = true;
		GameManager.instancia.generadorBasuraActivo = false;

		// eliminar todos los objetos
		/*
		GameObject[] objs = GameObject.FindGameObjectsWithTag("basura");
		foreach (var item in objs) { Destroy(item); }
		objs = GameObject.FindGameObjectsWithTag("pez");
		foreach (var item in objs) { Destroy(item); }
		objs = GameObject.FindGameObjectsWithTag("especial");
		foreach (var item in objs) { Destroy(item); }
		*/

		// subir velocidad
		float velocidad = GameManager.instancia.velocidadParallax;
		GameManager.instancia.velocidadParallax = 50f; 

		// esperar
		yield return new WaitForSeconds(seconds);

		// y luego volver
		GameManager.instancia.velocidadParallax = velocidad;

		GameManager.instancia.generadorBasuraActivo = true;
		corrutinaDeAvanceRapido = false;
	}









	// OPCIONES DEL MENU
	public void OpcionReiniciar(){
		GameManager.instancia.reiniciar();
	}

	public void OpcionPausa(){
		GameManager.instancia.pausar();
	}

	public void OpcionMenu(){
		GameManager.instancia.estadoActual = TiposDeEstado.MENU;
		GameManager.instancia.cambiarEscena("menu"); //este metodo del gamemanager ya guarda la partida
	}

}
