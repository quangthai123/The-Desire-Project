using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private Transform arrowPrefab;
    [SerializeField] private Transform lowSpawnPos;
    [SerializeField] private Transform highSpawnPos;
    [SerializeField] private bool facingRight = false;
    [SerializeField] private float idldTimeMin;
    [SerializeField] private float idldTimeMax;
    private Animator anim;
    void Start()
    {
        if (facingRight)
            transform.Rotate(0f, 180f, 0f);
        anim = GetComponent<Animator>();
        SelectRandomAttackPos();
    }
    private void SelectRandomAttackPos()
    {
        int rd = Random.Range(0, 2);
        if (rd == 0)
            SetLowAttack();
        else SetHighAttack();
    }
    private void SetLowAttack()
    {
        anim.SetTrigger("low");
        float rd = Random.Range(idldTimeMin, idldTimeMax);
        Invoke("SelectRandomAttackPos", rd);
    }
    private void SetHighAttack()
    {
        anim.SetTrigger("high");
        float rd = Random.Range(idldTimeMin, idldTimeMax);
        Invoke("SelectRandomAttackPos", rd);
    }
    private void ShootingTriggerLowPos()
    {
        if (facingRight)
            Instantiate(arrowPrefab, lowSpawnPos.position, Quaternion.Euler(0f, 180f, 0f));
        else
            Instantiate(arrowPrefab, lowSpawnPos.position, Quaternion.identity);
    }
    private void ShootingTriggerHighPos()
    {
        if (facingRight)
            Instantiate(arrowPrefab, highSpawnPos.position, Quaternion.Euler(0f, 180f, 0f));
        else
            Instantiate(arrowPrefab, highSpawnPos.position, Quaternion.identity);
    }
}
