using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team {
        Team_1,
        Team_2
    }
public class PlayerUnit : MonoBehaviour
{   
    public GameController gameController;
    public Team SelfTeam;
    [Header("properties")]
    public float MaxHealth;
    public float Health;
    // public float Weight = 1;
    public float DamageValue;
    [Header("Death")]
    public bool IsDead = false;
    public GameObject DeadEffect;
    [Header("Controlled")]
    public bool controlled;
    public int StartRound;
    public int LastRound;

    private void Start() {
        Health = MaxHealth;
    }

    public void SetControl(int LastRound) {
        this.StartRound = gameController.CurrentRound;
        this.LastRound = LastRound; 
    }
    public void ReleaseControl() {
        if (gameController.CurrentRound - StartRound >= LastRound) {
            controlled = false;
        }
    }

    public void Damage(float damage) {
        gameObject.GetComponent<PlayerUnit>().Health -= damage;
        if (gameObject.GetComponent<PlayerUnit>().Health <= 0) {
            IsDead = true;
        }
        if (Health > MaxHealth) {
            Health = MaxHealth;
        }
    }
}
