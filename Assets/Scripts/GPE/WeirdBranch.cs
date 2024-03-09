using UnityEngine;

public class WeirdBranch : QuestInteractor
{
    public static WeirdBranch Instance;

    private bool isActive = false;

    private void Awake()
    {
        if (Instance) Destroy(this);
        else Instance = this;
    }

    public override void OnInteraction()
    {
        if (!isActive)
        {
            GiveQuest();
            isActive = true;
        }
        else
        {
            Debug.Log("Dialogue Current Quest !");
        }
    }
}