using UnityEngine;

public class Mushroom : MonoBehaviour {
    public Animator anim;

    private void OnCollisionEnter2D(Collision2D other) {
        anim.Play("mushroom");
    }
}