using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonTrigger : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float ClickOn;
    
    public void OnPointerDown (PointerEventData eventData) {
        ClickOn = 1;
    }
 
    public void OnPointerUp(PointerEventData eventData) {
        ClickOn = 0;
    }
}
