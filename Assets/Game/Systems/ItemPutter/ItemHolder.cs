using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemHolder : MonoBehaviour
{
    [SerializeField] private GameObject Item;
    [SerializeField] public int itemCount;
    [SerializeField] public TMP_Text countText;
    void Start()
    {
        
    }
    private void Update()
    {
        countText.text = "x " + (itemCount).ToString();
    }

    public void WhenClicked()
    {   
        if (itemCount != 0)
        {
            itemCount--;
            FindObjectOfType<ItemInCursor>().CreateItem(Item, this);
        }
        
    }
}
