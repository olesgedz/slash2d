using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PointerListener : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
     // Define a property so that other classes can know whether the button is pressed
     public bool IsPressed
     {
        get ;
        private set ;
     }
    PointerListener()
    {
        IsPressed = false;
    }
     public void OnPointerDown(PointerEventData eventData)
     {
        IsPressed = true;
     }

     public void OnPointerUp(PointerEventData eventData)
     {
        IsPressed = false;
     }

     public void OnPointerExit(PointerEventData eventData)
     {
        IsPressed = false;
     }
}