using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassin : Impulse
{
	public override void ImpulseInteraction(GameObject other) {
		PlayerUnit other_unit = other.GetComponent<PlayerUnit>();
		PlayerUnit self_unit = gameObject.GetComponent<PlayerUnit>();
		if (other_unit == null) {
			Debug.Log("PlayerUnit is NULL");
			return;
		}
		// float weight = gameObject.GetComponent<PlayerUnit>().Weight;
		// other.gameObject.GetComponent<Rigidbody2D>().velocity = Dir.normalized * velocity * weight / other_unit.Weight;
		// this.gameObject.GetComponent<Rigidbody2D>().drag = 30;
        if (self_unit.SelfTeam != other_unit.SelfTeam) {
			other_unit.Damage(self_unit.DamageValue + 0.25f * other_unit.MaxHealth);
		}
        else
            return;
	}

}