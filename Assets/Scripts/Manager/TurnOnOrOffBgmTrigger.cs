using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnOrOffBgmTrigger : MonoBehaviour
{
    private Vector2 playerPos;
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        if (playerPos.x < transform.position.x && playerPos.y > transform.position.y)
            AudioManager.instance.playBgm = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(collision.gameObject.GetComponent<Player>().facingDirection==-1)
                AudioManager.instance.playBgm = false;
            else AudioManager.instance.playBgm = true;
        }
    }
}
