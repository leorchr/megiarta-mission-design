using System.Linq;
using UnityEngine;

public class InteractionHelper : MonoBehaviour
{
    public static InteractionHelper Instance;

    public GameObject interactionCue, vfxParticles;
    private GameObject cam;
    private Transform interactionUiPos;
    private float dist, scale;

    public Sprite ControllerButton, KeyboardButton;
    private Pickable[] foundPickableObjects;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        interactionCue.SetActive(false);
        foundPickableObjects = FindObjectsOfType<Pickable>();
    }

    public void Show(InteractionType interaction = InteractionType.None)
    {
        if (interaction != InteractionType.None)
        {
            if (interaction == InteractionType.Pickup)
            {
                if (PlayerInteraction.Instance._possiblePickable.UiPos == null)
                {
                    Debug.LogWarning("Missing UI Position in pickable");
                    return;
                }
                else
                {
                    interactionUiPos = PlayerInteraction.Instance._possiblePickable.UiPos;
                    //interactionCue.transform.parent = PlayerInteraction.Instance._possiblePickable.gameObject.transform;
                    interactionCue.transform.position = interactionUiPos.position;                }
            }
            else
            {
                if (PlayerInteraction.Instance._possibleInteractive.UiPos == null)
                {
                    Debug.LogWarning("Missing UI Position in interactive");
                    return;
                }
                interactionUiPos = PlayerInteraction.Instance._possibleInteractive.UiPos;

                //interactionCue.transform.parent = PlayerInteraction.Instance._possibleInteractive.gameObject.transform;
                interactionCue.transform.position = interactionUiPos.position;
            }

            interactionCue.SetActive(true);
            dist = Vector3.Distance(cam.transform.position, interactionUiPos.position);
            scale = interactionUiPos.localScale.x;
        }
        else
        {
            interactionCue.SetActive(false);
        }
    }

    public void ShowParticles()
    {
        var quests = QuestManager.Instance.questsProgress;
        for (int i = 0; i < quests.Count; i++)
        {
            for (int y = 0; y < quests[i].steps[quests[i].currentStep].requirements.Count; y++)
            {
                foreach (var pickable in foundPickableObjects)
                {
                    Pickable pick = pickable.GetComponent<Pickable>();
                    if(pick.item == quests[i].steps[quests[i].currentStep].requirements[y].item)
                    {
                        if (pick.VfxPos == null)
                        {
                            Debug.LogWarning("Missing VFX Position in pickable");
                            return;
                        }
                        else
                        {
                            GameObject particles = Instantiate(vfxParticles, pick.VfxPos);
                            particles.transform.parent = pick.gameObject.transform;
                        }
                    }
                }
            }
        }
    }

    private void Update()
    {
        if (interactionCue != null && interactionCue.activeSelf )
        {
            interactionCue.transform.LookAt(cam.transform.position);
            interactionCue.transform.Rotate(new Vector3(0, 0, 0));
            float interactionScale = scale * dist * 0.5f;
            interactionCue.transform.localScale = new Vector3(interactionScale, interactionScale, interactionScale);

            switch (InputManager.instance.getCurrentControlScheme())
            {
                case ControlScheme.Keyboard:
                    interactionCue.GetComponent<SpriteRenderer>().sprite = KeyboardButton;
                    break;
                case ControlScheme.Controller:
                    interactionCue.GetComponent<SpriteRenderer>().sprite = ControllerButton;
                    break;
                default: break;
            }
        }
    }
}