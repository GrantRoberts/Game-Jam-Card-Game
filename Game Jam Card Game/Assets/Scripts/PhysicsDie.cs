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

    [Header("Force")]
    public float rotationForce = 10;
    public float upwardsForce = 10;

    new Rigidbody rigidbody;
    new Renderer renderer;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        renderer = GetComponent<Renderer>();

        RollDie();
    }

    [ContextMenu("Roll Die")]
    public void RollDie() => RollDie(rigidbody);

    public void RollDie(Rigidbody rb)
    {
        foreach (TextMeshPro face in faces)
        {
            face.text = Mathf.Max(0, int.Parse(face.name) + modifier).ToString();
            print($"{face} mapped to {face.text}");
        }
        rb.AddTorque(new Vector3(Random.Range(-rotationForce, rotationForce), Random.Range(-rotationForce, rotationForce), Random.Range(-rotationForce, rotationForce)));
        rb.AddForce(Vector3.up * Random.Range(upwardsForce * 0.75f, upwardsForce * 1.25f));
    }

    private void Update()
    {
        valueAccessable = (rigidbody.velocity == Vector3.zero);

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

    public Rigidbody GetRigidbody() { return rigidbody; }
    public Renderer GetRenderer() { return renderer; }
}
