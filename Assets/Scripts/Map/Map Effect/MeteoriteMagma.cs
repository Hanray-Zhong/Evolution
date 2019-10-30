using UnityEngine;
using System.Collections;

public class MeteoriteMagma : MonoBehaviour {
    public int StartRound;
    private Animator animator;
    private GameController gameController;

    private void Awake() {
        animator = gameObject.GetComponent<Animator>();
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
        animator.Play("MeteoriteMagmaDisappear");
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}