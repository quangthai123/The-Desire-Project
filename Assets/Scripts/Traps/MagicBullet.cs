using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBullet : MonoBehaviour
{
    [SerializeField] private float speed;
    void Start()
    {
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.right * speed * Time.deltaTime, 0f);
    }
    
}
