using UnityEngine;

public class Android_Input : GameInput {
    public bl_Joystick joystick;
    private float hl;
	private float vt;
    private Vector2 MoveDir;
    private float impulse;
    public ButtonTrigger ImpulseButton;
    private float impulseUP;
    public ButtonTrigger ImpulseUpButton;

    private float impulseDOWN;
    public ButtonTrigger ImpulseDownButton;


    public override Vector2 GetMoveDir() {
        hl = joystick.Horizontal;
		vt = joystick.Vertical;
        MoveDir = new Vector2(hl, vt).normalized;
        return MoveDir;
    }
    public override float GetInputInteraction() {
		impulse = ImpulseButton.ClickOn;
        return impulse;
    }

    public override float GetImpulseUP() {
        impulseUP = ImpulseUpButton.ClickOn;
        return impulseUP;
    }

    public override float GetImpulseDOWN() {
        impulseDOWN = ImpulseDownButton.ClickOn;
        return impulseDOWN;
    }
}