using UnityEngine;
using System.Collections;

public class RuneSelection : MonoBehaviour {


	float heldTime;
	bool mouseDown = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (mouseDown){
			//This tracks the time as the mouse is held down.
			heldTime += Time.deltaTime;
			/*If the mouse has been held down for long enough, send the signal 
			 * that an element has been chosen
			 */
			if (heldTime > 2f){
				GameObject pallet = GameObject.Find("ElementSelectionPallet");
				//for now, we move the pallet here. Later work can change this
				pallet.transform.Translate(new Vector3(8,0,0));		
				GameObject castingPallet = GameObject.Find("CastingPallet");
				/* Here we send a message to the selection pallet
				 * This message contains the name of the element to clone. 
				 * The Anchor on the selection pallet that is marked will clone
				 * This element and replace the old element with a copy of the 
				 * newly selected element.
				 */
				castingPallet.BroadcastMessage("reply", name);
				mouseDown = false;
			}
		}
	}

	//Nessesary evil to clear this script off when cloning for use on the selection pallet
	void removeScript(){
		Destroy(this);
	}

	void OnMouseDown(){
		//Flag that we have been clicked on.
		heldTime = 0f;
		mouseDown = true;
	}

	void OnMouseUp(){
		//Flag that the mouse has been let go of.
		mouseDown = false;

	}
}
