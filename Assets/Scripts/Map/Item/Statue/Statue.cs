using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player" && !other.GetComponent<PlayerController_>().HaveStatue) {
            other.GetComponent<PlayerController_>().HaveStatue = true;
            Destroy(gameObject);
        }
    }
}
