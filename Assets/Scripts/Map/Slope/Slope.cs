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
        GameObject Player = other.gameObject.transform.parent.gameObject;

        Rigidbody2D rigidbody2D = Player.GetComponent<Rigidbody2D>();
        if (rigidbody2D == null) {
            Debug.Log("Error : The RigidBody is null");
            return;
        }
        // Debug.Log("get");
        rigidbody2D.AddForce(SlopeDir * rigidbody2D.mass * 9.8f * Mathf.Sin(KanetoTools.Arithmetic.AngleToRadian(45)), ForceMode2D.Force);
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(1, 0, 0, 1);
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)SlopeDir);
    }
}
