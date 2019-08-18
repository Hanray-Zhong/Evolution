using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonTrigger : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float ClickOn;
    public MoveCamera MainCamera;
    
    public void OnPointerDown (PointerEventData eventData) {
        MainCamera.IsFollowing = false;
        ClickOn = 1;
    }
 
    public void OnPointerUp(PointerEventData eventData) {
        MainCamera.IsFollowing = true;
        ClickOn = 0;
    }
}
