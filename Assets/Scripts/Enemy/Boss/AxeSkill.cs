using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeSkill : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = -transform.right * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<Player>().stateMachine.currentState != collision.gameObject.GetComponent<Player>().dashState
                 && collision.gameObject.GetComponent<Player>().stateMachine.currentState != collision.gameObject.GetComponent<Player>().airDashState && collision.gameObject.GetComponent<Player>().stateMachine.currentState != collision.gameObject.GetComponent<Player>().blockState)
            {
                AudioManager.instance.playerSFX(16);
                collision.gameObject.GetComponent<Player>().BeDamaged(70, transform.position);      
            }
            if(collision.gameObject.GetComponent<Player>().stateMachine.currentState == collision.gameObject.GetComponent<Player>().blockState)
            {
                AudioManager.instance.playerSFX(10);
                collision.gameObject.GetComponent<Player>().isKnocked = true;
                collision.gameObject.GetComponent<Player>().LightlyPushingPlayer(transform.position);   
            }

        }
    }
}
