using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private ScoreController scoreController;

    private void Awake() {
        scoreController = GameObject.FindGameObjectWithTag("ScoreController").GetComponent<ScoreController>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            PlayerUnit u = other.GetComponent<PlayerUnit>();
            if (u != null) {
                if (u.SelfTeam == Team.Team_1) {
                    scoreController.Team2_Score += 4;
                }
                else {
                    scoreController.Team1_Score += 4;
                }
            }
            Destroy(gameObject);
        }
    }
}
