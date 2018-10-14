using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomElementMovement : MonoBehaviour {
	public float radius = 1f;
	public float lerpTime = 5f;
	public float timeInBetweenTargetChanges = 1f/60f;
	public float randomness = 0.1f;

	private Vector3 originalPosition;
	private Vector3 wantedPosition;

	void Start() {
		originalPosition = transform.position;
		wantedPosition = originalPosition;
		StartCoroutine(Jiggle());
	}

	void FixedUpdate() {
		transform.position = Vector2.Lerp(transform.position,wantedPosition,lerpTime * Time.deltaTime);
	}

	IEnumerator Jiggle() {
		while(true) {
			wantedPosition = originalPosition + (Vector3)(Random.insideUnitCircle * radius);

			yield return new WaitForSeconds(timeInBetweenTargetChanges + Random.Range(-randomness,randomness));
		}
	}
}
