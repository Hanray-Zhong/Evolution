using UnityEngine;

public class Rain : MonoBehaviour {
    public MapEffect MapEffect;
    public GameObject[] Players;

    
    public void DetermineRain(bool RainOn) {
        if (RainOn) {
            foreach (var player in Players) {
                player.GetComponent<Rigidbody2D>().drag = 0.7f * player.GetComponent<PlayerUnit>().Drag;
            }
        }
        else {
            foreach (var player in Players) {
                player.GetComponent<Rigidbody2D>().drag = player.GetComponent<PlayerUnit>().Drag;
            }
        }
    }
}