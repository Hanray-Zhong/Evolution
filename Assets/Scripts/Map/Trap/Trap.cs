using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float Damage;
    public float Force;
    private void OnCollisionEnter2D(Collision2D other) {
        PlayerUnit u = other.gameObject.GetComponent<PlayerUnit>();
        if (u != null) {
            Vector2 Dir = (other.gameObject.transform.position - gameObject.transform.position).normalized;
            if (Mathf.Abs(Dir.x) > Mathf.Abs(Dir.y)) {
                Dir = new Vector2(Dir.x, 0).normalized;
            }
            else {
                Dir = new Vector2(0, Dir.y).normalized;
            }
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(Dir * Force, ForceMode2D.Impulse);
            u.Damage(Damage);
        }
    }
}
