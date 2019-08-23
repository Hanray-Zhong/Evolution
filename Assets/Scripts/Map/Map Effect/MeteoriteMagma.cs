using UnityEngine;
using System.Collections;

public class MeteoriteMagma : MonoBehaviour {
    public int StartRound;
    private GameController gameController;

    private void Awake() {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        StartCoroutine(AppearCoroutine());
    }

    private void Update() {
        if (gameController.CurrentRound != StartRound) {
            StartCoroutine(DestroyCoroutine());
        }
    }
    IEnumerator AppearCoroutine() {
        Color color = gameObject.GetComponent<SpriteRenderer>().color;
        while (color.a < 1) {
            color.a += 0.01f;
            gameObject.GetComponent<SpriteRenderer>().color = color;
            yield return new WaitForSeconds(0.01f);
        }
    }
    IEnumerator DestroyCoroutine() {
        Color color = gameObject.GetComponent<SpriteRenderer>().color;
        while (color.a > 0) {
            color.a -= 0.01f;
            gameObject.GetComponent<SpriteRenderer>().color = color;
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(gameObject);
    }
}