using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName ="Databases/ItemsDatabase")]
public class ItemsDatabase : ScriptableObject
{
    public List <Item> items;
}
