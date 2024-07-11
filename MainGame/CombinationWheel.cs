using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombinationWheel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI digit = null;
    [SerializeField] private Button up = null;
    [SerializeField] private Button down = null;
    [SerializeField] private AudioSource audioSource = null;
    private int number = 0;
    void Start()
    {
        up?.onClick.AddListener(Increase);
        down?.onClick.AddListener(Decrease);
    }

    void Increase()
    {
        audioSource?.Play();
        number = (number + 1) % 10;
        digit.text = number.ToString();
    }

    void Decrease()
    {
        audioSource?.Play();
        number--;
        if (number < 0) number = 9;
        digit.text = number.ToString();
    }

    /*
    private void SetItemPanel(string name, int quantity)
    {
        name += "Panel";
        foreach (GameObject child in this.children)
        {
            if (child.name == name)
            {
                if (!child.activeSelf) child.SetActive(true);

                if (child.GetComponentInChildren<TextMeshProUGUI>() != null) child.GetComponentInChildren<TextMeshProUGUI>().text = quantity.ToString();
            }
        }
    } */
}
