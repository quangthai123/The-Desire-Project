using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicTrap : MonoBehaviour
{
    [SerializeField] private Transform magicBulletPrefab;
    [SerializeField] private Transform shootingPos;
    [SerializeField] private float shootingCooldown;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Shooting", shootingCooldown, shootingCooldown);
        //Debug.Log(transform.localEulerAngles.z);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void Shooting()
    {
        float zAxis = transform.localEulerAngles.z;
        Transform newBullet = Instantiate(magicBulletPrefab, shootingPos.position, Quaternion.Euler(0f, 0f, zAxis + 90f));
    }
}
;