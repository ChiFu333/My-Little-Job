using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private int num;
    [SerializeField] private GameObject check;
    
    void Start()
    {
        LevelRememberer LR = FindObjectOfType<LevelRememberer>();
        if(LR != null && LR.completedLevels[num-1]) check.SetActive(true);
    }
    public void LoadMyLevel()
    {
        FindObjectOfType<SceneLoader>().LoadScene("Level"+num.ToString());
    }

}
