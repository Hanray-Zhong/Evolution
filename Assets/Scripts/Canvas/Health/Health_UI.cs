using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_UI : MonoBehaviour
{
    
    public GameObject Player;
    private void Update() {
        if (Player == null) {
            gameObject.SetActive(false);
            return;
        }
        // 血条跟随
        gameObject.transform.position = Player.transform.position + new Vector3(0, 0.9f);
        // 血条显示
        gameObject.GetComponent<Image>().fillAmount = Player.GetComponent<PlayerUnit>().Health / Player.GetComponent<PlayerUnit>().MaxHealth;
        Color oriColor = gameObject.GetComponent<Image>().color;
        gameObject.GetComponent<Image>().color = new Color(oriColor.r, oriColor.g, oriColor.b, Player.GetComponent<SpriteRenderer>().color.a);
    }
}
