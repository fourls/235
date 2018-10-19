using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathDataTransfer : MonoBehaviour {
	public static DeathDataTransfer ins = null;

	public ArenaStats stats = null;

	void Awake() {
	}

	void OnEnable() {
		if(ins == null) ins = this;
		else if (ins != this) Destroy(gameObject);

		DontDestroyOnLoad(gameObject);

		stats = GameManager.ins.lastArena.observer.stats;
		SceneManager.LoadScene("End");
	}
}
