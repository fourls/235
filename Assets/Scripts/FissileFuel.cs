using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FissileFuel : Particle {
	public GameObject fissionReactionPrefab;

	void Start() {
		am.unreactedFuel.Add(this);
	}

	void OnDestroy() {
		am.unreactedFuel.Remove(this);
	}

	void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.CompareTag("Neutron")) {
			Neutron neutron = other.gameObject.GetComponent<Neutron>();
			float chance = 1f/(float)neutron.speedLevel;
			if(chance >= Random.value) {
				Destroy(other.gameObject);
				Fission();
			} else {
				neutron.Bounce(other.GetContact(0).normal);
			}
		}
	}

	public void Fission() {
		Destroy(gameObject);

		GameObject fission = Instantiate(fissionReactionPrefab,transform.position,Quaternion.identity);
		fission.GetComponent<Particle>().am = am;
	}
}
