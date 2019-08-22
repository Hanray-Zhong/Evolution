using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapEffect : MonoBehaviour
{
    public GameController gameController;
    public ItemGenerator Generator;
    [Header("Thunder")]
    public bool HavaThunder = true;
    public bool ThunderReady;
    public GameObject ThunderPrefab;
    public GameObject[] ThunderObjs;
    [Header("Rain")]
    public bool HavaRain = true;
    public bool RainOn;
    public GameObject[] Players;
    public Animator RainAnim;
    public GameObject RainEffect;
    [Header("UI")]
    public Sprite[] Sprites;
    public Image WeatherUI;


    public void InitPerRound() {
        // Thunder
        if (HavaThunder) {
            InitPerRound_Thunder();
        }
        // Rain
        if (HavaRain) {
            InitPerRound_Rain();
        }
        // UI
        InitPerRound_UI();
    }

    private void InitPerRound_Thunder() {
        if (gameController.CurrentRound % 5 != 3) {
            ThunderReady = true;
        }
        if (gameController.CurrentRound % 5 == 3 && ThunderReady) {
            if (ThunderReady) {
                Generator.GenerateRandom(5, ThunderPrefab); 
                ThunderReady = false;
            }
        }
        ThunderObjs = GameObject.FindGameObjectsWithTag("Thunder");
        foreach (var item in ThunderObjs) {
            item.GetComponent<Thunder>().CheakTrigger(RainOn);
        }
    }
    private void InitPerRound_Rain() {
        if (gameController.CurrentRound % 6 == 0) {
            RainEffect.SetActive(true);
            if (!RainOn) {
                RainAnim.Play("SunToRain");
            }
            RainOn = true;
        }
        else {
            RainEffect.SetActive(false);
            if (RainOn) {
                RainAnim.Play("RainToSun");
            }
            RainOn = false;
        }
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

    private void InitPerRound_UI() {
        if (HavaThunder && HavaRain) {
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
    }
}
