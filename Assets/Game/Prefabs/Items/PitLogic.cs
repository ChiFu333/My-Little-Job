using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitLogic : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null && other.transform.CompareTag("Player"))
        {
            
            other.GetComponent<BotLogic>().fallInPit = true;
        }
    }
}
