using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBullet : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private float timeToSpawnThisBullet;
    void Start()
    {
        Destroy(gameObject, 10f);
        rb = GetComponent<Rigidbody2D>();
        timeToSpawnThisBullet = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right * speed;
            //transform.Translate(transform.right * speed * Time.deltaTime, 0f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 3 || collision.gameObject.layer == 6)
        {
            if(Time.time - timeToSpawnThisBullet > 1f)
                Destroy(gameObject);
        }
    }
}
