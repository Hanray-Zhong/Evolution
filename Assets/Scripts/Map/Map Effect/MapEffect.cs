using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapEffect : MonoBehaviour
{
    public GameController gameController;
    public ItemGenerator Generator;
    [Header("Thunder")]
    public bool ThunderReady;
    public GameObject ThunderPrefab;
    public GameObject[] ThunderObjs;
    [Header("Rain")]
    public bool RainOn;
    public Rain rain;
    [Header("UI")]
    public Sprite[] Sprites;
    public Image WeatherUI;


    public void InitPerRound() {
        // Thunder
        if (gameController.CurrentRound % 5 != 3) {
            ThunderReady = true;
        }
        if (gameController.CurrentRound % 5 == 3 && ThunderReady) {
            Thunder();
        }
        DetermineThunder(RainOn);
        // Rain
        if (gameController.CurrentRound % 6 == 0) {
            RainOn = true;
        }
        else {
            RainOn = false;
        }
        rain.DetermineRain(RainOn);
        // UI
        if (!ThunderReady && RainOn) {
            WeatherUI.sprite = Sprites[0];
            return;
        }
        else if (ThunderReady && RainOn) {
            WeatherUI.sprite = Sprites[1];
            return;
        }
        else if (!ThunderReady && !RainOn) {
            WeatherUI.sprite = Sprites[2];
            return;
        }
        else if (ThunderReady && !RainOn) {
            WeatherUI.sprite = Sprites[3];
        }
    }


    private void Thunder() {
        if (ThunderReady) {
            Generator.GenerateRandom(5, ThunderPrefab); 
            ThunderReady = false;
        }
    }
    public void DetermineThunder(bool rainOn) {
        ThunderObjs = GameObject.FindGameObjectsWithTag("Thunder");
        foreach (var item in ThunderObjs) {
            item.GetComponent<Thunder>().CheakTrigger(rainOn);
        }
    }

}
