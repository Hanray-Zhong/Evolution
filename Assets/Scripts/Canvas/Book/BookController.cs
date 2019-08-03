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
        foreach (var name in Names) {
            name.SetActive(false);
        }
        foreach (var Introduction in Introductions) {
            Introduction.SetActive(false);
        }
        foreach (var Skill in Skills) {
            Skill.SetActive(false);
        }
        foreach (var CharacterImage in CharacterImages) {
            CharacterImage.SetActive(false);
        }
        if (CurrentIndex > 0) {
            CurrentIndex--;
        }
        Names[CurrentIndex].SetActive(true);
        Introductions[CurrentIndex].SetActive(true);
        Skills[CurrentIndex].SetActive(true);
        CharacterImages[CurrentIndex].SetActive(true);
    }
    public void ChangeRight() {
        foreach (var name in Names) {
            name.SetActive(false);
        }
        foreach (var Introduction in Introductions) {
            Introduction.SetActive(false);
        }
        foreach (var Skill in Skills) {
            Skill.SetActive(false);
        }
        foreach (var CharacterImage in CharacterImages) {
            CharacterImage.SetActive(false);
        }
        if (CurrentIndex < 2) {
            CurrentIndex++;
        }
        Names[CurrentIndex].SetActive(true);
        Introductions[CurrentIndex].SetActive(true);
        Skills[CurrentIndex].SetActive(true);
        CharacterImages[CurrentIndex].SetActive(true);
    }
}
