using System.Linq;
using TMPro;
using UnityEngine;

public class PhysicsDie : MonoBehaviour
{
    public bool showStatusViaColor = false;

    public int value;

    public Transform[] faces;

    public float rotationForce = 10;

    public float upwardsForce = 10;

    Rigidbody r;

    public TextMeshProUGUI displayOutput;

    bool valueAccessable;

    new Renderer renderer;

    void Awake()
    {
        r = GetComponent<Rigidbody>();
        renderer = GetComponent<Renderer>();
    }

    [ContextMenu("Roll Die")]
    public void RollDie() => RollDie(r);

    public void RollDie(Rigidbody rb)
    {
        rb.AddTorque(new Vector3(Random.Range(-rotationForce, rotationForce), Random.Range(-rotationForce, rotationForce), Random.Range(-rotationForce, rotationForce)));
        rb.AddForce(Vector3.up * Random.Range(upwardsForce * 0.75f, upwardsForce * 1.25f));
    }

    private void Update()
    {
        valueAccessable = (r.velocity == Vector3.zero);

        if (showStatusViaColor)
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
            var value = int.Parse(faces.Aggregate((face1, face2) => face1.position.y > face2.position.y ? face1 : face2).name);
            if (displayOutput)
                displayOutput.text = value.ToString();
            return value;
        }
        return -1;
    }
}
