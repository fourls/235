using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FissionReaction : Particle {
	[Header("Prefabs")]
	public GameObject neutronPrefab;
	[Header("References")]
	public SpriteRenderer sprite;
	public Transform neutronPlaceholder;
	[Header("Values")]
	public int neutronsLeft = 3;
	public Gradient gradient;
	public float colorChangeSpeed;

	private float currentGradientPoint = 0;
	private int gradientMoveDirection = 1;

	private bool isReleasing = false;
	private Vector2 releasingDirection = Vector2.right;

	void Start() {
		am.reactions.Add(this);
	}

	void OnDestroy() {
		am.reactions.Remove(this);
	}

	void Update() {
		currentGradientPoint += colorChangeSpeed * Time.deltaTime * gradientMoveDirection;
		sprite.color = gradient.Evaluate(currentGradientPoint);
		if(currentGradientPoint >= 1) gradientMoveDirection = -1;
		else if (currentGradientPoint <= 0) gradientMoveDirection = 1;
	}

	public GameObject Release() {
		GameObject neutron = Instantiate(neutronPrefab,transform.position,Quaternion.identity);

		neutronsLeft --;
		if(neutronsLeft <= 0)
			Destroy(gameObject);

		return neutron;
	}
}
