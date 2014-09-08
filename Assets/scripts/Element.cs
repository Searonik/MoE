using UnityEngine;
using System.Collections;

public class Element : MonoBehaviour {

	bool overAnchor = false;
	Vector3 oldMousePos;
	public GameObject anchor;
	GameObject newAnchor;
	float heldTime;
	bool mouseDown = false;

	// Use this for initialization
	void Start () {
		transform.position = anchor.transform.position + new Vector3(0,0,-1);
	}
	
	// Update is called once per frame
	void Update () {
		if (mouseDown){
			/* this amounts to a timer, accumulating only when the mouse is initally 
			 * held over it's intial position. 
			 */
			heldTime += Time.deltaTime;
			/* If it is held for 2 seconds, we call up the reference pallets and 
			 * select an element to replace it.
			 */
			if (heldTime > 2f){
				// As a temporary measure, we move the pallet ourselves.
				GameObject pallet = GameObject.Find("ElementSelectionPallet");
				pallet.transform.Translate(new Vector3(-8,0,0));
				anchor.SendMessage("setSelected", true);
				mouseDown = false;
			}
		}
	}

	//This is our anchor setter for scripts cloning elements around
	void setAnchor(string target){
		anchor = GameObject.Find(target);

	}

	//This method is used to remove children objects of anchors
	void killSelf(){
		Destroy(this.gameObject);
	}



	void OnMouseDown(){
		/*Here, we detect our elmenet has been clicked on. We reset the timer for 
		 * click-and-hold functionality, and track that the mouse is held down		 * 
		 */
		heldTime = 0f;
		mouseDown = true;
	}
	

	void OnMouseDrag(){
		/*  Here, we are simply dragging the image around with the input.
		 */
		//This line snaps the image to the location of where input is occuring
		transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
		if (mouseDown){
			/* This line detects if we have moved away from the image's position.
			 * If we moved the image away from it's anchor point, we flag that it
			 * has been moved, which stops update() from adding to the timer. 
			 */
			if (Vector3.Distance(transform.position, anchor.transform.position) > 1){
				mouseDown=false;
			}
		}
	}
	void OnMouseUp(){
		mouseDown = false;
		//Switch object to new anchor if nessesary, then set it to it's resting anchor
		if (overAnchor){
			//First, clear out the element on this anchor, if any
			newAnchor.BroadcastMessage("killSelf");
			//Now clone this element and attach it to the new anchor
			GameObject copy = Instantiate(this.gameObject) as GameObject;
			copy.transform.parent = newAnchor.transform;
			copy.SendMessage("setAnchor", newAnchor.name);
		}
		transform.position = anchor.transform.position;
	}

	//Collision detection methods
	void OnTriggerEnter2D(Collider2D col){
		//if we have collided with an spell anchor, then take note of which object
		if (col.gameObject.tag == "spell"){
			overAnchor = true;
			newAnchor = col.gameObject;
		}

	}
	void OnTriggerStay2D(Collider2D col){
		//Debug.Log("Still there");
	}
	void OnTriggerExit2D(Collider2D col){
		//if we leave the anchor, we clear out our stored information on what object was collided with - we don't want to snap to them.
		overAnchor = false;
		newAnchor = null;
	}
}
