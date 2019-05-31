using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xbox_Input : GameInput {
    private Vector2 MoveDir;
    private float hl;
	private float vt;
	private float impulse;

    public override Vector2 GetMoveDir() {
        hl = Input.GetAxis("Horizontal_Xbox");
		vt = Input.GetAxis("Vertical_Xbox");
        MoveDir = new Vector2(hl, vt).normalized;
        return MoveDir;
    }
    public override float GetInputInteraction() {
		impulse = Input.GetAxis("Impulse_Xbox");
        return impulse;
    }
}
