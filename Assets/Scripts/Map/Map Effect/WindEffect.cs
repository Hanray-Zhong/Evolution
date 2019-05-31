using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEffect : MonoBehaviour
{
    public Vector2 WindDir;
    public float WindForce;
    public GameObject[] Players;
    private void Update() {
        foreach (var Player in Players) {
            Player.GetComponent<Rigidbody2D>().AddForce(WindDir.normalized * WindForce * Time.deltaTime, ForceMode2D.Force);
        }
    }
}
