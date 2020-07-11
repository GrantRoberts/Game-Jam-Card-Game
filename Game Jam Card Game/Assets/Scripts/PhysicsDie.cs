using System.Linq;
using TMPro;
using UnityEngine;

public class PhysicsDie : MonoBehaviour
{
    public int value;

    public Transform[] faces;

    public int rotationForce = 5000;

    Rigidbody r;

    public TextMeshProUGUI displayOutput;

    bool valueAccessable;

    void Awake()
    {
        r = GetComponent<Rigidbody>();
    }

    [ContextMenu("Roll Die")]
    public void RollDie() => RollDie(r);

    public void RollDie(Rigidbody rb)
    {
        rb.AddTorque(new Vector3(Random.Range(-rotationForce, rotationForce), Random.Range(-rotationForce, rotationForce), Random.Range(-rotationForce, rotationForce)));
        rb.AddForce(Vector3.up * Random.Range(400, 500));
    }

    private void Update()
    {
        valueAccessable = (r.velocity == Vector3.zero);

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
