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
	public List<Rod> rods;
	public bool tutorial = false;



	[HideInInspector]
	public List<FissileFuel> unreactedFuel = new List<FissileFuel>();
	[HideInInspector]
	public List<FissionReaction> reactions = new List<FissionReaction>();
	[HideInInspector]
	public Neutron neutron;
	// [HideInInspector]
	public CompletionStatus completeState = CompletionStatus.Incomplete;

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
		}

		public override void OnStay() {
			if(owner.tutorial)
				owner.stateMachine.Next(new TutorialState());
			else
				owner.stateMachine.Next(new GameState());
		}
	}
	
	// --------------------------------------------------------------------------
	public class TutorialState : State<ArenaManager> {
		public override void OnEnter() {
			ReleaseNewNeutron();
		}
		public override void OnStay() {
			if(owner.unreactedFuel.Count == 0) {
				owner.completeState = CompletionStatus.Success;
				owner.stateMachine.Next(null);
				return;
			}

			if(owner.neutron == null) {
				ReleaseNewNeutron();
			}
		}

		void ReleaseNewNeutron() {
			if(owner.reactions.Count > 0)
				owner.neutron = owner.reactions[0].Release().GetComponent<Neutron>();
			else
				owner.neutron = Instantiate(owner.neutronPrefab,owner.neutronStart.position,Quaternion.identity).GetComponent<Neutron>();
		}
	}
	// --------------------------------------------------------------------------
	public class GameState : State<ArenaManager> {
		public override void OnStay() {
			if(owner.neutron == null) {
				ReleaseNewNeutron();
			}

			if(owner.unreactedFuel.Count == 0) {
				owner.completeState = CompletionStatus.Success;
				owner.stateMachine.Next(null);
			}
		}

		void ReleaseNewNeutron() {
			if(owner.reactions.Count == 0) {
				owner.completeState = CompletionStatus.Failure;
				owner.stateMachine.Next(null);
				return;
			}

			owner.neutron = owner.reactions[0].Release().GetComponent<Neutron>();
		}
	}
}

public enum CompletionStatus {
	Incomplete,
	Success,
	Failure
}