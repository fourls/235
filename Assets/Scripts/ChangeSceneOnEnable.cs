using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnEnable : MonoBehaviour {
	public string sceneName;
	public float delay = 0f;

	void OnEnable() {
		Invoke("Load",delay);
	}

	void Load() {
		SceneManager.LoadScene(sceneName);
	}
}
