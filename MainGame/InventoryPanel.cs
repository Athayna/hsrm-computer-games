using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryPanel : MonoBehaviour
{
    private List<GameObject> children = null;
    void Start()
    {
        children = new List<GameObject>();
        foreach (Transform child in transform)
        {
            children.Add(child.gameObject);
        }

        FindObjectOfType<Inventory>().OnItemAdded.AddListener(SetItemPanel);
        FindObjectOfType<Inventory>().OnItemRemoved.AddListener(SetItemPanel);
    }

    private void SetItemPanel(string name, int quantity)
    {
        name += "Panel";
        foreach (GameObject child in children)
        {
            if (child.name == name)
            {
                if (!child.activeSelf) child.SetActive(true);

                if (child.GetComponentInChildren<TextMeshProUGUI>() != null) child.GetComponentInChildren<TextMeshProUGUI>().text = quantity.ToString();
            }
        }
    }


}
