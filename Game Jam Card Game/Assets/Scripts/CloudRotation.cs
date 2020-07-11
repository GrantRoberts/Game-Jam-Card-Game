using UnityEngine;

public class CloudRotation : MonoBehaviour
{
    public float speed = 2.5f;

    [ContextMenu("Set Cloud Rotation")]
    public void SetCloudRotation()
    {
        foreach (Transform cloud in transform)
        {
            cloud.LookAt(Vector3.zero);
            cloud.rotation = Quaternion.Euler(0, cloud.eulerAngles.y, cloud.eulerAngles.z);
        }
    }

    void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, speed * Time.deltaTime);
    }
}
