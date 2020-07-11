using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildrenToCenter : MonoBehaviour
{
    [ContextMenu("Look In")]
    public void LookIn()
    {
        foreach (Transform t in transform)
        {
            t.LookAt(transform);
        }
    }
}
