using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRememberer : MonoBehaviour
{
    public bool[] completedLevels = new bool[24];
    public static LevelRememberer inst;
    private void Awake()
    {
        if (inst == null)
        {
            inst = this;
            DontDestroyOnLoad(inst);
            for(int i = 0; i < 24; i++)
            {
                completedLevels[i] = false;
            }
        }
        else
        {
            Destroy(gameObject);
        }  
    }
}
