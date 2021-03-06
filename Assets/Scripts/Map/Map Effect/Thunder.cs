using UnityEngine;

public class Thunder : MonoBehaviour {

    public float radius;
    public Collider2D[] objects;
    public GameController gameController;
    public Animator ThunderAnimation;
    public Animator ThunderWarning;
    public int Start_round;
    public AudioSource ThunderVoice;

    private void Awake() {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        Start_round = gameController.CurrentRound;
    }
    private void Update() {
        objects = Physics2D.OverlapCircleAll(transform.position, radius, 1 << LayerMask.NameToLayer("Player"));
    }

    public void CheakTrigger(bool RainOn) {
        objects = Physics2D.OverlapCircleAll(transform.position, radius, 1 << LayerMask.NameToLayer("Player"));
        if (gameController.CurrentRound != Start_round) {
            ThunderVoice.Play();
            ThunderAnimation.SetBool("ThunderDetermine", true);
            ThunderWarning.Play("Warning");
            foreach (var obj in objects) {
                PlayerUnit u = obj.gameObject.GetComponent<PlayerUnit>();
                if (u != null) {
                    if (!RainOn)
                        u.Damage(0.4f * u.MaxHealth);
                    else
                        u.Damage(u.MaxHealth);
                    u.controlled = true;
                    u.SetControl(1);
                }
            }
            Destroy(gameObject, 1.2f);
        }
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(1, 0, 0, 1);
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}