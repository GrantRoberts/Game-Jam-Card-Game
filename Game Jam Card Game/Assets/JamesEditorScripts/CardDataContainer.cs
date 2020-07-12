using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Card Data")]
public class CardDataContainer : ScriptableObject
{
    public string header;
    [Range(1, 10)]
    public int dc = 1;
    public CardEffect[] successEffects;
    public CardEffect[] failureEffects;

}
