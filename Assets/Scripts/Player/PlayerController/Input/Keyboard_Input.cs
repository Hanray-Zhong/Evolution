using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard_Input : GameInput {
    public Camera MainCamera;
    private Vector2 MoveDir;
	private float impulse;
    private float impulseUP;
    private float impulseDOWN;

    public override Vector2 GetMoveDir() {
        MoveDir = (Vector2)(Input.mousePosition - MainCamera.WorldToScreenPoint(gameObject.GetComponent<Transform>().position)).normalized;
        return MoveDir;
    }
    public override float GetInputInteraction() {
		impulse = Input.GetAxis("Impulse");
        return impulse;
    }
    public override float GetImpulseUP() {
        impulseUP = Input.GetAxis("ImpulseUP");
        return impulseUP;
    }

    public override float GetImpulseDOWN() {
        impulseDOWN = Input.GetAxis("ImpulseDOWN");
        return impulseDOWN;
    }
}
