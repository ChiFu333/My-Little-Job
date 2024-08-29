using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotLogic : MonoBehaviour
{
    [SerializeField] public int countToDo;
    [SerializeField] private AudioClip scanSound, foundSound, notFoundSound, moving, cleaning, falling, dancing, clicking;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private AnimationDataSO clean,dance;
    [SerializeField] private GameObject FoundEmotion, NoEmotion;
    [SerializeField] private GameObject WinPanel;
    private Vector2[] dirs = new Vector2[4] { Vector2.up, Vector2.right,Vector2.down,Vector2.left };
    private SpriteRenderer SR;

    private const float MININTERACTDISTANCE = 0.3f;
    private const float CELLLENGHT = 0.7f;
    private bool working = false;
    public bool fallInPit = false;

    private float timeToScan = 0.25f;
    private float timeToMove = 0.3f;
    private float timeToClean = 1.1f;

    private string[] distantTags = new string[1] { "Pot" };
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
    }
    public void StartScanning()
    {
        if(!working) StartCoroutine(Scaning());
    }
    private IEnumerator Scaning()
    {
        float minDist = 1000f;
        int minValue = -1;
        RaycastHit2D hit;
        GetComponent<Collider2D>().enabled = false;    
        for (int i = 0; i < 4; i++)
        {
            AudioManager.inst.Play(scanSound);
            SR.sprite = sprites[i];
            hit = Physics2D.Raycast(transform.position, dirs[i]);
            yield return new WaitForSeconds(timeToScan);
            if (hit.collider != null && !hit.transform.CompareTag("Wall"))
            {
                float dist = ((Vector2)hit.transform.position - (Vector2)transform.position).magnitude;
                if (minDist > dist)
                {
                    minDist = dist - 0.1f;
                    minValue = i;
                }
            }
        }
        SR.sprite = sprites[2];
        if (minValue != -1)
        {
            GetComponent<Collider2D>().enabled = true;
            AudioManager.inst.Play(foundSound);
            FoundEmotion.SetActive(true);
            yield return new WaitForSeconds(timeToScan);
            FoundEmotion.SetActive(false);
            SR.sprite = sprites[minValue];
            StartCoroutine(Move(dirs[minValue], CheckTags(Physics2D.Raycast(transform.position, dirs[minValue]).transform)));
        }
        else
        {
            AudioManager.inst.Play(notFoundSound);
            NoEmotion.SetActive(true);
        }
    }
    private bool CheckTags(Transform tr)
    {
        for (int i = 0; i < distantTags.Length; i++)
        {
            if (tr.CompareTag(distantTags[i]))
            {
                return true;
            }
        }
        return false;
    }
    private IEnumerator Move(Vector2 dir, bool distanteObject) 
    {
        while(true)
        {
            GetComponent<Collider2D>().enabled = false;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir);
            GetComponent<Collider2D>().enabled = true;
            float checkDist = MININTERACTDISTANCE;
            if (distanteObject) checkDist += CELLLENGHT;
            if(fallInPit)
            {
                AudioManager.inst.Play(falling);
                Destroy(gameObject);
                yield break;
            }
            if (((Vector2)hit.transform.position - (Vector2)transform.position).magnitude < checkDist)
            {
                StartCoroutine(Interact(dir));
                yield break;
            }
            transform.Translate(dir * CELLLENGHT);
            AudioManager.inst.Play(moving);
            yield return new WaitForSeconds(timeToMove);
            
        }
    }
    private IEnumerator Interact(Vector2 dir)
    {
        GetComponent<Collider2D>().enabled = false;
        Transform tr = Physics2D.Raycast(transform.position, dir).transform;
        GetComponent<Collider2D>().enabled = true;
        if (tr.CompareTag("Junk"))
        {
            AudioManager.inst.Play(cleaning);
            Destroy(tr.gameObject);
            GetComponent<BotAnimation>().SetAnimation(clean);
            yield return new WaitForSeconds(timeToClean);
            countToDo--;
        }
        else if (tr.CompareTag("Button"))
        {
            SR.sprite = sprites[2];
            AudioManager.inst.Play(clicking);
            tr.GetComponent<ButtonLogic>().ButtonPressed();
            yield return new WaitForSeconds(0.5f);
        }
        GetComponent<BotAnimation>().SetAnimation(null);
        if (countToDo == 0)
        {
            AudioManager.inst.Play(dancing);
            GetComponent<BotAnimation>().SetAnimation(dance);
            yield return new WaitForSeconds(1.5f);
            WinPanel.SetActive(true);
        }
        else
        {
            StartCoroutine(Scaning());
        }
        
    }
}
