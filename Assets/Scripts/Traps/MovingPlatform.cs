using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform[] wayPoints;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float coolDown;
    private Rigidbody2D rb;
    private float coolDownTimer;
    private int currentPoint;
    // Start is called before the first frame update
    void Start()
    {
        currentPoint = 0;
        transform.position = wayPoints[0].position;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        coolDownTimer -= Time.deltaTime;
        if (coolDownTimer <= 0f)
        {
            transform.position = Vector2.MoveTowards(transform.position, wayPoints[currentPoint].position, Time.deltaTime * speed);
        }
        if (Vector2.Distance(transform.position, wayPoints[currentPoint].position) < .1f)
        {
            coolDownTimer = coolDown;
            currentPoint++;
            if (currentPoint >= wayPoints.Length)
            {
                currentPoint = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && gameObject.tag != "Saw")
        {
            collision.gameObject.transform.SetParent(transform);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && gameObject.tag != "Saw")
        {
            collision.gameObject.transform.SetParent(null);
        }
    }

    private void OnDrawGizmos()
    {
        for(int i=0; i<wayPoints.Length; i++)
        {
            Gizmos.DrawLine(transform.position, wayPoints[i].position);
        }
    }
}
