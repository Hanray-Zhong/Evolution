using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Altar : MonoBehaviour
{
    public ScoreController scoreController;
    public bool isStatueExist;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<PlayerController_>().HaveStatue && other.gameObject.GetComponent<PlayerUnit>().SelfTeam == Team.Team_1) {
            scoreController.Team1_Score += 8;
            other.gameObject.GetComponent<PlayerController_>().HaveStatue = false;
            isStatueExist = false;
        }
        else if (other.gameObject.GetComponent<PlayerController_>().HaveStatue && other.gameObject.GetComponent<PlayerUnit>().SelfTeam == Team.Team_2) {
            scoreController.Team2_Score += 8;
            other.gameObject.GetComponent<PlayerController_>().HaveStatue = false;
            isStatueExist = false;
        }
        
    }
}
