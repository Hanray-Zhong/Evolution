using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] Players;
    private GameObject currentPlayer;
    private int currentPlayerID;
    private bool UseCoroutine = false;

    private void Start() {
        Init();
    }

    private void Update() {
        RoundController();
    }

    void RoundController() {
        foreach (var player in Players) {
            if (player.GetComponent<Rigidbody2D>().velocity != Vector2.zero) {
                return;
            }
        }
        if (currentPlayer.GetComponent<PlayerController_>().IsControlled && currentPlayer.GetComponent<PlayerController_>().ImpulseEnd && !UseCoroutine) {
            UseCoroutine = true;
            StartCoroutine(ChangePlayer());
        }
    }

    void Init() {
        currentPlayerID = 0;
        currentPlayer = Players[currentPlayerID];
        currentPlayer.GetComponent<PlayerController_>().IsControlled = true;
        currentPlayer.GetComponent<PlayerController_>().ImpulseEnd = false;
    }

    IEnumerator ChangePlayer() {
        yield return new WaitForSeconds(1);
        currentPlayer.GetComponent<PlayerController_>().IsControlled = false;
        currentPlayerID++;
        if (currentPlayerID == Players.Length) {
            currentPlayerID = 0;
        }
        currentPlayer = Players[currentPlayerID];
        currentPlayer.GetComponent<PlayerController_>().ImpulseEnd = false;
        currentPlayer.GetComponent<PlayerController_>().IsControlled = true;
        UseCoroutine = false;
    }
}
