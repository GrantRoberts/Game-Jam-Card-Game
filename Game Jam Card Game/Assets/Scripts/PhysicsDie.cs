using System.Linq;
using TMPro;
using UnityEngine;

public class PhysicsDie : MonoBehaviour
{
    private Rigidbody m_Rigidbody;
    private Renderer m_Renderer;

    public TextMeshPro[] faces;

    [Header("Value")]
    public int? m_Value;

    private int m_Modifier = 0;

    [Header("Rolling")]
    public float m_UpwardsForce = 10f;
    public float m_RotationForce = 10f;

    [Tooltip("How long the die must be at rest to consider it to have finished rolling")]
    private float m_MaxVCB = 0.2f;
    [Tooltip("How long the die has been unmoving")]
    private float m_VelocityCheckBuffer = 0.0f;
    public bool m_ValueAccessable;


    public Camera m_GameCamera;
    private Ray m_PointerRay = new Ray();
    private RaycastHit m_Hit = new RaycastHit();

    public float m_DragHeight = 1.0f;

    public Rigidbody GetRigidbody() => m_Rigidbody;

    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Renderer = GetComponent<Renderer>();
        m_VelocityCheckBuffer = m_MaxVCB;
    }

    private void Update()
    {
        if (m_Rigidbody.velocity == Vector3.zero)
        {
            if (m_VelocityCheckBuffer <= 0.0f)
            {
                m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            }
            else
            {
                m_VelocityCheckBuffer -= Time.deltaTime;
            }
        }

        m_Value = GetResult();
    }

    [ContextMenu("Roll Die")]
    public void RollDie()
    {
        m_ValueAccessable = false;
        m_Rigidbody.constraints = RigidbodyConstraints.None;
        m_VelocityCheckBuffer = m_MaxVCB;

        UpdateDiceNumbers();
        transform.rotation = Random.rotation;
        m_Rigidbody.AddRelativeTorque(new Vector3(Random.Range(-m_RotationForce, m_RotationForce), Random.Range(-m_RotationForce, m_RotationForce), Random.Range(-m_RotationForce, m_RotationForce)));
    }

    public void UpdateDiceNumbers()
    {
        foreach (TextMeshPro face in faces)
        {
            face.text = Mathf.Max(0, int.Parse(face.name) + m_Modifier).ToString();

            if (face.text == "6" || face.text == "9")
            {
                face.text = $"<u>{face.text}";
            }
        }
    }

    public int? GetResult()
    {
        if (m_ValueAccessable)
        {
            foreach (TextMeshPro face in faces)
            {
                face.color = Color.black;
            }
            TextMeshPro topFace = faces.Aggregate((face1, face2) => face1.transform.position.y > face2.transform.position.y ? face1 : face2);
            topFace.color = new Color(0.78f, 0f, 0.01f);
            // '<' check is to deal with the underline on 6 and 9
            return int.Parse(topFace.text.First() == '<' ? topFace.text.Substring(3) : topFace.text); 
        }

        return null;
    }

    public void AddModifier(int modifier)
    {
        m_Modifier += modifier;
        m_Renderer.material.color = PlayerManager.instance.m_DiceBonusGradient.Evaluate((Mathf.Clamp((float)m_Modifier / 5, -1f, 1f) + 1) / 2);
    }

    public void Reset()
    {
        m_Modifier = 0;
        gameObject.SetActive(false);
        m_Renderer.material.color = Color.white;
    }


    public void OnMouseDrag()
    {
        // This is so scuffed.
        // Make the raycast coming up go through this die.
        gameObject.layer = 2;

        m_PointerRay = m_GameCamera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(m_PointerRay, out m_Hit, float.MaxValue);
        transform.position = m_Hit.point + (Vector3.up * m_DragHeight);
        m_Rigidbody.velocity = Vector3.zero;

        // This is dumb.
        // Have to reset for function to even be called again.
        gameObject.layer = 0;
    }

    //public void OnCollisionStay(Collision collision)
    //{
    //    JamesCard cardHit = collision.gameObject.GetComponent<JamesCard>();
    //    if (!cardHit && !cardHit.GetDie())
    //    {
    //        cardHit.SetDie(this);
    //    }
    //}

    //public void OnCollisionExit(Collision collision)
    //{
    //    JamesCard cardHit = collision.gameObject.GetComponent<JamesCard>();
    //    if (!cardHit && cardHit.GetDie())
    //    {
    //        cardHit.SetDie(null);
    //    }
    //}
}
