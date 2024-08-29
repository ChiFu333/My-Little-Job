using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class ItemInCursor : MonoBehaviour
{
    private ItemHolder sender;
    private GameObject myItem;
    [SerializeField] private AudioClip putOut;
    public void CreateItem(GameObject other, ItemHolder holder)
    {
        sender = holder;
        if(myItem != null) Destroy(myItem); // стоит учеть возращение предмета
        myItem = Instantiate(other, transform);
        myItem.GetComponent<SpriteRenderer>().sortingOrder = 55;
        myItem.GetComponent<BoxCollider2D>().enabled = false;
        
    }   
    private void Update()
    {
        if (myItem != null)
        {
            Vector3 p = Input.mousePosition;
            p.z = 20;
            Vector2 pos = Camera.main.ScreenToWorldPoint(p);
            myItem.transform.position = ClampPos(pos);
            if(Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.BoxCast(myItem.transform.position, Vector2.one * 0.5f, 0, Vector2.zero,0,1);
                if (hit.collider == null)
                {
                    AudioManager.inst.Play(putOut);
                    myItem.transform.parent = null;
                    myItem.GetComponent<BoxCollider2D>().enabled = true;
                    myItem.GetComponent<SpriteRenderer>().sortingOrder = 5;
                    myItem = null;
                }
            }
            if(Input.GetMouseButtonDown(1))
            {
                Destroy(myItem);
                sender.itemCount++;
            }
        }
    }
    private Vector2 ClampPos(Vector2 pos)
    {
        float x = pos.x;
        float y = pos.y;
        if (x > 0f)
        {
            x -= (x + 0.35f) % 0.7f - 0.35f;
        }
        else if (x < 0 && x > -0.35f)
        {
            x = 0;
        }
        else
        {
            x -= (x + 0.35f) % 0.7f + 0.35f;
        }
        if (y > 0)
        {
            y -= (y + 0.35f) % 0.7f - 0.35f;
        }
        else if (y < 0 && y > -0.35f)
        {
            y = 0;
        }
        else
        {
            y -= (y + 0.35f) % 0.7f + 0.35f;
        }
        return new Vector2(x, y);
    }
}
