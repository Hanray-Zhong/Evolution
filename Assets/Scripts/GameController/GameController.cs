using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject MainCamera;
    public GameObject[] Players;
    public GameObject currentPlayer;
    private int currentPlayerID;
    private bool UseCoroutine = false;
    [Header("Current Player Status")]
    public bool IsControlled;
    public bool ImpulseEnd;
    [Header("Player Turn")]
    public int CurrentTurn = 1;
    // public int Team1_first;
    // public int Team2_first;
    // public  bool Turn_Start;
    // public  bool Turn_End;
    
    private void Start() {
        Init();
    }

    private void Update() {
        RoundController();
    }

    void RoundController() {
        foreach (var player in Players) {
            if (player == null) {
                continue;
            }
            if (player.GetComponent<Rigidbody2D>().velocity != Vector2.zero) {
                return;
            }
        }
        if (IsControlled && ImpulseEnd && !UseCoroutine) {
            UseCoroutine = true;
            StartCoroutine(ChangePlayer());
        }
    }

    void Init() {
        currentPlayerID = 0;
        currentPlayer = Players[currentPlayerID];

        // MainCamera.GetComponent<KanetoTools.CameraFollow>().player = currentPlayer.transform;
        // MainCamera.GetComponent<KanetoTools.CameraFollow>().FollowMouse = false;
        // MainCamera.GetComponent<KanetoTools.CameraFollow>().PlayerToMouse = true;
        // MainCamera.GetComponent<KanetoTools.CameraFollow>().Margin = Vector2.zero;

        currentPlayer.GetComponent<PlayerController_>().IsControlled = true;
        currentPlayer.GetComponent<PlayerController_>().ImpulseEnd = false;
        currentPlayer.GetComponent<PlayerController_>().Impulse_force = 50;

        IsControlled = currentPlayer.GetComponent<PlayerController_>().IsControlled;
        ImpulseEnd = currentPlayer.GetComponent<PlayerController_>().ImpulseEnd;
    }

    IEnumerator ChangePlayer() {
        yield return new WaitForSeconds(1);
        if (currentPlayer != null)
            currentPlayer.GetComponent<PlayerController_>().IsControlled = false;
        int protect = 0;
        do {
            protect++;
            if (protect > 7) {
                Debug.Log("Loop Error");
                break;
            }
            currentPlayerID++;
            if (currentPlayerID == Players.Length) {
                currentPlayerID = 0;
            }
            // if (currentPlayerID == Team1_first) {
            //     Turn_Start = true;
            //     Turn_End = false;
            // } 
            // else if (currentPlayerID == Team2_first) {
            //     Turn_Start = false;
            //     Turn_Start = true;
            // } 
            // else {
            //     Turn_Start = false;
            //     Turn_End = false;
            // }
            CurrentTurn++;
            currentPlayer = Players[currentPlayerID]; 
        } while (currentPlayer == null);
        // MainCamera.GetComponent<KanetoTools.CameraFollow>().player = currentPlayer.transform;
        // MainCamera.GetComponent<KanetoTools.CameraFollow>().FollowMouse = false;
        // MainCamera.GetComponent<KanetoTools.CameraFollow>().PlayerToMouse = true;
        // MainCamera.GetComponent<KanetoTools.CameraFollow>().Margin = Vector2.zero;

        currentPlayer.GetComponent<PlayerController_>().ImpulseEnd = false;
        currentPlayer.GetComponent<PlayerController_>().IsControlled = true;
        currentPlayer.GetComponent<PlayerController_>().Impulse_force = 50;
        IsControlled = currentPlayer.GetComponent<PlayerController_>().IsControlled;
        ImpulseEnd = currentPlayer.GetComponent<PlayerController_>().ImpulseEnd;
        UseCoroutine = false;
    }
}
