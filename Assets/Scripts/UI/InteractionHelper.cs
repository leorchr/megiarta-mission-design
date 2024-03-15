using UnityEngine;

public class InteractionHelper : MonoBehaviour
{
    public static InteractionHelper Instance;

    public GameObject interactionCue, vfxParticles;
    private GameObject cam;
    private Transform interactionUiPos;
    private Transform vfxPos;
    private float dist, scale;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        interactionCue.SetActive(false);
        vfxParticles.SetActive(false);
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
                    interactionCue.transform.parent = PlayerInteraction.Instance._possiblePickable.gameObject.transform;
                    interactionCue.transform.position = interactionUiPos.position;
                }

                if (PlayerInteraction.Instance._possiblePickable.VfxPos == null)
                {
                    Debug.LogWarning("Missing VFX Position in pickable");
                    return;
                }
                else
                {
                    vfxPos = PlayerInteraction.Instance._possiblePickable.VfxPos;
                    vfxParticles.transform.parent = PlayerInteraction.Instance._possiblePickable.gameObject.transform;
                    vfxParticles.transform.position = vfxPos.position;
                }


            }
            else
            {
                if (PlayerInteraction.Instance._possibleInteractive.UiPos == null)
                {
                    Debug.LogWarning("Missing UI Position in interactive");
                    return;
                }
                interactionUiPos = PlayerInteraction.Instance._possibleInteractive.UiPos;

                interactionCue.transform.parent = PlayerInteraction.Instance._possibleInteractive.gameObject.transform;
                interactionCue.transform.position = interactionUiPos.position;
            }

            interactionCue.SetActive(true);
            vfxParticles.SetActive(true);
            dist = Vector3.Distance(cam.transform.position, interactionUiPos.position);
            scale = interactionUiPos.localScale.x;
        }
        else
        {
            interactionCue.SetActive(false);
            vfxParticles.SetActive(false);
        }
    }

    private void Update()
    {
        if (interactionCue.activeSelf)
        {
            interactionCue.transform.LookAt(cam.transform.position);
            interactionCue.transform.Rotate(new Vector3(0, 0, 0));
            float interactionScale = scale * dist * 0.5f;
            interactionCue.transform.localScale = new Vector3(interactionScale, interactionScale, interactionScale);
        }
    }
}