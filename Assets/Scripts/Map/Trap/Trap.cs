using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float Damage;
    public float Force;
    public ScoreController scoreController;
    private void OnTriggerEnter2D(Collider2D other) {
        GameObject Player = other.gameObject.transform.parent.gameObject;
        Debug.Log(Player.tag);

        PlayerUnit u = Player.GetComponent<PlayerUnit>();
        Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Player.GetComponent<Animator>().SetBool("Fall", true);
        if (u != null) {
            u.FallVoice.Play();
            u.DeathRound = u.gameController.CurrentRound;
            u.IsDead = true;
            if (u.SelfTeam == Team.Team_1) {
                scoreController.Team2_Score += 3;
            }
            else {
                scoreController.Team1_Score += 3;
            }
        }
    }
}
