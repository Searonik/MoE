using UnityEngine;
using System.Collections;


public class AnchorControl : MonoBehaviour {

	bool isSelected = false;

	//Our method for reciving a message from an element that it is being replaced.
	void setSelected(bool select){
		isSelected = select;
	}

	/* When an element on the reference pallet has been selected, It sends a message
	 * to this selection pallet to clone it by name.
	 */
	void reply(string targetName){
		/* We do not track which anchor point has been chosen externally, so
		 * a message is broadcast - we simply use a boolean so only the intended
		 * anchor point responds
		 */
		if (isSelected){
			BroadcastMessage("killSelf");						//remove old element
			GameObject element = GameObject.Find(targetName);   //being cloning process
			element =  Instantiate(element) as GameObject;
			element.SendMessage("removeScript");				//add the old pallet rune's script
			element.AddComponent("Element");					//and add the new active element script
			element.SendMessage("setAnchor", name);				//note : clean up code by removing the neew for parent anchors. In learning how to set parents in unity, the need for a reference anchor to know our parent is unnessesary.
			element.transform.parent = transform;
			element.transform.localPosition = new Vector3(0,0,-2);
			element.transform.localScale = new Vector3(1,1,0);	//somehow, when setting the parent, our scale gets set to (0,0,0), no idea why. fixing this issue
			isSelected = false;									//very very bad things will happen if we don't flag ourselves as no longer selected.
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
