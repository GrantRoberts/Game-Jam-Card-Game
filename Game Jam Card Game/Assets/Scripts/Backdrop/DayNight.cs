using UnityEngine;

public class DayNight : MonoBehaviour
{

    public float speed = 1f;
    public Camera c;

    public Gradient skyGradient;

    public float progress;

    void Update()
    {
        transform.RotateAround(transform.position, Vector3.right, speed * Time.deltaTime);
        progress = transform.localEulerAngles.x + 90;
        progress = (progress > 180 ? progress - 360 : progress) / 180;
        c.backgroundColor = skyGradient.Evaluate(progress);
    }
}
