using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEffect : MonoBehaviour
{
    public GameController gameController;
    public ItemGenerator Generator;
    [Header("Thunder")]
    public bool ThunderOn;
    public GameObject ThunderPrefab;
    public GameObject[] ThunderObjs;


    public void InitPerRound() {
        if (gameController.CurrentRound % 5 != 3) {
            ThunderOn = true;
        }
        if (gameController.CurrentRound % 5 == 3 && ThunderOn) {
            Thunder();
        }
        DetermineThunder();
    }


    private void Thunder() {
        if (ThunderOn) {
            Generator.GenerateRandom(5, ThunderPrefab); 
            ThunderOn = false;
        }
    }
    public void DetermineThunder() {
        ThunderObjs = GameObject.FindGameObjectsWithTag("Thunder");
        foreach (var item in ThunderObjs) {
            item.GetComponent<Thunder>().CheakTrigger();
        }
    }

}
