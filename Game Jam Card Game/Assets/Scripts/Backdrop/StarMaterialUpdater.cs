using UnityEngine;

public class StarMaterialUpdater : MonoBehaviour
{
    public DayNight dayNight;

    public MeshRenderer referenceStar;

    Color c;

    private void Awake()
    {
        c = referenceStar.sharedMaterial.color;
    }

    private void Update()
    {
        referenceStar.sharedMaterial.color = new Color(c.r, c.g, c.b, Mathf.Clamp((dayNight.progress - 0.4f)*10, 0, 1));
        referenceStar.sharedMaterial.SetColor("_EmissionColor", c * Mathf.Clamp((dayNight.progress - 0.4f) * 25, 0, 2.5f));
    }
}
