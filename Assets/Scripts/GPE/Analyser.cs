using UnityEngine;

public class Analyser : QuestInteractor
{


    private void Start()
    {
        
    }

    public override void OnInteraction()
    {
        this.GetComponent<ArtifactRandomSound>().MakeSound();
        base.OnInteraction();
    }

    private void OnDestroy()
    {

    }
}