using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Impulse
{
    public override void ImpulseInteraction(GameObject other, Vector2 Dir, float velocity) {
		PlayerUnit other_unit = other.GetComponent<PlayerUnit>();
		PlayerUnit self_unit = gameObject.GetComponent<PlayerUnit>();
		if (other_unit == null) {
			Debug.Log("PlayerUnit is NULL");
			return;
		}
		float ImpulseCoefficient = gameObject.GetComponent<PlayerController_>().ImpulseCoefficient;
		other.gameObject.GetComponent<Rigidbody2D>().AddForce(Dir.normalized * velocity * ImpulseCoefficient / other_unit.Weight, ForceMode2D.Impulse);
		this.gameObject.GetComponent<Rigidbody2D>().drag = 30;
        if (self_unit.SelfTeam != other_unit.SelfTeam)
			other_unit.Damage(self_unit.DamageValue);
        else
            return;
	}
    
}
