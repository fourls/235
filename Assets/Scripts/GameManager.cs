using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager ins = null;
	public ArenaManager arena = null;

	public StateMachine<GameManager> stateMachine;

	void Awake() {
		if(ins == null) ins = this;
		else if (ins != this) Destroy(gameObject);

		stateMachine = new StateMachine<GameManager>(this);
		stateMachine.Next(new WaitingState());
	}

	void Update() {
		stateMachine.Update();
	}

	// --------------------------------------------------------------------------
	public class WaitingState : State<GameManager> {
		public override void OnStay() {
			if(owner.arena != null) {
				owner.stateMachine.Next(new InArenaState());
			}
		}
	}
	// --------------------------------------------------------------------------
	public class InArenaState : State<GameManager> {
		public override void OnEnter() {
			owner.arena.stateMachine.Next(new ArenaManager.SetupState());
		}

		public override void OnStay() {
			if(owner.arena.completeState != CompletionStatus.Incomplete) {
				if(owner.arena.onEnd != null) {
					DirectorController.ins.Play(owner.arena.onEnd);
				}
				owner.arena.stateMachine.Next(null);
				owner.arena = null;
				owner.stateMachine.Next(new WaitingState());
			}
		}
	}
}
