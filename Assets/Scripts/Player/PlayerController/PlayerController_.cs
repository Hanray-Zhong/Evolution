using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_ : MonoBehaviour {
	private float impulse;
	private float impulseOffset;
	public float impulse_cd = 0;
	private Vector2 MoveDir;
	public GameInput gameInput;

	[Header("Move")]
	public float Speed;
	public float Impulse_force;
	public float Drag;

	void FixedUpdate () {
		InputConfiguration ();
		StartMove ();
		Impulse();
		VisibleImpulseForce();
	}
	void InputConfiguration () {
		MoveDir = gameInput.GetMoveDir();
		impulseOffset = gameInput.GetInputInteraction() - impulse;
		impulse = gameInput.GetInputInteraction();
	}
	void StartMove () {
		this.gameObject.GetComponent<Rigidbody2D>().drag = Drag;
		this.gameObject.GetComponent<Rigidbody2D>().AddForce(MoveDir * Speed * Time.deltaTime);
	}
	void Impulse() {
		if (impulse == 1 && Impulse_force <= 25 && impulse_cd == 0) {
			Impulse_force += 0.5f;
		}
		else if (impulseOffset < 0 && impulse_cd == 0) {
			this.gameObject.GetComponent<Rigidbody2D>().AddForce(MoveDir * Impulse_force, ForceMode2D.Impulse);
			Impulse_force = 10;
			impulse_cd++;
		}
		else if (impulse == 0) {
			Impulse_force = 10;
		}
		if (impulse_cd != 0) {
			impulse_cd++;
		}
		if (impulse_cd >= 90) {
			impulse_cd = 0;
		}
	}
	void VisibleImpulseForce() {
		if (impulse_cd != 0) {
			Color color_in_cd = new Color(0, 0, 0, 0.5f);
			gameObject.GetComponent<SpriteRenderer>().color = color_in_cd;
			return;
		}
		else {
			Color color_ready = new Color(0, 0, 0, 1);
			gameObject.GetComponent<SpriteRenderer>().color = color_ready;
		}
		Color color = new Color((Impulse_force - 10) / 15, 0, 0);
		gameObject.GetComponent<SpriteRenderer>().color = color;
	}
	private void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player") {
			if (other.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > 6.5f) {
				Debug.Log("Impulse");
			}
		}
	}
}
