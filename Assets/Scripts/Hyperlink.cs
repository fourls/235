using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Hyperlink : MonoBehaviour {
	public TimelineAsset timeline;
	private bool used = false;
	
	void OnMouseDown() {
		// if(!used)
		DirectorController.ins.Play(timeline);
		
		used = true;
		GetComponent<SpriteRenderer>().color = new Color(1,1,1,0.5f);
	}
}
