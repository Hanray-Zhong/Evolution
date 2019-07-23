using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float Damage;
    public float Force;
    private void OnTriggerEnter2D(Collider2D other) {
        GameObject Player = other.gameObject.transform.parent.gameObject;
        Debug.Log(Player.tag);

        PlayerUnit u = Player.GetComponent<PlayerUnit>();
        Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Player.GetComponent<Animator>().SetBool("Fall", true);
        if (u != null) {
            u.Damage(Damage);
        }
    }
}
