using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Twinkling : MonoBehaviour
{
    private CanvasGroup canvasGroup;
	private float fadeSpeed;
	private void Start() {
		canvasGroup = GetComponent<CanvasGroup>();
	}
    void Update () {
		if (canvasGroup.alpha == 0) 
			fadeSpeed = 0.01f;
		else if (canvasGroup.alpha == 1)
			fadeSpeed = -0.01f;
		
		canvasGroup.alpha += fadeSpeed;
	}
}
