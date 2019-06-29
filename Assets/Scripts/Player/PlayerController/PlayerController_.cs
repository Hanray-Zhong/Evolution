using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_ : MonoBehaviour {
	private float impulse;
	private float impulseOffset;
	private Vector2 MoveDir;
	[Header("Controller")]
	public bool IsControlled = false;
	public bool ImpulseEnd = true;
	public GameInput gameInput;
	[Header("Move")]
	public GameObject DirectionArrow;
	public float Impulse_force;
	public float MAX_Speed;
	public float Drag;
	[Header("Impulse")]
	public Impulse Impulse;
	public float ImpulseCoefficient;

	void FixedUpdate () {
		InputConfiguration();
		StartMove();
		StartImpulse();
		VisibleIsControlled();
	}
	void InputConfiguration () {
		if (gameInput == null || ImpulseEnd) {
			return;
		}
		MoveDir = gameInput.GetMoveDir();
		impulseOffset = gameInput.GetInputInteraction() - impulse;
		impulse = gameInput.GetInputInteraction();
	}
	void StartMove () {
		this.gameObject.GetComponent<Rigidbody2D>().drag = Drag;
		if (DirectionArrow != null) {
			DirectionArrow.GetComponent<Transform>().localScale = new Vector3(Impulse_force / 100, Impulse_force / 100, Impulse_force / 100);
			gameObject.transform.up = MoveDir;
		}
	}
	void StartImpulse() {
		if (impulse == 1 && Impulse_force <= 100) {
			Impulse_force += 2;
		}
		else if (impulseOffset < 0 && !ImpulseEnd) {
			// this.gameObject.GetComponent<Rigidbody2D>().AddForce(MoveDir * Impulse_force, ForceMode2D.Impulse);
			this.gameObject.GetComponent<Rigidbody2D>().velocity = MoveDir * (Impulse_force / 100) * MAX_Speed;
			Impulse_force = 0;
			ImpulseEnd = true;
		}
		else if (impulse == 0) {
			Impulse_force = 0;
		}
	}
	void VisibleIsControlled() {
		if (IsControlled) {
			Color color_in_cd = new Color(0, 0, 0, 1f);
			gameObject.GetComponent<SpriteRenderer>().color = color_in_cd;
			return;
		}
		else {
			Color color_ready = new Color(0, 0, 0, 0.5f);
			gameObject.GetComponent<SpriteRenderer>().color = color_ready;
		}
	}

	// 物理引擎
	private void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player" && IsControlled) {
			Debug.Log("物体速度:" + other.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude);
			// 计算玩家在两者连线上的速度大小
			float velocity;
			Vector2 thisToOther = other.gameObject.transform.position - gameObject.transform.position;
			float angle = Vector2.Angle(gameObject.GetComponent<Rigidbody2D>().velocity, thisToOther);
			velocity = Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * Mathf.Cos(angle));
			Debug.Log("相对速度:" + velocity);
			if (velocity > 3) {
				Impulse.ImpulseInteraction(other.gameObject, thisToOther, velocity);
			}
		}
	}
	private void OnCollisionExit2D(Collision2D other) {
		this.gameObject.GetComponent<Rigidbody2D>().drag = Drag;
	}
}
