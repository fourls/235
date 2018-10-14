using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rod : MonoBehaviour {
	public List<ParticlesWithDensity> particles;
	public float width = 10;
	public float height = 100;

	public virtual void Fill(ArenaManager am) {
		foreach(ParticlesWithDensity pData in particles) {
			float amount = width * height * pData.density;

			for (int i = 0; i < amount; i++) {
				GameObject prefab = pData.particle;
				GameObject particle = Instantiate(prefab);
				
				if(particle.GetComponent<Particle>() != null)
					particle.GetComponent<Particle>().am = am;

				particle.transform.SetParent(transform);
				particle.transform.localPosition = new Vector2(
					Random.Range(-width/2,width/2),
					Random.Range(-height/2,height/2)
				);
			}
		}
	}
}

[System.Serializable]
public class ParticlesWithDensity {
	public GameObject particle;
	public float density = 0.1f;
}