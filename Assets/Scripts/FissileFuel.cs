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

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.CompareTag("Neutron")) {
			Destroy(other.gameObject);
			Fission();
		}
	}

	public void Fission() {
		Destroy(gameObject);

		GameObject fission = Instantiate(fissionReactionPrefab,transform.position,Quaternion.identity);
		fission.GetComponent<Particle>().am = am;
	}
}
