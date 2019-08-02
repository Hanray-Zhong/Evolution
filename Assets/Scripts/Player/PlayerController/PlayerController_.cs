using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_ : MonoBehaviour {
	private float impulse;
	private float impulse_Change;
	private float impulseOffset;
	private Vector2 MoveDir;
	[Header("Controller")]
	public bool IsControlled = false;
	public bool ImpulseEnd = true;
	public GameInput gameInput;
	public GameController gameController;
	[Header("Move")]
	public GameObject DirectionArrow;
	public float Impulse_force;
	public float MAX_Speed;
	// public float Drag;
	[Header("Impulse")]
	public Impulse Impulse;
	public ShakeCamera shakeCamera;
	// public float ImpulseCoefficient;

	void FixedUpdate () {
		InputConfiguration();
		StartMove();
		StartImpulse();
		// MyDrag();
		// VisibleIsControlled();
	}
	void InputConfiguration () {
		if (gameInput == null || ImpulseEnd) {
			return;
		}
		MoveDir = gameInput.GetMoveDir();
		impulseOffset = gameInput.GetInputInteraction() - impulse;
		impulse = gameInput.GetInputInteraction();
		impulse_Change = gameInput.GetImpulseUP() - gameInput.GetImpulseDOWN();
	}
	void StartMove () {
		// this.gameObject.GetComponent<Rigidbody2D>().drag = Drag;
		if (DirectionArrow != null) {
			DirectionArrow.GetComponent<Transform>().localScale = new Vector3(50 * Impulse_force / 100, 50 * Impulse_force / 100, 50 * Impulse_force / 100);
			gameObject.transform.up = MoveDir;
		}
	}
	void StartImpulse() {
		if (ImpulseEnd) {
			return;
		}
		if (Impulse_force <= 100 && Impulse_force >= 0) {
			Impulse_force += 2 * impulse_Change;
		}
		if (Impulse_force > 100) {
			Impulse_force = 100;
		}
		if (Impulse_force < 0) {
			Impulse_force = 0;
		}
		if (impulse == 1 && !ImpulseEnd) {
			// this.gameObject.GetComponent<Rigidbody2D>().AddForce(MoveDir * Impulse_force, ForceMode2D.Impulse);
			this.gameObject.GetComponent<Rigidbody2D>().velocity = MoveDir * (Impulse_force / 100) * MAX_Speed;
			Impulse_force = 0;
			ImpulseEnd = true;
			gameController.ImpulseEnd = ImpulseEnd;
		}
	}
	// void VisibleIsControlled() {
	// 	if (IsControlled) {
	// 		Color color_in_cd = new Color(0, 0, 0, 1f);
	// 		gameObject.GetComponent<SpriteRenderer>().color = color_in_cd;
	// 		return;
	// 	}
	// 	else {
	// 		Color color_ready = new Color(0, 0, 0, 0.5f);
	// 		gameObject.GetComponent<SpriteRenderer>().color = color_ready;
	// 	}
	// }
	// void MyDrag() {
	// 	if (gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > 0.5f) {
	// 		gameObject.GetComponent<Rigidbody2D>().velocity -= gameObject.GetComponent<Rigidbody2D>().velocity.normalized * Drag * Time.deltaTime;
	// 	}
	// 	else {
	// 		gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
	// 	}
	// }

	private void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player" && IsControlled) {
			if (other.gameObject.GetComponent<PlayerUnit>().SelfTeam != gameObject.GetComponent<PlayerUnit>().SelfTeam)
				StartCoroutine("ShakerCamera");
			Impulse.ImpulseInteraction(other.gameObject);
		}
	}
	IEnumerator ShakerCamera() {
		shakeCamera.enabled = true;
		yield return new WaitForSeconds(0.1f);
		shakeCamera.enabled = false;
	}
}