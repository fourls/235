using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class DirectorController : MonoBehaviour {
	public static DirectorController ins = null;
	private PlayableDirector director;

	void Awake() {
		if(ins == null) ins = this;
		else if (ins != this) Destroy(gameObject);
		director = GetComponent<PlayableDirector>();
	}

	public void Play(TimelineAsset timeline) {
		director.time = 0;
		director.Play(timeline);
	}
}
