using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nucleon : MonoBehaviour {
	public Transform goalPosition;
	public float intensity;

	void FixedUpdate() {
		GetComponent<Rigidbody2D>().velocity = (goalPosition.position - transform.position).normalized * intensity;
	}
}
