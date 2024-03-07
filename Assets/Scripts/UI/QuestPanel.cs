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
        stepText.text = quest.stepName;

        panelAnim.OpenClose();
        SetTotalRequirements();
        Notify();
    }

    public void Notify()
    {
        if(trackedQuest.requirements.Count > 0)
        {
            int amount = 0;
            foreach (QuestItem item in trackedQuest.requirements)
            {
                int qtt = Inventory.Instance.GetItemQuantity(item);
                amount += qtt;
            }
            progress.gameObject.SetActive(true);
            progress.text = amount + " / " + max;
        }
    }

    public void SetTotalRequirements()
    {
        if (trackedQuest.requirements.Count > 0)
        {
            foreach (QuestItem item in trackedQuest.requirements)
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