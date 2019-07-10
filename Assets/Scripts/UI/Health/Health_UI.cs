using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_UI : MonoBehaviour
{
    public GameObject Player;
    public GameObject MainCamera;
    private void Update() {
        if (Player == null) {
            return;
        }
        // 血条跟随
        gameObject.transform.position = MainCamera.GetComponent<Camera>().WorldToScreenPoint(Player.transform.position);
        // 血条显示
        gameObject.GetComponent<Image>().fillAmount = Player.GetComponent<PlayerUnit>().Health / Player.GetComponent<PlayerUnit>().MaxHealth;
    }
}
