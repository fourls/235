using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neutron : MonoBehaviour {
	[Header("References")]
	public Transform arrow;
	[Header("Values")]
	public float rotationSpeed = 5;
	public float baseSpeed = 5;
	public int speedLevel = 4;

	private Rigidbody2D rb;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
	}

	void Update() {
		if(speedLevel < 1) {
			Die();
		}
	}

	void FixedUpdate() {
		float speed = baseSpeed * speedLevel; 
		float vert = Input.GetAxisRaw("Vertical");
		float horiz = Input.GetAxisRaw("Horizontal");

		rb.rotation += horiz * rotationSpeed * -1;

		Vector2 direction = (arrow.transform.position - transform.position).normalized;
		transform.position += (Vector3)direction * speed * Time.deltaTime;
	}

	void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.CompareTag("Bounces")) {
			rb.rotation += 180;
		}
	}

	public void Die() {
		Destroy(gameObject);
	}
}
