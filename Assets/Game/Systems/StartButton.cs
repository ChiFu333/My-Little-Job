using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    [SerializeField] public List<ItemHolder> holders;
    private bool OneStart = true;
    public void StartGame()
    {
        if (!OneStart) return;
        for(int i = 0; i < holders.Count; i++)
        {
            if (holders[i].itemCount != 0) return;
           
        }
        OneStart = false;
        FindObjectOfType<BotLogic>().StartScanning();
    }
}
