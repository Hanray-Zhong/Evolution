using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject MainCamera;
    public GameObject[] Players;
    public GameObject currentPlayer;
    public MapEffect MapEffect;
    private int currentPlayerID;
    private bool UseCoroutine = false;
    [Header("Current Player Status")]
    public bool IsControlled;
    public bool ImpulseEnd;
    [Header("Player Turn")]
    public int CurrentRound;
    public int CurrentHalfRound;
    public int CurrentTurn = 1;

    
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
        if (((IsControlled && ImpulseEnd) || currentPlayer.GetComponent<PlayerUnit>().IsDead || currentPlayer.GetComponent<PlayerUnit>().controlled) && !UseCoroutine) {
            UseCoroutine = true;
            StartCoroutine(SwitchRound());
        }
    }

    void Init() {
        CurrentRound = 1;
        CurrentHalfRound = 1;

        currentPlayerID = 0;
        currentPlayer = Players[currentPlayerID];
        currentPlayer.GetComponent<PlayerController_>().IsControlled = true;
        currentPlayer.GetComponent<PlayerController_>().ImpulseEnd = false;
        currentPlayer.GetComponent<PlayerController_>().Impulse_force = 50;

        IsControlled = currentPlayer.GetComponent<PlayerController_>().IsControlled;
        ImpulseEnd = currentPlayer.GetComponent<PlayerController_>().ImpulseEnd;
    }

    IEnumerator SwitchRound() {
        yield return new WaitForSeconds(2);
        CurrentTurn++;
        currentPlayerID++;
        if (currentPlayerID == Players.Length) {
            currentPlayerID = 0;
        }

        // 回合控制以及回合开始时发生的效果
        if (currentPlayerID == Players.Length / 2 || currentPlayerID == 0)
            CurrentHalfRound++;
        if (currentPlayerID == 0)
            CurrentRound++;
        MapEffect.InitPerRound();
        foreach (var palyer in Players) {
            PlayerUnit u = palyer.GetComponent<PlayerUnit>();
            if (u != null && u.IsDead) 
                u.Resurrection();
            if (u != null && u.controlled) 
                u.ReleaseControl();
        }
        
        // 切换人物
        if (currentPlayer != null)
            currentPlayer.GetComponent<PlayerController_>().IsControlled = false;        
        currentPlayer = Players[currentPlayerID];
        PlayerUnit cu = currentPlayer.GetComponent<PlayerUnit>();
        if (!cu.IsDead && !cu.controlled) {
            currentPlayer.GetComponent<PlayerController_>().ImpulseEnd = false;
            currentPlayer.GetComponent<PlayerController_>().IsControlled = true;
            currentPlayer.GetComponent<PlayerController_>().Impulse_force = 50;
            IsControlled = currentPlayer.GetComponent<PlayerController_>().IsControlled;
            ImpulseEnd = currentPlayer.GetComponent<PlayerController_>().ImpulseEnd;
        }
        
        // 携程结束
        UseCoroutine = false;
    }
}
