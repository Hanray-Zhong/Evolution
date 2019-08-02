using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public GameObject Target;
    public Vector2 Offset;

    private void Update() {
        gameObject.GetComponent<SpriteRenderer>().sprite = Target.GetComponent<SpriteRenderer>().sprite;
        gameObject.transform.position = Target.transform.position + (Vector3)Offset;
        gameObject.transform.rotation = Target.transform.rotation;
        gameObject.transform.localScale = Target.transform.localScale;
    }
}
