using UnityEngine;

public class Star : MonoBehaviour
{
    public DayNight dayNight;

    public MeshRenderer referenceStar;

    Color c;

    private void Awake()
    {
        c = referenceStar.sharedMaterial.color;
        print(referenceStar.sharedMaterial);
    }

    private void Update()
    {
        referenceStar.sharedMaterial.SetColor("_EmissionColor", c * Mathf.Clamp((dayNight.progress - 0.4f) * 25, 0, 2.5f));
    }
}
