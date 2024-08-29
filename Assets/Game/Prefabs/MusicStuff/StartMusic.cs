using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMusic : MonoBehaviour
{
    [SerializeField] AudioClip music;
    void Start()
    {
        if(AudioManager.inst != null) AudioManager.inst.PlayMusic(music);
    }
}
