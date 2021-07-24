using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UiClickHandler : MonoBehaviour, IPointerClickHandler
{
    // add callbacks in the inspector like for buttons
    public UnityEvent onClick;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        // invoke your event
        onClick.Invoke();
    }
}
