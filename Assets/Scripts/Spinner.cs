using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 0;

    void Update()
    {
        Vector3 rotate = Vector3.up * rotationSpeed * Time.deltaTime;
        transform.Rotate(rotate);
    }

}
