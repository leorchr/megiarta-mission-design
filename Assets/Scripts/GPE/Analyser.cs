using UnityEngine;

public class Analyser : QuestInteractor
{
    public static Analyser Instance;

    private void Awake()
    {
        if (Instance) Destroy(this);
        else Instance = this;
    }

    private void Start()
    {
        GiveQuest();
    }

    public override void OnInteraction()
    {
        if (current == 2 && quests[current].currentStep == 0)
        {
            Debug.Log("Dialogue Quest 3 !");
        }
        else if (!waitForObject || Inventory.Instance.HasEveryItem(requiredItems))
        {
            FinishQuest();
        }
        else
        {
            Debug.Log("Dialogue Current Quest !");
        }
    }

    private void OnDestroy()
    {
        if (Rowboat.Instance) Rowboat.Instance.enabled = true;
        else Debug.LogWarning("Missing rowboat for quest 4");
    }
}