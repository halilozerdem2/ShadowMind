using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static List<Item> items = new List<Item>(); // Envanterdeki itemler

    private void Start()
    {
    }
    public void AddItem(Item newItem)
    {
        items.Add(newItem);
    }

    public void RemoveItem(Item itemToRemove)
    {
        if (items.Contains(itemToRemove))
        {
            items.Remove(itemToRemove);
        }
    }
}
