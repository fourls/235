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
		am.observer.stats.fuelsFissioned ++;
	}

	void OnDestroy() {
		if(am.reactions.Contains(this))
			am.reactions.Remove(this);
	}

	void Update() {
		currentGradientPoint += colorChangeSpeed * Time.deltaTime * gradientMoveDirection;
		sprite.color = gradient.Evaluate(currentGradientPoint);
		if(currentGradientPoint >= 1) gradientMoveDirection = -1;
		else if (currentGradientPoint <= 0) gradientMoveDirection = 1;
	}

	public GameObject Release() {
		GetComponent<Animator>().SetTrigger("Release");

		GameObject neutron = Instantiate(neutronPrefab,transform.position,Quaternion.identity);
		neutron.GetComponent<Neutron>().StartMovingIn(0.5f);

		neutronsLeft --;
		if(neutronsLeft <= 0) {
			GetComponent<Animator>().SetTrigger("Destroy");
			am.reactions.Remove(this);
		}

		return neutron;
	}

	public void Destroy() {
		Destroy(gameObject);
	}
}
