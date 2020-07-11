using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarRandomiser : MonoBehaviour
{
    public float minHeight, maxHeight;

    [ContextMenu("Randomise star height")]
    public void Resize()
    {
        if (minHeight >= maxHeight)
        {
            Debug.Log("Invaild starting heights!");
            return;
        }

        foreach (Transform child in transform)
        {
            child.transform.position = new Vector3(child.transform.position.x, Random.Range(minHeight, maxHeight), child.transform.position.z);
        }
    }
}
