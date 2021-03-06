﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team {
        Team_1,
        Team_2
    }
public class PlayerUnit : MonoBehaviour
{   
    public GameController gameController;
    public ScoreController scoreController;
    public Animator animator;
    public Team SelfTeam;
    [Header("properties")]
    public float MaxHealth;
    public float Health;
    public float Drag;
    // public float Weight = 1;
    public float DamageValue;
    [Header("Death")]
    public bool IsDead = false;
    public int Death;
    public Vector2 ResurrectionPos;
    public AudioSource FallVoice;
    public int DeathRound;
    [Header("Controlled")]
    public bool controlled;
    public int StartRound;
    public int LastRound;

    private void Start() {
        Health = MaxHealth;
        gameObject.GetComponent<Rigidbody2D>().drag = Drag;
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

    public void Resurrection() {
        if (gameController.CurrentRound - DeathRound >= 1) {
            animator.SetBool("Fall", false);
            animator.Play("Idle");
            transform.position = ResurrectionPos;
            IsDead = false;
            controlled = false;
            Health = MaxHealth;
        }
    }

    public void Damage(float damage) {
        gameObject.GetComponent<PlayerUnit>().Health -= damage;
        if (gameObject.GetComponent<PlayerUnit>().Health <= 0) {
            this.DeathRound = gameController.CurrentRound;
            IsDead = true;
            if (gameObject.GetComponent<PlayerController_>().HaveStatue) {
                gameObject.GetComponent<PlayerController_>().HaveStatue = false;
                gameController.currentPlayer.GetComponent<PlayerController_>().HaveStatue = true;
            }
            if (SelfTeam == Team.Team_1) {
                scoreController.Team2_Score += 3;
            }
            else {
                scoreController.Team1_Score += 3;
            }
            animator.Play("Dead");
        }
        if (Health > MaxHealth) {
            Health = MaxHealth;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(1, 0, 0, 1);
        Gizmos.DrawSphere(ResurrectionPos, 0.05f);

    }
}
