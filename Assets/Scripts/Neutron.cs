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
	public float startMultiplier = 0;

	private Rigidbody2D rb;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
		rb.rotation = Random.Range(0f,360f);
	}

	void Update() {
		if(speedLevel < 1) {
			speedLevel = 1;
			Die();
		}
	}

	void FixedUpdate() {
		float speed = baseSpeed * speedLevel * startMultiplier; 
		float vert = Input.GetAxisRaw("Vertical");
		float horiz = Input.GetAxisRaw("Horizontal");

		rb.rotation += horiz * rotationSpeed * -1;

		Vector2 direction = (arrow.transform.position - transform.position).normalized;
		transform.position += (Vector3)direction * speed * Time.deltaTime;
	}

	void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.CompareTag("Bounces")) {
			Bounce(other.GetContact(0).normal);
		}
	}

	public void StartMovingIn(float seconds) {
		Invoke("StartMoving",seconds);
	}

	void StartMoving() {
		startMultiplier = 1f;
	}

	public void Bounce(Vector2 n) {
		Vector2 d = (arrow.transform.position - transform.position).normalized;
		Vector2 r = d - 2 * Vector2.Dot(d,n) * n;
		rb.rotation = Vector2.SignedAngle(Vector2.right,r);
	}

	public void Die() {
		Destroy(gameObject);
	}
}
