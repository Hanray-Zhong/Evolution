using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public GameController gameController;
    public ItemGenerator Generator;
    [Header("Gold Coin")]
    public GameObject CoinPrefab;
    public bool CoinOn;



    public void InitPerRound() {
        if (gameController.CurrentHalfRound % 5 != 4) {
            CoinOn = true;
        }
        if (gameController.CurrentHalfRound % 5 == 4 && CoinOn) {
            Coin();
        }
    }

    private void Coin() {
        if (CoinOn) {
            Generator.GenerateOnPointsWithoutPlayer(CoinPrefab); 
            CoinOn = false;
        }
    }
}
