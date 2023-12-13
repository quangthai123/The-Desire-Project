using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    [SerializeField] private float radiusCheck;
    [SerializeField] private LayerMask groundAndWallLayer;
    private void Start()
    {
        //Destroy(gameObject, 10f);
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(-speed * Mathf.Sign(transform.rotation.y), 0f);
        if (Physics2D.OverlapCircle(transform.position, radiusCheck, groundAndWallLayer))
            Destroy(gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radiusCheck);
    }
}
