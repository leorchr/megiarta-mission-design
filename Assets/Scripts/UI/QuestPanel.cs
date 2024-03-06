using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestPanel : MonoBehaviour
{
    public TextMeshProUGUI questText, stepText;
    private QuestData trackedQuest;
    [SerializeField] QuestPanelAnimation panelAnim;

    private int max = 0;

    public void SetupQuest(QuestData quest)
    {
        trackedQuest = quest;
        questText.text = quest.questName;
        stepText.text = quest.stepName;

        panelAnim.OpenClose();
        //SetTotalRequirements();
        //Notify();
    }

    /*public void Notify()
    {
        int amount = 0;
        foreach (QuestItem item in trackedQuest.requirements)
        {
            int qtt = Inventory.Instance.GetItemQuantity(item);
            amount += qtt;
        }
        progress.text = amount + " / " + max;
        if (Inventory.Instance.HasEveryItem(trackedQuest.requirements))
        {
            Complete();
        }
    }*/

    public void SetTotalRequirements()
    {
        foreach (QuestItem item in trackedQuest.requirements)
        {
            max += item.quantity;
        }
    }

    public void Complete()
    {
        panelAnim.OpenClose();
    }
}