using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    Animator anim;
    public string checkpointId;
    public bool activated;

    private void Start()
    {
        anim=GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>()!=null)
        {
            ActivateCheckpoint();
        }
    }


    [ContextMenu("GenerateId")]
    private void GenerateId()
    {
        checkpointId=System.Guid.NewGuid().ToString();
    }

    public void ActivateCheckpoint()
    {
        activated=true;
        anim.SetBool("active", true);
    }
}
