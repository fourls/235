using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Timeline;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour {
	public TimelineAsset winTimeline;
	public TimelineAsset lossTimeline;
	[Header("UI Elements")]
	// public float timeSpent = 0;
	// public int neutronsUsed = 0;
	// public int fuelsFissioned = 0;
	// public int moderatorsHit = 0;
	// public int controlsHit = 0;
	public TextMeshPro mevCreatedText;
	public TextMeshPro timeSpentText;
	public TextMeshPro neutronsUsedText;
	public TextMeshPro fuelsFissionedText;
	public TextMeshPro moderatorsHitText;
	public TextMeshPro controlsHitText;

	void Start() {
		if(DeathDataTransfer.ins == null)
			SceneManager.LoadScene("Start");
		else {
			FillOutInfo(DeathDataTransfer.ins.stats);
		}
	}

	void FillOutInfo(ArenaStats stats) {
		int mevCreated = 210 * stats.fuelsFissioned;
		mevCreatedText.text = mevCreated.ToString() + " MeV";

		timeSpentText.text = Mathf.Ceil(stats.timeSpent).ToString() + " seconds";

		neutronsUsedText.text = stats.neutronsUsed.ToString();

		fuelsFissionedText.text = stats.fuelsFissioned.ToString();

		moderatorsHitText.text = stats.moderatorsHit.ToString();

		controlsHitText.text = stats.controlsHit.ToString();

		if(stats.win) {
			DirectorController.ins.Play(winTimeline);
		} else {
			DirectorController.ins.Play(lossTimeline);
		}
	}
}
