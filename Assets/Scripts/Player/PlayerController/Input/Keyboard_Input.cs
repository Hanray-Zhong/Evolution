using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard_Input : GameInput {
    public Camera MainCamera;
    private Vector2 MoveDir;
	private float impulse;
    private Vector2 lastMousePos = Vector2.zero;
    private float impulseUP;
    private float impulseDOWN;


    private void Update() {
        Vector2 currentMousePos = Input.mousePosition;
		Vector2 deltaMousePos = (currentMousePos - lastMousePos).normalized;
        if (Input.GetAxis("MouseLeft") != 1) {
            impulseUP = 0;
            impulseDOWN = 0;
            return;
        }
        if (deltaMousePos.x > 0) {
            if (Input.mousePosition.x < MainCamera.WorldToScreenPoint(gameObject.transform.position).x) {
                impulseUP = 0;
                impulseDOWN = 3;
            }
            else {
                impulseUP = 3;
                impulseDOWN = 0;
            }
        }
        else if (deltaMousePos.x < 0) {
            if (Input.mousePosition.x < MainCamera.WorldToScreenPoint(gameObject.transform.position).x) {
                impulseUP = 3;
                impulseDOWN = 0;
            }
            else {
                impulseUP = 0;
                impulseDOWN = 3;
            }
        }
        else if (deltaMousePos.x == 0) {
            if (deltaMousePos.y > 0) {
                if (Input.mousePosition.y < MainCamera.WorldToScreenPoint(gameObject.transform.position).y) {
                    impulseUP = 0;
                    impulseDOWN = 3;
                }
                else {
                    impulseUP = 3;
                    impulseDOWN = 0;
                }
            }
            else if (deltaMousePos.y < 0){
                if (Input.mousePosition.y < MainCamera.WorldToScreenPoint(gameObject.transform.position).y) {
                    impulseUP = 3;
                    impulseDOWN = 0;
                }
                else {
                    impulseUP = 0;
                    impulseDOWN = 3;
                }
            }
            else {
                impulseUP = 0;
                impulseDOWN = 0;
            }
        }
        else {
            impulseUP = 0;
            impulseDOWN = 0;
        }
        lastMousePos = currentMousePos;
    }
    public override Vector2 GetMoveDir() {
        MoveDir = (Vector2)(Input.mousePosition - MainCamera.WorldToScreenPoint(gameObject.GetComponent<Transform>().position)).normalized;
        return MoveDir;
    }
    public override float GetInputInteraction() {
        impulse = Input.GetAxis("Impulse");
        return impulse;
    }
    public override float GetImpulseUP() {
        return impulseUP;
    }

    public override float GetImpulseDOWN() {
        return impulseDOWN;
    }
}
