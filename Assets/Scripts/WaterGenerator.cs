using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGenerator : Rod {
	public override void Fill(ArenaManager am) {
		float amount = width * height * particles[0].density;

		for (int i = 0; i < amount; i++) {
			GameObject prefab = particles[0].particle;
			GameObject molecule = Instantiate(prefab);
			molecule.transform.SetParent(transform);
			Vector2 chosenLocalPosition = new Vector2(
				Random.Range(-width/2,width/2),
				Random.Range(-height/2,height/2)
			);

			int maxIterations = 10;
			while(Physics2D.Raycast(transform.TransformPoint(chosenLocalPosition),Vector2.zero,0.1f,1 << LayerMask.NameToLayer("Rods")).collider != null) {
				chosenLocalPosition = new Vector2(
					Random.Range(-width/2,width/2),
					Random.Range(-height/2,height/2)
				);
				maxIterations --;
				if(maxIterations < 0) break;
			}

			molecule.transform.localPosition = chosenLocalPosition;

			if(molecule.GetComponent<Particle>() != null)
				molecule.GetComponent<Particle>().am = am;
		}
	}
}
