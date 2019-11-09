using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    public AudioClip[] music;
    private bool canChange;
    private AudioSource audioSource;

    private static MusicController instance = null;
    public static MusicController Instance {
        get { return instance; }
    }

    void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } 
        else {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);//使对象目标在加载新场景时不被自动销毁。
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    private void Update() {
        if ((SceneManager.GetActiveScene().buildIndex == 0  || 
             SceneManager.GetActiveScene().buildIndex == 1) && audioSource.clip != music[0]) {
                ChangeMusic(music[0]);
            }
        else if ((SceneManager.GetActiveScene().buildIndex == 3) && audioSource.clip != music[1]) {
                ChangeMusic(music[1]);
        }
        if (canChange) {
            audioSource.volume -= 0.03f;
        }
    }

    private void ChangeMusic(AudioClip music) {
        canChange = true;
        StartCoroutine(Change(music));
    }

    IEnumerator Change(AudioClip music) {
        yield return new WaitForSeconds(1);
        canChange = false;
        audioSource.volume = 1;
        audioSource.clip = music;
        audioSource.Play();
    }
}
