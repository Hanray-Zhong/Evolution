using UnityEngine;
using System.Collections;

public class Meteorite : MonoBehaviour {
    public float radius;
    private Collider2D[] objects;
    private GameController gameController;
    public Animator MeteoriteAnimation;
    public Animator MeteoriteWarning;
    public AudioSource MeteoriteVoice;
    public GameObject MagmaPrefab;
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
            // MeteoriteVoice.Play();
            MeteoriteWarning.Play("MeteoriteWarningEnd");
            MeteoriteAnimation.SetBool("MeteoriteDetermine", true);
            StartCoroutine(WarningFade());
            foreach (var obj in objects) {
                PlayerUnit u = obj.gameObject.GetComponent<PlayerUnit>();
                if (u != null) {
                    u.Damage(0.3f * u.MaxHealth);
                }
            }
            GameObject Magma = Instantiate(MagmaPrefab, gameObject.transform.position, Quaternion.identity);
            Magma.GetComponent<MeteoriteMagma>().StartRound = gameController.CurrentRound;
            Destroy(gameObject, 3);
        }
    }
    IEnumerator WarningFade() {
        Color color = gameObject.GetComponent<SpriteRenderer>().color;
        while (color.a > 0) {
            color.a -= 0.01f;
            gameObject.GetComponent<SpriteRenderer>().color = color;
            yield return new WaitForSeconds(0.01f);
        }
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(1, 0, 0, 1);
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}