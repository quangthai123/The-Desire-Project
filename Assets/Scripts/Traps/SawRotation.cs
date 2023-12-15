using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawRotation : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 1f;

    private void Update()
    {
        transform.Rotate(0f, 0f, 360f * rotateSpeed * Time.deltaTime);
    }
}
