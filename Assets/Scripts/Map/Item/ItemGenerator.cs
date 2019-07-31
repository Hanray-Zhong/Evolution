using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public BoxCollider2D[] Colliders;
    public Vector2 Margin;
    public Vector2 Edge;

    public void GenerateRandom(int times, GameObject obj) {
        for (int i = 0; i < times; i++) {
            int index = Random.Range(0, Colliders.Length);
            Vector2 Pos = new Vector2(Random.Range(Colliders[index].bounds.min.x + Margin.x, Colliders[index].bounds.max.x - Margin.x), Random.Range(Colliders[index].bounds.min.y + Margin.y, Colliders[index].bounds.max.y - Margin.y));
            Instantiate(obj, Pos, Quaternion.identity);
        }
    }
    public void GenerateOnPointsWithoutPlayer(GameObject obj) {
        List<BoxCollider2D> cols = new List<BoxCollider2D>();
        foreach (var col in Colliders) {
            cols.Add(col);
        }
        for (int i = 0; i < Colliders.Length; i++) {
            int index = Random.Range(0, cols.ToArray().Length);
            if (cols.ToArray()[index].IsTouchingLayers(1 << LayerMask.NameToLayer("Player"))) {
                cols.RemoveAt(index);
                continue;
            }
            Vector2 Pos = new Vector2(Random.Range(Colliders[index].bounds.min.x + Margin.x, Colliders[index].bounds.max.x - Margin.x), Random.Range(Colliders[index].bounds.min.y + Margin.y, Colliders[index].bounds.max.y - Margin.y));
            Instantiate(obj, Pos, Quaternion.identity);
            break;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(1, 0, 0, 1);
        foreach (var col in Colliders) {
            Vector3 a = new Vector3(col.bounds.max.x - Margin.x, col.bounds.max.y - Margin.y, 0);
            Vector3 b = new Vector3(col.bounds.max.x - Margin.x, col.bounds.min.y + Margin.y, 0);
            Vector3 c = new Vector3(col.bounds.min.x + Margin.x, col.bounds.min.y + Margin.y, 0);
            Vector3 d = new Vector3(col.bounds.min.x + Margin.x, col.bounds.max.y - Margin.y, 0);

            Gizmos.DrawLine(a, b);
            Gizmos.DrawLine(a, d);
            Gizmos.DrawLine(c, b);
            Gizmos.DrawLine(c, d);
        }
        
    } 
    // private void OnDrawGizmos() {
    //     Gizmos.color = new Color(0.5f, 0.5f, 0.5f, 1);
    //     foreach (var col in Colliders) {
    //         Vector3 a = new Vector3(col.bounds.max.x - Edge.x, col.bounds.max.y - Edge.y, 0);
    //         Vector3 b = new Vector3(col.bounds.max.x - Edge.x, col.bounds.min.y + Edge.y, 0);
    //         Vector3 c = new Vector3(col.bounds.min.x + Edge.x, col.bounds.min.y + Edge.y, 0);
    //         Vector3 d = new Vector3(col.bounds.min.x + Edge.x, col.bounds.max.y - Edge.y, 0);

    //         Gizmos.DrawLine(a, b);
    //         Gizmos.DrawLine(a, d);
    //         Gizmos.DrawLine(c, b);
    //         Gizmos.DrawLine(c, d);
    //     }
    // }
}
