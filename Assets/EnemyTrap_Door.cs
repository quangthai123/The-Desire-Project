using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrap_Door : MonoBehaviour
{
    [SerializeField] private GameObject doorOfThisEnemyTrap;
    [SerializeField] private GameObject itemToControlDoor;
    [SerializeField] private GameObject foreground;
    [SerializeField] private GameObject contraintEnemiesTrap;
    [SerializeField] private bool rightDoor = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {      
        if (itemToControlDoor == null)
        {
            if(doorOfThisEnemyTrap != null)
                doorOfThisEnemyTrap.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && itemToControlDoor != null)
        {
            if(doorOfThisEnemyTrap != null)
                doorOfThisEnemyTrap.SetActive(true);
            foreground.SetActive(false);
            if(contraintEnemiesTrap != null)
                contraintEnemiesTrap.SetActive(false);  
        }
        else if(collision.gameObject.tag == "Player" && itemToControlDoor == null)
        {
            if (collision.gameObject.GetComponent<Player>().facingDirection == -1)
            {
                if (!rightDoor)
                    foreground.SetActive(true);
                else
                {
                    foreground.SetActive(false);
                    contraintEnemiesTrap.SetActive(false);
                }
            }
            else if (collision.gameObject.GetComponent<Player>().facingDirection == 1)
            {
                if (!rightDoor)
                {
                    contraintEnemiesTrap.SetActive(false);
                    foreground.SetActive(false);
                }
                else
                {
                    foreground.SetActive(true);
                }
            }
        }   
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && itemToControlDoor == null)
        {
            if (collision.gameObject.GetComponent<Player>().facingDirection == -1)
            {
                if (!rightDoor)
                    foreground.SetActive(true);
                else
                {
                    foreground.SetActive(false);
                }
            }
            else if (collision.gameObject.GetComponent<Player>().facingDirection == 1)
            {
                if (!rightDoor)
                {
                    foreground.SetActive(false);
                }
                else
                {
                    foreground.SetActive(true);
                }
            }
        }
    }
}
