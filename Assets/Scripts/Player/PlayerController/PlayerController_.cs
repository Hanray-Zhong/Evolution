using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_ : MonoBehaviour {
	private float hl;
	private float vt;
	private float impulse;
	public float impulse_cd = 0;
	private Vector2 MoveDir;

	[Header("Move")]
	public float Speed;
	public float Impulse_force;
	public float Drag;

	void FixedUpdate () {
		HandleConfiguration ();
		GetMoveDir ();
		StartMove ();
		Impulse();
	}
	void HandleConfiguration () {
		hl = Input.GetAxis("Horizontal");
		vt = Input.GetAxis("Vertical");
	}
	void GetMoveDir () {
		MoveDir = new Vector2(hl, vt).normalized;
	}
	void StartMove () {
		this.gameObject.GetComponent<Rigidbody2D>().drag = Drag;
		this.gameObject.GetComponent<Rigidbody2D>().AddForce(MoveDir * Speed * Time.deltaTime);
	}
	void Impulse() {
		if (Input.GetKey(KeyCode.J) && Impulse_force <= 25) {
			Impulse_force += 0.5f;
		}
		else if (Input.GetKeyUp(KeyCode.J) && impulse_cd == 0) {
			this.gameObject.GetComponent<Rigidbody2D>().AddForce(MoveDir * Impulse_force, ForceMode2D.Impulse);
			Debug.Log(Impulse_force);
			Impulse_force = 10;
			impulse_cd++;
		}
		else if (!Input.GetKey(KeyCode.J)) {
			Impulse_force = 10;
		}
		if (impulse_cd != 0) {
			impulse_cd++;
		}
		if (impulse_cd >= 90) {
			impulse_cd = 0;
		}
	}
}
