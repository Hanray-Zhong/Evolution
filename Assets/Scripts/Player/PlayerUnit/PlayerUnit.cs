using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team {
        Team_1,
        Team_2
    }
public class PlayerUnit : MonoBehaviour
{
    public Team SelfTeam;
    [Header("properties")]
    public float Health = 100;
    public float Weight = 1;
    public float DamageValue;
    [Header("Death")]
    public bool IsDead = false;
    public GameObject DeadEffect;

    public void Damage(float damage) {
        gameObject.GetComponent<PlayerUnit>().Health -= damage;
        if (gameObject.GetComponent<PlayerUnit>().Health <= 0) {
            IsDead = true;
            Instantiate(DeadEffect, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }
}
