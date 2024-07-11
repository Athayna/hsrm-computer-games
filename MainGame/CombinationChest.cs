using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombinationChest : Chest
{
    [SerializeField] private TextMeshProUGUI digit1;
    [SerializeField] private TextMeshProUGUI digit2;
    [SerializeField] private TextMeshProUGUI digit3;
    [SerializeField] private GameObject combinationPanel;
    [SerializeField] private Button submit = null;

    [SerializeField] private int solution1;
    [SerializeField] private int solution2;
    [SerializeField] private int solution3;

    protected override void Start()
    {
        base.Start();
        uniqueInteraction = false;

        submit?.onClick.AddListener(Submit);
    }

    public override void Interact()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        combinationPanel.SetActive(true);
        FindObjectOfType<ThirdPersonPlayerController>().enabled = false;
        Time.timeScale = 0;
    }

    private void Submit()
    {
        combinationPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        FindAnyObjectByType<ThirdPersonPlayerController>().enabled = true;
        Time.timeScale = 1;

        if (digit1.text == solution1.ToString() && digit2.text == solution2.ToString() && digit3.text == solution3.ToString())
        {
            uniqueInteraction = true;
            base.Interact();
            return;
        }

        FindObjectOfType<MainSceneManager>().Dialogue("The combination is wrong! Try Again!");
    }

    public override string ValidateInteract()
    {
        return "Enter Code";
    }
}
