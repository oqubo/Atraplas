using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

// --------------------------
// Posibles estados de juego
// --------------------------
public enum TiposDeEstado { INTRO, MENU, JUEGO, SALIR, TUTORIAL, CREDITOS }

// ------------------------
// Clase Game Manager
// ------------------------
public class GameManager : MonoBehaviour {
	
	// ------------------------
	// Atributos estaticos 
	// ------------------------

	//att privado (_instancia)
	static private GameManager _instancia;

	//att publico (instancia) por el que accedemos 
	static public GameManager instancia
	{
		// metodo get
		// se ejecuta al acceder por GameManager.instancia
		get
		{
			// si es la primera vez que accedemos a la instancia del GameManager, 
			// no existira, y la crearemos
			if (_instancia == null)
			{
				// creamos un nuevo objeto llamado "_MiGameManager"
				GameObject go = new GameObject("_MiGameManager");

				// anadimos el script "GameManager" al objeto
				go.AddComponent<GameManager>();

				// guardamos en la instancia el objeto creado
				// debemos guardar el componente ya que _instancia es del tipo GameManager
				_instancia = go.GetComponent<GameManager>();

				// hacemos que el objeto no se elimine al cambiar de escena
				DontDestroyOnLoad(go);
			}

			// devolvemos la instancia
			// si no existia, en este punto ya la habra creado
			return _instancia;
		}

		// metodo set
		// no implementado para no permitir modificar la instancia "GameManager.instancia = x;"
	}


	// Constructor
	// Lo ocultamos el constructor para no poder crear nuevos objetos "sin control"
	protected GameManager(){}





	// ------------------------------------------------------------------------------------------------
	// GESTION DE ESCENAS
	// ------------------------------------------------------------------------------------------------
	public TiposDeEstado estadoActual;
	public float valorTimeScale;

	public void cambiarEscena(string escena) { 
		guardarPartida();
		quitarPausa();
		SceneManager.LoadScene(escena); 
	}

	public void reiniciar(){
		guardarPartida();
		quitarPausa();
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
		
	public void guardarPartida(){
		
//		if(PlayerPrefs.GetInt ("puntosMax") < puntos)
//		{
//			PlayerPrefs.SetInt("puntosMax", puntos);
//		}

		PlayerPrefs.SetInt("puntosMax", PlayerPrefs.GetInt ("puntosMax") + puntos);

	
	}

	public void cargarPartida(){

		if(numpartidas == 0){
			//limpiar variables menos preferencias
			puntos = PlayerPrefs.GetInt("puntosMax");
			idiomaActual = PlayerPrefs.GetInt("idioma");
			tutorialVisto = PlayerPrefs.GetInt("tutorialVisto");
			PlayerPrefs.DeleteAll();
			PlayerPrefs.SetInt("puntosMax",puntos);
			PlayerPrefs.SetInt("idioma",idiomaActual);			
			PlayerPrefs.SetInt("tutorialVisto",tutorialVisto);
		}

	}

	public void guardarPreferencias(){
		PlayerPrefs.SetInt("idioma", idiomaActual);
	}

	public void cargarPreferencias(){
		idiomaActual = PlayerPrefs.GetInt("idioma");
	}



	public void pausar(){
		// si esta activo lo pausamos
		if(Time.timeScale > 0){
			valorTimeScale = Time.timeScale;
			Time.timeScale = 0;
		}
		// sino, quitamos la pausa
		else{
			quitarPausa();
		}
	}
	public void quitarPausa(){
		if(Time.timeScale == 0) Time.timeScale = valorTimeScale;
	}

	public void finDePartida(){
		pausar();
		guardarPartida();
		cambiarEscena("gameover");
		quitarPausa();
	}




	// ------------------------------------------------------------------------------------
	// Atributos
	// ------------------------------------------------------------------------------------

	public int tutorialVisto;
	public int numpartidas;
	public int idiomaActual; //0=ESP, 1=ING
	public int vida;
	public int puntos;
	public int especial;
	public float salto;

	// dificultad
	public int nivel;
	public float tentaculoVelocidad;
	public float espera;
	public float ratioMalos;
	public float velocidadElementosMin;
	public float velocidadElementosMax;
	public float tiempoMancha;

	// estado
	public int tentaculoDireccion; //0=quieto, -1=baja, 1=sube
	public bool estoyRecogiendoAlgo;
	public bool generadorBasuraActivo;
	public GameObject elementoCapturado;

	// estetico
	public float velocidadParallax;
	public float tiempoAvanceRapido;
	public float offsetSuperficie; 
	public float profundidad;
		


	// ------------------------------------------------------------------------------------------------
	// Metodos
	// ------------------------------------------------------------------------------------------------

	public void iniciarPartida(){
		
		vida = 10;
		puntos = 0;
		especial = 3;
		salto = 3f;

		// dificultad
		nivel = 1;
		tentaculoVelocidad = 5f;
		espera = 4f;
		ratioMalos = 50f;
		velocidadElementosMin = 1f;
		velocidadElementosMax = 3f;
		tiempoMancha = 20f;

		// estado
		tentaculoDireccion = 0;
		estoyRecogiendoAlgo = false;
		generadorBasuraActivo = true;
		elementoCapturado = null;

		// estetico
		velocidadParallax = 1f;
		tiempoAvanceRapido = 3f;
		offsetSuperficie = 0.5f; 
		profundidad = 4f;

	}






	public void subirDificultad(){

		// bonus
		sumarVida(1);
		//sumarEspecial();

		// dificultad 10 niveles
		nivel++;
		tentaculoVelocidad += 1f; if(tentaculoVelocidad > 10f) tentaculoVelocidad = 10f;
		espera -= 0.5f; if(espera < 1f) espera = 1f;
		ratioMalos += 5f; if(ratioMalos > 70f) ratioMalos = 70f;
		velocidadElementosMin += 1f; if(velocidadElementosMin > 8f) velocidadElementosMin = 8f;
		velocidadElementosMax += 1f; if(velocidadElementosMax > 10f) velocidadElementosMax = 10f;
	}


	public void sumarPuntos(){
		//puntos += elementoCapturado.GetComponent<InfoElemento>().puntos;
		puntos++;

		GameObject.FindGameObjectWithTag("esponja").GetComponent<Animator>().SetTrigger("contenta");

		GameObject.Find("ControladorDeJuego").GetComponent<Juego>().audiosource.PlayOneShot(
			GameObject.Find("ControladorDeJuego").GetComponent<Juego>().fxacierto);
		
		GameObject.Find("FX_Fireworks").GetComponent<ParticleSystem>().Play();

	}

	public void quitarVida(int i){

		GameObject.FindGameObjectWithTag("esponja").GetComponent<Animator>().SetTrigger("triste");

		GameObject.Find("ControladorDeJuego").GetComponent<Juego>().audiosource.PlayOneShot(
			GameObject.Find("ControladorDeJuego").GetComponent<Juego>().fxerror);

		GameObject.Find("FX_Dirt").GetComponent<ParticleSystem>().Play();

		GameObject.FindGameObjectWithTag("pulpo").GetComponent<Animator>().SetTrigger("danio");

		// quitar vida
		vida -= i;
		if(vida < 1) finDePartida();
	}

	public void sumarVida(int i){
		GameObject.Find("FX_FireworksVida").GetComponent<ParticleSystem>().Play();

		if(vida + i > 10) vida = 10;
		else vida += i;
	}

	public void generarMancha(){
		if(GameObject.FindGameObjectWithTag("generadorManchas")){
			GameObject.FindGameObjectWithTag("generadorManchas").GetComponent<generadorManchas>().generarMancha();
		}
	}


	public void sumarEspecial(){
		if(especial < 5) especial++;
	}

	public void usarEspecial(){

		if(especial > 0){
			especial--;

		}
	}




}