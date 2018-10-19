using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlElement : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.CompareTag("Neutron")) {
			GameManager.ins.arena.observer.stats.controlsHit ++;
			other.gameObject.GetComponent<Neutron>().Die();
		}
	}
}
