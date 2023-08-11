using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Database", menuName ="Databases/CardsDatabase")]
public class CardsDatabase : ScriptableObject
{
    public List <Card> cards;
}
