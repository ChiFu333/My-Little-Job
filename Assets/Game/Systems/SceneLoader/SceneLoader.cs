using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private float loadTime = 0.5f;
    [SerializeField] private AnimationClip fadeoutClip;
    private Animation anim;
    private void Start()
    {
        anim = GetComponent<Animation>();
    }   
    public void LoadScene(string sceneName)
    {
        StartCoroutine(Loading(sceneName));
    }
    private IEnumerator Loading(string name)
    {
        anim.clip = fadeoutClip;
        anim.Play();
        yield return new WaitForSeconds(loadTime);
        SceneManager.LoadScene(name);
    }
    public void ReloadScene()
    {
        StartCoroutine(Loading(SceneManager.GetActiveScene().name));
    }
}
