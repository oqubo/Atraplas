using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {


	public GameObject referenciaTamanio;
	public float scrollSpeed;
	public float tileSize;

	public float debugSuma, debugStartPos;

	private Vector2 startPosition, newPosition;

	void Start ()
	{
		tileSize = referenciaTamanio.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
		
		startPosition = transform.position;
		newPosition = transform.position;

	}

	void Update ()
	{
		scrollSpeed = GameManager.instancia.velocidadParallax;
		newPosition.x = transform.position.x - Time.deltaTime * scrollSpeed;
		transform.position = newPosition;

		if((transform.position.x + tileSize) < startPosition.x) {
			Vector2 newPos = transform.position;
			newPos.x += 2.0f * tileSize; 
			transform.position = newPos;
		}
	}
}