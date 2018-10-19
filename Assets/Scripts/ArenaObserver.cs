using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ArenaObserver {
	public ArenaManager arena;
	public ArenaStats stats = new ArenaStats();
	private bool currentlyCounting = false;

	public ArenaObserver(ArenaManager arena) {
		this.arena = arena;
	}

	public void StartCountingTime() {
		currentlyCounting = true;
		stats.timeSpent = 0;
	}

	public void StopCountingTime() {
		currentlyCounting = false;
	}

	public void Update() {
		if(currentlyCounting)
			stats.timeSpent += Time.deltaTime;
	}
}
