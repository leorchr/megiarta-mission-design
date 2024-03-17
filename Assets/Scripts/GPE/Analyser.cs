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
        this.GetComponent<ArtifactRandomSound>().MakeSound();

        if (current == 2 && quests[current].currentStep == 0)
        {
            //Debug.Log("Dialogue Quest 3 !");
        }
        else if (!waitForObject || Inventory.Instance.HasEveryItem(requiredItems))
        {
            FinishQuest();
        }
        else
        {
            DialogueManager.instance.PlayDialogue(quests[current].GetCurrentStep().BeginDialogue );
        }
    }

    private void OnDestroy()
    {
        PlayerInteraction.Instance.StopInteractive();
        if (Rowboat.Instance) Rowboat.Instance.enabled = true;
        else Debug.LogWarning("Missing rowboat for quest 4");
    }
}