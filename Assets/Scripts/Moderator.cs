using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moderator : MonoBehaviour {
	public float angularVelocityOnHit = 480;

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.CompareTag("Neutron")) {
			GameManager.ins.arena.observer.stats.moderatorsHit ++;
			GetComponent<Rigidbody2D>().angularVelocity = angularVelocityOnHit * other.gameObject.GetComponent<Neutron>().speedLevel;
			if(Random.value > 0.5)
				GetComponent<Rigidbody2D>().angularVelocity *= -1;
			other.gameObject.GetComponent<Neutron>().speedLevel --;
		}
	}
}
