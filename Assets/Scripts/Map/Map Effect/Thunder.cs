using UnityEngine;

public class Thunder : MonoBehaviour {

    public GameController gameController;
    public int Start_Turn;

    private void Awake() {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    
    private void OnTriggerStay2D(Collider2D other) {
        if (other.tag != "Player") {
            return;
        }
        // Debug.Log("CurrentTurn: " + gameController.CurrentTurn + ", " + (gameController.CurrentTurn % 6));
        if (gameController.CurrentTurn != Start_Turn && ((gameController.CurrentTurn - 1) % 6) == 0) {
            Debug.Log("Get");
            PlayerUnit u = other.gameObject.GetComponent<PlayerUnit>();
            if (u == null)
                return;
            u.Damage(0.4f * u.MaxHealth);
            Destroy(gameObject);
        }
    }
}