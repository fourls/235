using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T> {
	private T owner;
	private State<T> current = null;

	public StateMachine(T owner) {
		this.owner = owner;
	}

	public void Update() {
		if(current != null) current.OnStay();
	}

	public void Next(State<T> next) {
		if(current != null)
			current.OnExit();

		current = next;
		
		if(next != null) {
			current.owner = owner;
			current.OnEnter();
		}
	}
}

public class State<T> {
	public T owner;
	public virtual void OnEnter() {}
	public virtual void OnStay() {}
	public virtual void OnExit() {}
}
