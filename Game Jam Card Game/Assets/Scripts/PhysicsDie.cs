﻿using System.Linq;
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

    public int m_Modifier = 0;

    [Header("Rolling")]
    public float upwardsForce = 10f;
    public float rotationForce = 10;

    Rigidbody m_Rigidbody;
    Renderer m_Renderer;

    bool m_DoneRolling = false;

    float m_VelocityCheckBuffer = 0.0f;

    float m_MaxVCB = 0.2f;

    TextMeshProUGUI m_ModifierText = null;

    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Renderer = GetComponent<Renderer>();
        m_VelocityCheckBuffer = m_MaxVCB;
        m_ModifierText = GetComponentInChildren<TextMeshProUGUI>();
    }

    [ContextMenu("Roll Die")]
    public void RollDie()
    {
        valueAccessable = true;
        m_Rigidbody.constraints = RigidbodyConstraints.None;
        m_DoneRolling = false;
        m_VelocityCheckBuffer = m_MaxVCB;

        foreach (TextMeshPro face in faces)
        {
            face.text = Mathf.Max(0, int.Parse(face.name) + m_Modifier).ToString();

            if (face.text == "6" || face.text == "9")
            {
                print(face.text);
                face.text = $"<u>{face.text}";
            }
        }
        m_Rigidbody.AddRelativeTorque(new Vector3(Random.Range(-rotationForce, rotationForce), Random.Range(-rotationForce, rotationForce), Random.Range(-rotationForce, rotationForce)));
    }

    private void Update()
    {
        if (m_Rigidbody.velocity == Vector3.zero)
        {
            if (m_DoneRolling)
            {
                valueAccessable = true;
                m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            }

            if (m_VelocityCheckBuffer <= 0.0f)
                m_DoneRolling = true;
            else
                m_VelocityCheckBuffer -= Time.deltaTime;
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
            foreach (TextMeshPro face in faces)
            {
                face.color = Color.black;
            }
            TextMeshPro topFace = faces.Aggregate((face1, face2) => face1.transform.position.y > face2.transform.position.y ? face1 : face2);
            topFace.color = new Color(0.78f, 0f, 0.01f);
            var value = int.Parse(topFace.text.First() == '<' ? topFace.text.Substring(3) : topFace.text);
            if (displayOutput)
                displayOutput.text = value.ToString();
            return value;
        }

        return -1;
    }

    public void AddModifier(int modifier)
    {
        m_Modifier += modifier;
        //UpdateModifierText();
    }

    public void SetModifier(int modifier)
    {
        m_Modifier = modifier;
        //UpdateModifierText();
    }

    //public void UpdateModifierText()
    //{
    //    m_ModifierText.text = m_Modifier.ToString();
    //}

    public Rigidbody GetRigidbody() => m_Rigidbody;
    public Renderer GetRenderer() => m_Renderer;
}
