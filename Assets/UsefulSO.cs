using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Useful", menuName = "Useful/SO")]
public class UsefulSO : ScriptableObject
{
    public void PlaySound(AudioClip clip)
    {
        if(AudioManager.inst != null) AudioManager.inst.Play(clip);
    }
    public void RememberLevel(int level)
    {
        if (LevelRememberer.inst != null) LevelRememberer.inst.completedLevels[level - 1] = true;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
