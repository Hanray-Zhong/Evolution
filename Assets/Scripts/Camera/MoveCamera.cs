using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public float Speed;
    private Vector2 lastMousePos = Vector2.zero;
    private Vector2 velocity;

    void Update() {
        Vector2 currentMousePos = Input.mousePosition;
		velocity = (currentMousePos - lastMousePos).normalized;
        gameObject.GetComponent<Transform>().Translate(velocity * Speed * Time.deltaTime, Space.World);
        lastMousePos = currentMousePos;
    }
}
