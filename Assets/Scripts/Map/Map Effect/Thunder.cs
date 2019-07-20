using UnityEngine;

public class Thunder : MonoBehaviour {

    public float radius;
    public Collider2D[] objects;
    public GameController gameController;
    public int Start_round;

    private void Awake() {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        Start_round = gameController.CurrentRound;
    }
    private void Update() {
        objects = Physics2D.OverlapCircleAll(transform.position, radius, 1 << LayerMask.NameToLayer("Player"));
    }

    public void CheakTrigger() {
        objects = Physics2D.OverlapCircleAll(transform.position, radius, 1 << LayerMask.NameToLayer("Player"));
        if (gameController.CurrentRound != Start_round) {
            foreach (var obj in objects) {
                PlayerUnit u = obj.gameObject.GetComponent<PlayerUnit>();
                if (u == null)
                    return;
                u.Damage(0.4f * u.MaxHealth);
                u.controlled = true;
                u.SetControl(1);
            }
            Destroy(gameObject);
        }
        if (gameController.CurrentRound != Start_round && objects.Length == 0) {
            Debug.Log("get");
            Destroy(gameObject);
        }
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(1, 0, 0, 1);
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}