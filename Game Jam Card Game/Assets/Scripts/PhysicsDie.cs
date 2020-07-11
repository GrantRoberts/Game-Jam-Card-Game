using System.Linq;
using TMPro;
using UnityEngine;

public class PhysicsDie : MonoBehaviour
{
    public Transform[] faces;

    [Header("DEBUG")]
    public bool DEBUGShowStatusViaColor = false;
    public TextMeshProUGUI displayOutput;

    [Header("Value")]
    public bool valueAccessable;
    public int value;

    [Header("Force")]
    public float rotationForce = 10;
    public float upwardsForce = 10;

    Rigidbody r;
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
            var value = int.Parse(faces.Aggregate((face1, face2) => face1.position.y > face2.position.y ? face1 : face2).name);
            if (displayOutput)
                displayOutput.text = value.ToString();
            return value;
        }
        return -1;
    }
}
