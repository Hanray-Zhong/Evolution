using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemController : MonoBehaviour {	
	public GameObject BlackImage;

	void Update () {
		if (Input.GetKey(KeyCode.Escape)) {
			Application.Quit();
		}
	}
	public void LoadScene(int i) {
		BlackImage.SetActive(true);
		BlackImage.GetComponent<Fade>().CanFade = true;
		StartCoroutine(Load(i));
	}
	IEnumerator Load(int i) {
		yield return new WaitForSeconds(1.5f);
		SceneManager.LoadScene(i);
	}
	public void QuitGame() {
        Application.Quit();
    }
}
