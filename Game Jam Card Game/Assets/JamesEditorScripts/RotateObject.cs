using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public Vector3 rotateAround = Vector3.up;

    public float speed;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, rotateAround, speed * Time.deltaTime);
    }
}
