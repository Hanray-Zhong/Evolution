using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanetoTools;

public class Slope : MonoBehaviour
{
    public Vector2 SlopeDir;
    private void Awake() {
        SlopeDir = SlopeDir.normalized;
    }
    private void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "Player") {
            Rigidbody2D rigidbody2D = other.gameObject.GetComponent<Rigidbody2D>();
            if (rigidbody2D == null) {
                Debug.Log("Error : The RigidBody is null");
                return;
            }
            // Debug.Log("get");
            rigidbody2D.AddForce(SlopeDir * rigidbody2D.mass * 9.8f * Mathf.Sin(KanetoTools.Arithmetic.AngleToRadian(45)), ForceMode2D.Force);
        }
    }
}
