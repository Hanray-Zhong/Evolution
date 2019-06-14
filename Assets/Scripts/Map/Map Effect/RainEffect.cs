using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainEffect : MonoBehaviour
{
    public float DragInRain;
    public GameObject[] Players;
    private void Update() {
        foreach (var Player in Players) {
            if (Player != null)
                Player.GetComponent<PlayerController_>().Drag = DragInRain;
        }
    }
}
