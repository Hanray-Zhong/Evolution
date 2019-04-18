using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard_Input : GameInput {
    private Vector2 MoveDir;
    private float hl;
	private float vt;
	private float impulse;

    public override Vector2 GetMoveDir() {
        hl = Input.GetAxis("Horizontal");
		vt = Input.GetAxis("Vertical");
        MoveDir = new Vector2(hl, vt).normalized;
        return MoveDir;
    }
    public override float GetInputInteraction() {
		impulse = Input.GetAxis("impulse");
        return impulse;
    }
}
