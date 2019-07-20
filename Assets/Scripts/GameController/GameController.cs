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
        // CurrentRound = (CurrentTurn - 1) / Players.Length + 1;
        // CurrentHalfRound = (CurrentTurn - 1) / (Players.Length / 2) + 1;
        // MapEffect.InitPerRound();
        // currentPlayer.GetComponent<PlayerUnit>().ReleaseControl();
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
        if ((IsControlled && ImpulseEnd && !UseCoroutine) || currentPlayer.GetComponent<PlayerUnit>().IsDead) {
            UseCoroutine = true;
            StartCoroutine(SwitchRound());
        }
    }

    void Init() {
        CurrentRound = CurrentTurn / Players.Length + 1;
        CurrentHalfRound = CurrentTurn / (Players.Length / 2) + 1;

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

        // 回合控制以及回合开始时发生的效果
        CurrentRound = CurrentTurn / Players.Length + 1;
        CurrentHalfRound = CurrentTurn / (Players.Length / 2) + 1;
        MapEffect.InitPerRound();
        foreach (var palyer in Players) {
            PlayerUnit u = palyer.GetComponent<PlayerUnit>();
            if (u == null || !u.controlled) continue;
            u.ReleaseControl();
        }
        
        // 切换人物
        if (currentPlayer != null)
            currentPlayer.GetComponent<PlayerController_>().IsControlled = false;
        int protect = 0;
        bool _continue;
        do {
            _continue = false;
            protect++;
            if (protect > 7) {
                Debug.Log("Loop Error");
                break;
            }
            currentPlayerID++;
            if (currentPlayerID == Players.Length) {
                currentPlayerID = 0;
            }
            CurrentTurn++;
            currentPlayer = Players[currentPlayerID]; 
            if (currentPlayer.GetComponent<PlayerUnit>().controlled) {
                _continue = true;
            }
        } while (currentPlayer.GetComponent<PlayerUnit>().IsDead == true || _continue);

        currentPlayer.GetComponent<PlayerController_>().ImpulseEnd = false;
        currentPlayer.GetComponent<PlayerController_>().IsControlled = true;
        currentPlayer.GetComponent<PlayerController_>().Impulse_force = 50;
        IsControlled = currentPlayer.GetComponent<PlayerController_>().IsControlled;
        ImpulseEnd = currentPlayer.GetComponent<PlayerController_>().ImpulseEnd;
        
        // 携程结束
        UseCoroutine = false;
    }
}
