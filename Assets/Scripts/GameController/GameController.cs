using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] Players;
    public GameObject currentPlayer;
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
        currentPlayer.GetComponent<PlayerController_>().Impulse_force = 50;
    }

    IEnumerator ChangePlayer() {
        yield return new WaitForSeconds(0);
        currentPlayer.GetComponent<PlayerController_>().IsControlled = false;
        int protect = 0;
        do {
            protect++;
            if (protect > 100) {
                Debug.Log("Loop Error");
                break;
            }
            currentPlayerID++;
            if (currentPlayerID == Players.Length) {
                currentPlayerID = 0;
            }
            currentPlayer = Players[currentPlayerID]; 
        } while (currentPlayer == null);
        currentPlayer.GetComponent<PlayerController_>().ImpulseEnd = false;
        currentPlayer.GetComponent<PlayerController_>().IsControlled = true;
        currentPlayer.GetComponent<PlayerController_>().Impulse_force = 50;
        UseCoroutine = false;
    }
}
