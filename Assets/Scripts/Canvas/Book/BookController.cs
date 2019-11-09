using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookController : MonoBehaviour
{
    public GameObject[] Names;
    public GameObject[] Introductions;
    public GameObject[] Skills;
    public GameObject[] CharacterImages;
    public int CurrentIndex;

    public void ChangeLeft() {
        if (CurrentIndex > 0) {
            CurrentIndex--;
        }
        if (Names.Length != 0) {
            foreach (var name in Names) {
                name.SetActive(false);
            }
            Names[CurrentIndex].SetActive(true);
        }
        if (Introductions.Length != 0) {
            foreach (var Introduction in Introductions) {
                Introduction.SetActive(false);
            }
            Introductions[CurrentIndex].SetActive(true);
        }
        if (Skills.Length != 0) {
            foreach (var Skill in Skills) {
                Skill.SetActive(false);
            }
            Skills[CurrentIndex].SetActive(true);
        }
        if (CharacterImages.Length != 0) {
            foreach (var CharacterImage in CharacterImages) {
                CharacterImage.SetActive(false);
            }
            CharacterImages[CurrentIndex].SetActive(true);
        }
    }
    public void ChangeRight() {
        if (CurrentIndex < Names.Length - 1) {
            CurrentIndex++;
        }
        if (Names.Length != 0) {
            foreach (var name in Names) {
                name.SetActive(false);
            }
            Names[CurrentIndex].SetActive(true);
        }
        if (Introductions.Length != 0) {
            foreach (var Introduction in Introductions) {
                Introduction.SetActive(false);
            }
            Introductions[CurrentIndex].SetActive(true);
        }
        if (Skills.Length != 0) {
            foreach (var Skill in Skills) {
                Skill.SetActive(false);
            }
            Skills[CurrentIndex].SetActive(true);
        }
        if (CharacterImages.Length != 0) {
            foreach (var CharacterImage in CharacterImages) {
                CharacterImage.SetActive(false);
            }
            CharacterImages[CurrentIndex].SetActive(true);
        }
    }
}
