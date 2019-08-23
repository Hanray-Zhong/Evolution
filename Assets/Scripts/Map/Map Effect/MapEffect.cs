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
    [Header("DynamicMagma")]
    public bool HaveDynamicMagma = true;
    public bool Magma_On;
    public GameObject DynamicMagma;
    [Header("Meteorite")]
    public bool HaveMeteorite = true;
    public bool MeteoriteReady;
    public GameObject MeteoritePrefab;
    public GameObject[] MeteoriteObjs;
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
        // Map_2
        if (HaveDynamicMagma) {
            InitPerRound_DynamicMagma();
        }
        if (HaveMeteorite) {
            InitPerRound_Meteorite();
        }
        // UI
        InitPerRound_UI();
    }

    private void InitPerRound_Thunder() {
        if (gameController.CurrentRound % 5 != 3) {
            ThunderReady = true;
        }
        if (gameController.CurrentRound % 5 == 3 && ThunderReady) {
            if (Generator == null) {
                Debug.LogError("Error : There is no Generator");
                return;
            }
            Generator.GenerateRandom(5, ThunderPrefab); 
            ThunderReady = false;
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
    private void InitPerRound_DynamicMagma() {
        if (gameController.CurrentRound % 3 == 0) {
            if (!Magma_On) {
                Magma_On = true;
                StartCoroutine(DynamicMagma_On(DynamicMagma));
            }
        }
        else {
            if (Magma_On) {
                StartCoroutine(DynamicMagma_Off(DynamicMagma));
                Magma_On = false;
            }
        }
    }
    IEnumerator DynamicMagma_On(GameObject DynamicMagma) {
        DynamicMagma.SetActive(true);
        Color color = DynamicMagma.GetComponent<SpriteRenderer>().color;
        while (color.a < 1) {
            color.a += 0.01f;
            DynamicMagma.GetComponent<SpriteRenderer>().color = color;
            yield return new WaitForSeconds(0.005f);
        }
    }
    IEnumerator DynamicMagma_Off(GameObject DynamicMagma) {
        Color color = DynamicMagma.GetComponent<SpriteRenderer>().color;
        while (color.a > 0) {
            color.a -= 0.01f;
            DynamicMagma.GetComponent<SpriteRenderer>().color = color;
            yield return new WaitForSeconds(0.005f);
        }
        DynamicMagma.SetActive(false);
    }
    private void InitPerRound_Meteorite() {
        if (gameController.CurrentRound % 5 != 3) {
            MeteoriteReady = true;
        }
        if (gameController.CurrentRound % 5 == 3 && MeteoriteReady) {
            if (Generator == null) {
                Debug.LogError("Error : There is no Generator");
                return;
            }
            Generator.GenerateRandom(4, MeteoritePrefab); 
            MeteoriteReady = false;
        }
        MeteoriteObjs = GameObject.FindGameObjectsWithTag("Meteorite");
        foreach (var item in MeteoriteObjs) {
            item.GetComponent<Meteorite>().CheakTrigger();
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
