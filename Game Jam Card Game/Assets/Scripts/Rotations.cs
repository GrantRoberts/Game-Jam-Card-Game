using System;
using TMPro;
using UnityEngine;

public static class Rotations
{
    private static readonly Vector3[] m_RestingPositions = new Vector3[]
    {
        new Vector3(+40.0f, +155.0f, -195.0f),
        new Vector3(+20.0f, -119.0f, -325.0f),
        new Vector3(+00.0f, +090.0f, -220.0f),
        new Vector3(+22.5f, -061.0f, -325.0f),
        new Vector3(-40.0f, +023.5f, -195.0f),
        new Vector3(-40.0f, +023.5f, -015.0f),
        new Vector3(-22.5f, -061.0f, -145.0f),
        new Vector3(+00.0f, +090.0f, -040.0f),
        new Vector3(+20.0f, -119.0f, -145.0f),
        new Vector3(+40.0f, +155.0f, -015.0f)
    };

    public static Vector3 GetRotation(TextMeshPro number)
    {
        try
        {
            return m_RestingPositions[int.Parse(number.name) + 1];
        }
        catch (IndexOutOfRangeException)
        {
            Debug.LogError($"Bad object, name {number.name}", number);
            throw;
        }
    }
}
