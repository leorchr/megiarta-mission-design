using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestPanel : MonoBehaviour
{
    public TextMeshProUGUI questText, stepText, progress;
    [HideInInspector] public QuestData trackedQuest;
    [SerializeField] QuestPanelAnimation panelAnim;

    private int max = 0;

    public void SetupQuest(QuestData quest)
    {
        trackedQuest = quest;
        questText.text = quest.questName;
        stepText.text = quest.GetCurrentStep().stepName;

        panelAnim.OpenClose();
        SetTotalRequirements();
        Notify();
    }

    public void UpdateQuest()
    {
        questText.text = trackedQuest.questName;
        stepText.text = trackedQuest.GetCurrentStep().stepName;
        SetTotalRequirements();
    }

    public void Notify()
    {
        UpdateQuest();
        if (trackedQuest.GetCurrentStep().requirements.Count > 0)
        {
            int amount = 0;
            foreach (QuestItem item in trackedQuest.GetCurrentStep().requirements)
            {
                int qtt = Inventory.Instance.GetItemQuantity(item);
                amount += qtt;
            }
            progress.gameObject.SetActive(true);
            progress.text = amount + " / " + max;
        }
        else progress.text = string.Empty;
    }

    public void SetTotalRequirements()
    {
        max = 0;
        if (trackedQuest.GetCurrentStep().requirements.Count > 0)
        {
            foreach (QuestItem item in trackedQuest.GetCurrentStep().requirements)
            {
                max += item.quantity;
            }
        }
    }

    public void Complete()
    {
        panelAnim.OpenClose();
    }

    public void CompleteEnd()
    {
        Destroy(gameObject);
    }
}