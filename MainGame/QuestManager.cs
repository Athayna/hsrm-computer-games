using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questText = null;

    private Dictionary<int, string> quests = new Dictionary<int, string>();
    void Start()
    {
        // fill quest descriptions
        quests.Add(1, "Head to the village and find clues about the rune stones");     // On game start
        quests.Add(2, "Collect 5 berries for the mage's potion");                      // OnMageInteracted()
        quests.Add(3, "Sneak by the troll in the forest and find the rune stone");     // OnItemAdded("InvisibilitySpell", 1)
        quests.Add(4, "Find the giant plant hiding the rune stone in the forest");     // OnItemAdded("FireSpell", 1)
        quests.Add(5, "Try to find more clues about the rune stones in the village");  // OnItemAdded("RuneStone", 2)
        quests.Add(6, "Find the combination for the old man's chest");                 // OnOldManInteracted()
        quests.Add(7, "Find the big rock hiding the rune stone in the forest");            // OnItemAdded("TelekinesisSpell", 1)
        quests.Add(8, "Return to the portal and place the rune stones");               // OnItemAdded("RuneStone", 3)
        quests.Add(9, "Step through the portal...");                                   // OnPortalFinished()

        // subscribe to events
        FindObjectOfType<Mage>().OnMageInteracted.AddListener(UpdateQuestsWithBool);
        FindObjectOfType<Portal>().OnPortalFinished.AddListener(UpdateQuestsWithBool);
        FindObjectOfType<Inventory>().OnItemAdded.AddListener(UpdateQuestsWithInventory);
        FindObjectOfType<OldMan>().OnOldManInteracted.AddListener(UpdateQuestsWithBool);

        // initialize quest log with first quest
        UpdateQuestLog(1);
    }

    private void UpdateQuestsWithBool(string name, bool boolean)
    {
        Debug.Log("Updating quest log with " + name + " and " + boolean);
        
        if (name == "Mage") {
            UpdateQuestLog(2);
            FindObjectOfType<Mage>().OnMageInteracted.RemoveListener(UpdateQuestsWithBool);
        }
        else if (name == "OldMan") {
            UpdateQuestLog(6);
            FindObjectOfType<OldMan>().OnOldManInteracted.RemoveListener(UpdateQuestsWithBool);
            FindAnyObjectByType<CombinationChest>().GetComponent<BoxCollider>().enabled = true;
        }
        else if (name == "Portal") {
            UpdateQuestLog(9);
            FindObjectOfType<Portal>().OnPortalFinished.RemoveListener(UpdateQuestsWithBool);    
        }
    }

    private void UpdateQuestsWithInventory(string name, int quantity)
    {
        Debug.Log("Updating quest log with " + name + " and " + quantity);

        if (name == "InvisibilitySpell") UpdateQuestLog(3);
        else if (name == "FireSpell") {
            UpdateQuestLog(4);
            FindObjectOfType<MainSceneManager>().Dialogue("You find a note in the chest: \"Congratulations. The next rune stone is hidden beneath a large plant in the forest. You will need this fire spell to burn it. Walk from here through the forest towards the village to find it. Good luck!\"");
        }
        else if (name == "RuneStone" && quantity == 2) {
            UpdateQuestLog(5);
            FindObjectOfType<OldMan>().transform.GetChild(1).gameObject.SetActive(true);
            FindObjectOfType<OldMan>().GetComponent<Collider>().enabled = true;
        }
        else if (name == "TelekinesisSpell") UpdateQuestLog(7);
        else if (name == "RuneStone" && quantity == 3) UpdateQuestLog(8);
    }

    public void UpdateQuestLog(int questId)
    {
        if (!quests.ContainsKey(questId))
        {
            return;
        }

        questText.text = quests[questId];
    }
}
