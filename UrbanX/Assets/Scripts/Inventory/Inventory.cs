using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "New Inventory", menuName ="Inventory/New Inventory")]
public class Inventory : ScriptableObject
{
    public List<Item> inventoryList = new List<Item>();
}
