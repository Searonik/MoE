using UnityEngine;
using System.Collections;

public class Element : MonoBehaviour {

	bool overAnchor = false;
	Vector3 oldMousePos;
	public GameObject anchor;
	GameObject newAnchor;

	// Use this for initialization
	void Start () {
		transform.position = anchor.transform.position + new Vector3(0,0,-1);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown(){
	
	}

	void OnMouseDrag(){
		transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
	}
	void OnMouseUp(){
		//Switch object to new anchor if nessesary, then set it to it's resting anchor
		if (overAnchor){
			GameObject copy = Instantiate(this.gameObject) as GameObject;
			//copy.GetComponent<ActionScript>();
			copy.transform.position = newAnchor.transform.position + new Vector3(0,0,-1);
		}
			transform.position = anchor.transform.position + new Vector3(0,0,-1);
	}

	//Collision detection methods
	void OnTriggerEnter2D(Collider2D col){
		Debug.Log("maybe");
		if (col.gameObject.tag == "spell"){
			Debug.Log("yes");
			overAnchor = true;
			newAnchor = col.gameObject;
		}

	}
	void OnTriggerStay2D(Collider2D col){
		Debug.Log("Still there");
	}
	void OnTriggerExit2D(Collider2D col){
		Debug.Log("Gone");
		overAnchor = false;
		newAnchor = null;
	}
}
