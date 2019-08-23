using UnityEngine;

public class Magma : MonoBehaviour {
    public float Damage;
    public float DamagePara;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            PlayerUnit u = other.GetComponent<PlayerUnit>();
            if (u != null) {
                u.Damage(Damage + DamagePara * u.MaxHealth);
            }
        }
    }
}