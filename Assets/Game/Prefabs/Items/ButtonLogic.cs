using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonLogic : MonoBehaviour
{
    [SerializeField] public UnityEvent eve;
    [SerializeField] public Sprite buttonOut;
    public void ButtonPressed()
    {
        eve.Invoke();
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = buttonOut;
    }
    
}
