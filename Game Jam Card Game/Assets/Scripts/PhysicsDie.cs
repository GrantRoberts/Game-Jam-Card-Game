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
    public float upwardsForce = 10f;
    public float rotationForce = 10;

    Rigidbody m_Rigidbody;
    Renderer m_Renderer;

    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Renderer = GetComponent<Renderer>();
    }

    [ContextMenu("Roll Die")]
    public void RollDie()
    {
        valueAccessable = true;
        m_Rigidbody.constraints = RigidbodyConstraints.None;

        foreach (TextMeshPro face in faces)
        {
            face.text = Mathf.Max(0, int.Parse(face.name) + modifier).ToString();
        }
        m_Rigidbody.AddRelativeTorque(new Vector3(Random.Range(-rotationForce, rotationForce), Random.Range(-rotationForce, rotationForce), Random.Range(-rotationForce, rotationForce)));
        m_Rigidbody.AddForce(Vector3.up * Random.Range(upwardsForce * 0.75f, upwardsForce * 1.25f));
    }

    private void Update()
    {
        if(m_Rigidbody.velocity == Vector3.zero)
        {
            valueAccessable = true;
            m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        }

        if (DEBUGShowStatusViaColor)
        {
            m_Renderer.material.color = displayOutput ?
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

    public Rigidbody GetRigidbody() => m_Rigidbody;
    public Renderer GetRenderer() => m_Renderer;
}
