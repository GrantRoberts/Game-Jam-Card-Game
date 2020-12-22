using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[ExecuteInEditMode]
public class TriggerDisplay : MonoBehaviour {

    public Color[] triggerColour = new Color[1];
    public Color triggerWireColour;

    private BoxCollider[] bca;

    private void OnEnable()
    {
        bca = GetComponents<BoxCollider>();
    }

    private void OnDrawGizmos()
    {
        RedrawBox();
    }
    private void OnDrawGizmosSelected()
    {
        RedrawBox();
    }

    private void RedrawBox()
    {

        for (int i = 0; i < bca.Length; i++)
        {
            BoxCollider bc = bca[i];
            if (bc != null)
            {
                Vector3 drawBoxScale = new Vector3(transform.lossyScale.x * bc.size.x, transform.lossyScale.y * bc.size.y, transform.lossyScale.z * bc.size.z);
                Vector3 tempScale = transform.worldToLocalMatrix.MultiplyPoint(transform.lossyScale);
                Vector3 drawBoxPosition = transform.localToWorldMatrix.MultiplyPoint(bc.center);

                Gizmos.matrix = Matrix4x4.TRS(drawBoxPosition, transform.rotation, drawBoxScale);
                Gizmos.color = triggerColour[i % triggerColour.Length];
                Gizmos.DrawCube(Vector3.zero, Vector3.one);
                Gizmos.color = triggerWireColour;
                Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
            }
        }

    }
}

