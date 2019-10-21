using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueController : MonoBehaviour
{
    public GameController gameController;
    public ItemGenerator Generator;
    public Altar altar;
    [Header("Statue")]
    public GameObject StatuePrefab;
    public bool StatueOn;



    public void InitPerRound() {
        if (gameController.CurrentHalfRound % 5 != 4) {
            StatueOn = true;
        }
        if (gameController.CurrentHalfRound % 5 == 4 && StatueOn && !altar.isStatueExist) {
            CreatStatue();
            altar.isStatueExist = true;
        }
    }

    private void CreatStatue() {
        if (StatueOn) {
            Generator.GenerateOnPointsWithoutPlayer(StatuePrefab); 
            StatueOn = false;
        }
    }
}
