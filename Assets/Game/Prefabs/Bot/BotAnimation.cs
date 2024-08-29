using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UnityEngine;

public class BotAnimation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer SR;
    private AnimationDataSO currentAnim = null;
    private bool stopFlag = false;
    void Start()
    {
        //SetAnimation(mydata);
    }
    public void SetAnimation(AnimationDataSO data)
    {
        if(currentAnim != data) 
        {
            currentAnim = data;
            PlayAnimaion(currentAnim, 0);
        }
    }
    private async void PlayAnimaion(AnimationDataSO data, int currentFrame)
    {
        if (data == null) return;
        currentFrame %= data.frames.Count;
        if (SR == null) return;
        SR.sprite = data.frames[currentFrame];
        Timer timer = new Timer();
        timer.SetFrequency(data.framerate);
        while (!timer.Execute())
        {
            if (currentAnim != data || stopFlag)
            {
                return;
            }
            await Task.Yield();
        }
        currentFrame++;
        
        PlayAnimaion(data, currentFrame);
    }
    private void OnApplicationQuit()
    {
        stopFlag = true;
    }

}
