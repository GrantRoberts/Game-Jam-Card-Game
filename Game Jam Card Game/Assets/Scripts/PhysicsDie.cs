using System.Linq;
using TMPro;
using UnityEngine;

public class PhysicsDie : MonoBehaviour
{
    public TextMeshPro[] faces;

    [Header("DEBUG")]
    public bool DEBUGShowStatusViaColor = false;
    public TextMeshProUGUI displayOutput;

    [Header("Value")]
    public bool valueAccessable;
    public int value;

    public int modifier = 0;

    [Header("Rolling")]
    public Collider spawnBounds;
    Bounds bounds;
    public float rotationForce = 10;

    new Rigidbody rigidbody;
    new Renderer renderer;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        renderer = GetComponent<Renderer>();

        bounds = spawnBounds.bounds;

        RollDie();
    }

    [ContextMenu("Roll Die")]
    public void RollDie() => RollDie(rigidbody);

    public void RollDie(Rigidbody rb)
    {
        foreach (TextMeshPro face in faces)
        {
            face.text = Mathf.Max(0, int.Parse(face.name) + modifier).ToString();
        }
        rigidbody.constraints = 0;
        Vector3 torqueForce = new Vector3(Random.Range(-rotationForce, rotationForce), Random.Range(-rotationForce, rotationForce), Random.Range(-rotationForce, rotationForce));
        rb.AddRelativeTorque(torqueForce, ForceMode.Impulse);
        rb.AddForce(new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)) * 0.01f, ForceMode.Impulse);
        transform.position = new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z));
    }

    private void Update()
    {
        if(rigidbody.velocity == Vector3.zero)
        {
            valueAccessable = true;
            rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        }

        if (DEBUGShowStatusViaColor)
        {
            renderer.material.color = displayOutput ?
                (valueAccessable ? Color.yellow : Color.cyan) :
                (valueAccessable ? Color.green : Color.red);
        }

        value = GetResult();
    }

    public int GetResult()
    {
        if (valueAccessable)
        {
            var value = int.Parse(faces.Aggregate((face1, face2) => face1.transform.position.y > face2.transform.position.y ? face1 : face2).text);
            if (displayOutput)
                displayOutput.text = value.ToString();
            return value;
        }
        return -1;
    }

    public Rigidbody GetRigidbody() => rigidbody;
    public Renderer GetRenderer() => renderer;
}
