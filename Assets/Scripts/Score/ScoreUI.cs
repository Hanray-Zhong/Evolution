using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public Team SelfTeam;
    public ScoreController scoreController;
    private Text text;

    private void Start() {
        text = gameObject.GetComponent<Text>();
    }
    private void Update() {
        if (SelfTeam == Team.Team_1) {
            text.text = scoreController.Team1_Score.ToString();
        }
        else {
            text.text = scoreController.Team2_Score.ToString();
        }
    }
}
