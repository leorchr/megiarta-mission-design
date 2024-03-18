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