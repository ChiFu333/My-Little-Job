using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] public int FishCount = 0;
    [SerializeField] public int WallCount = 0;
    [SerializeField] public ItemHolder FishHolder;
    [SerializeField] public ItemHolder WallHolder;
    void Start()
    {
        if(FishCount == 0)
        {
            FindObjectOfType<StartButton>().holders.Remove(FishHolder);
            FishHolder.gameObject.SetActive(false);
        }
        else
        {
            FishHolder.itemCount = FishCount;
        }
        if(WallCount == 0)
        {
            FindObjectOfType<StartButton>().holders.Remove(WallHolder);
            WallHolder.gameObject.SetActive(false);
        }
        else
        {
            WallHolder.itemCount = WallCount;
        }
        
        int c = FishCount + FindObjectsByType<Junk>(FindObjectsSortMode.None).Length;
        FindObjectOfType<BotLogic>().countToDo = c;
    }

}
