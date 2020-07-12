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
    TextMeshPro[] numbers;

    public int modifier = 0;

    [Header("Force")]
    public float rotationForce = 10;
    public float upwardsForce = 10;

    Rigidbody r;
    new Renderer renderer;

    void Awake()
    {
        r = GetComponent<Rigidbody>();
        renderer = GetComponent<Renderer>();

        numbers = faces.Select(f=>f.GetComponent<TextMeshPro>()).ToArray();

        RollDie();
    }

    [ContextMenu("Roll Die")]
    public void RollDie() => RollDie(r);

    public void RollDie(Rigidbody rb)
    {
        if (modifier != 0)
        {
            foreach (TextMeshPro number in numbers)
            {
                number.text = Mathf.Max(0, int.Parse(number.name) + modifier).ToString();
                print($"{number} mapped to {number.text}");
            }
        }
        rb.AddTorque(new Vector3(Random.Range(-rotationForce, rotationForce), Random.Range(-rotationForce, rotationForce), Random.Range(-rotationForce, rotationForce)));
        rb.AddForce(Vector3.up * Random.Range(upwardsForce * 0.75f, upwardsForce * 1.25f));
    }

    private void Update()
    {
        if(r.velocity == Vector3.zero)
        {
            valueAccessable = true;
            r.constraints = RigidbodyConstraints.FreezeRotation;
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
            var value = int.Parse(faces.Aggregate((face1, face2) => face1.position.y > face2.position.y ? face1 : face2).GetComponent<TextMeshPro>().text);
            if (displayOutput)
                displayOutput.text = value.ToString();
            return value;
        }
        return -1;
    }
}
