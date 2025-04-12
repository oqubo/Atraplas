using UnityEngine;
using System.Collections;

public class esponjaController : MonoBehaviour {
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void limpiaManchas(){

		if(GameObject.FindGameObjectWithTag("generadorManchas")){
			GameObject.FindGameObjectWithTag("generadorManchas").GetComponent<generadorManchas>().borrarMancha();
		}
	}
}
