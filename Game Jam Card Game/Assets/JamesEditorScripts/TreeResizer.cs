using UnityEngine;

public class TreeResizer : MonoBehaviour
{
    [Min(0)]
    public float maxSize = 1;
    [Min(0)]
    public float minSize = 1;

    [Space(20)]

    [Min(0)]
    public float maxYPos = 0;

    [ContextMenu("Resize trees")]
    public void Resize()
    {
        if (minSize >= maxSize)
        {
            Debug.Log("Invaild starting sizes!");
            return;
        }

        foreach (Transform child in transform)
        {
            float horizontalScale = Random.Range(minSize, maxSize);
            float verticalScale = Random.Range(minSize, maxSize);
            child.localScale = new Vector3(horizontalScale, verticalScale, horizontalScale);
            child.transform.position = new Vector3(child.transform.position.x, Random.Range(0, maxYPos), child.transform.position.z);
        }
    }
}
