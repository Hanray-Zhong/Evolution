using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEffect : MonoBehaviour
{
    public GameController gameController;
    [Header("Thunder")]
    public bool ThunderOn;
    public GameObject ThunderPrefab;

    public Vector2[] Positions;
    

    public void Thunder() {
        foreach (var pos in Positions) {
            GameObject thunder = Instantiate(ThunderPrefab, pos, ThunderPrefab.transform.rotation);
            thunder.GetComponent<Thunder>().Start_Turn = gameController.CurrentTurn;
        }
        
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(0.5f, 0.5f, 0.5f, 1);
        foreach (var pos in Positions) {
            Gizmos.DrawSphere(pos, 0.05f);
        }
    }
}
