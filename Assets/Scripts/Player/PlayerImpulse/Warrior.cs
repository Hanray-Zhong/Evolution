using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour
{
    private PlayerController_ playerController;
    private void Start() {
        playerController = gameObject.GetComponent<PlayerController_>();
    }
    // 碰撞物理引擎
	private void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player" && playerController.IsControlled) {
			Debug.Log("物体速度:" + other.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude);
			// 计算玩家在两者连线上的速度大小
			float velocity;
			Vector2 thisToOther = other.gameObject.transform.position - gameObject.transform.position;
			float angle = Vector2.Angle(gameObject.GetComponent<Rigidbody2D>().velocity, thisToOther);
			velocity = Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * Mathf.Cos(angle));
			Debug.Log("相对速度:" + velocity);
			if (velocity > 3) {
				PlayerUnit other_unit = other.gameObject.GetComponent<PlayerUnit>();
				PlayerUnit self_unit = gameObject.GetComponent<PlayerUnit>();
				if (other_unit == null) {
					Debug.Log("PlayerUnit is NULL");
					return;
				}
				Impulse(other.gameObject, thisToOther, velocity, other_unit.Weight);
				this.gameObject.GetComponent<Rigidbody2D>().drag = 30;
                if (self_unit.SelfTeam != other_unit.SelfTeam)
				    other_unit.Damage(self_unit.DamageValue);
                else
                    return;
			}
		}
	}
	private void OnCollisionExit2D(Collision2D other) {
		this.gameObject.GetComponent<Rigidbody2D>().drag = playerController.Drag;
	}
	private void Impulse(GameObject target, Vector2 Dir, float velocity, float weight) {
		Debug.Log("ImpulseCoefficient:" + playerController.ImpulseCoefficient);
		target.gameObject.GetComponent<Rigidbody2D>().AddForce(Dir.normalized * velocity * playerController.ImpulseCoefficient / weight, ForceMode2D.Impulse);
	}
}
