using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
	public static AudioManager ins = null;
	public AudioSource soundtrack;
	public AudioSource sfx;

	void Awake() {
		if(ins == null)
			ins = this;
		else if (ins != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
	}
}
