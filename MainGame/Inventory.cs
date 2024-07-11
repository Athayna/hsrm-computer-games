using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    public UnityEvent<string, int> OnItemAdded = null;
    public UnityEvent<string, int> OnItemRemoved = null;

    private Dictionary<string, int> items = new Dictionary<string, int>();

    public void Add(string itemName)
    {
        if (items.ContainsKey(itemName))
        {
            items[itemName] += 1;
        }

        else
        {
            items.Add(itemName, 1);
        }

        Debug.Log("Invoke OnItemAdded with: " + itemName + " " + items[itemName]);
        OnItemAdded?.Invoke(itemName, items[itemName]);
    }

    public void Remove(string itemName, int quantity)
    {
        if (items.TryGetValue(itemName, out int currentQuantity))
        {
            items[itemName] = Mathf.Max(currentQuantity - quantity, 0);
        }

        Debug.Log("Invoke OnItemRemoved with: " + itemName + " " + items[itemName]);
        OnItemRemoved?.Invoke(itemName, items[itemName]);
    }

    public int GetItemCount(string itemName)
    {
        if (items.TryGetValue(itemName, out int currentQuantity))
        {
            return currentQuantity;
        }

        return 0;
    }

    public bool HasItem(string itemName)
    {
        return items.ContainsKey(itemName);
    }
}
