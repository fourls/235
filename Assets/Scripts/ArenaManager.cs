using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class ArenaManager : MonoBehaviour {
	[Header("Prefabs")]
	public GameObject neutronPrefab;
	[Header("References")]
	public Transform cameraTarget;
	public Transform neutronStart;
	public TimelineAsset onEnd;
	[Header("Values")]
	public bool useMovingCamera = false;
	public float delayBeforeStart = 0.5f;
	public WinCondition winCondition;
	public bool allowsRespawn = true;
	public List<Rod> rods;
	public int startingSpeedLevel = 8;

	public ArenaObserver observer;



	[HideInInspector]
	public List<FissileFuel> unreactedFuel = new List<FissileFuel>();
	[HideInInspector]
	public List<FissionReaction> reactions = new List<FissionReaction>();
	[HideInInspector]
	public Neutron neutron;
	// [HideInInspector]
	public bool isComplete = false;

	public StateMachine<ArenaManager> stateMachine;

	void Awake() {
		stateMachine = new StateMachine<ArenaManager>(this);
	}

	void OnEnable() {
		if(GameManager.ins.arena == null)
			GameManager.ins.arena = this;
	}

	void Update() {
		stateMachine.Update();
		observer.Update();
	}


	void FixedUpdate() {
		if(neutron != null && cameraTarget != null) cameraTarget.position = neutron.transform.position;
	}

	void WinGame() {
		Time.timeScale = 0;
		Debug.Log("You won! :D");
	}

	void LoseGame() {
		Time.timeScale = 0;
		Debug.Log("You lost :(");
	}

	// --------------------------------------------------------------------------
	public class SetupState : State<ArenaManager> {
		public override void OnEnter() {
			foreach(Rod rod in owner.rods) {
				rod.Fill(owner);
			}
			owner.StartCoroutine(StartGameAfterDelay());
		}

		IEnumerator StartGameAfterDelay() {
			yield return null;
			yield return new WaitForSeconds(owner.delayBeforeStart);
			owner.stateMachine.Next(new GameState());
		}
	}
	
	// --------------------------------------------------------------------------
	public class GameState : State<ArenaManager> {
		public override void OnEnter() {
			owner.observer.StartCountingTime();
			ReleaseNewNeutron(true);
			owner.neutron.speedLevel = owner.startingSpeedLevel;
		}
		public override void OnStay() {
			if(owner.winCondition == WinCondition.NoUnreactedFuels && owner.unreactedFuel.Count == 0) {
				ExitState(true);
				return;
			}

			if(owner.neutron == null) {
				if(owner.winCondition == WinCondition.FirstDeath) {
					ExitState(true);
				} else {
					ReleaseNewNeutron();
				}
			}
		}

		void ReleaseNewNeutron(bool first=false) {
			owner.observer.stats.neutronsUsed ++;
			if(owner.reactions.Count > 0)
				owner.neutron = owner.reactions[0].Release().GetComponent<Neutron>();
			else if(owner.neutronStart != null && (owner.allowsRespawn || first)) {
				owner.neutron = Instantiate(owner.neutronPrefab,owner.neutronStart.position,Quaternion.identity).GetComponent<Neutron>();
				owner.neutron.StartMovingIn(0.5f);
			} else {
				owner.observer.stats.neutronsUsed --;
				ExitState(false);
			}
		}

		void ExitState(bool win) {
			owner.observer.StopCountingTime();
			owner.observer.stats.win = win;
			owner.isComplete = true;
			owner.stateMachine.Next(null);
		}
	}
	// --------------------------------------------------------------------------
	// public class GameState : State<ArenaManager> {
	// 	public override void OnStay() {
	// 		if(owner.neutron == null) {
	// 			ReleaseNewNeutron();
	// 		}

	// 		if(owner.unreactedFuel.Count == 0) {
	// 			owner.isComplete = true;
	// 			owner.stateMachine.Next(null);
	// 		}
	// 	}

	// 	void ReleaseNewNeutron() {
	// 		if(owner.reactions.Count == 0) {
	// 			owner.isComplete = true;
	// 			owner.stateMachine.Next(null);
	// 			return;
	// 		}

	// 		owner.neutron = owner.reactions[0].Release().GetComponent<Neutron>();
	// 	}
	// }
}

public enum WinCondition {
	NoUnreactedFuels,
	FirstDeath
}