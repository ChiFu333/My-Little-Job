using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Saw : MonoBehaviour
{
    public float Speed = 1;
    public float deltaMove = 0;
    public Vector2 CycleMove;
    public AudioClip dead;

    private void Start()
    {
        StartCoroutine(Moving());
    }
    private IEnumerator Moving()
    {
        int lenght = (int)CycleMove.magnitude;
        Vector2 vec = CycleMove.normalized;
        while(true)
        {
            for(int i = 0; i < lenght; i++)
            {
                transform.Translate(vec * deltaMove);
                yield return new WaitForSeconds(1/Speed);
            }
            for(int i = 0; i < lenght; i++)
            {
                transform.Translate(-vec * deltaMove);
                yield return new WaitForSeconds(1/Speed);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            AudioManager.inst.Play(dead);
        }
    }
}
