using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InteractionHelper : MonoBehaviour
{
    public static InteractionHelper Instance;

    public GameObject interactionCue;
    private GameObject cam;
    private Transform interactionUiPos;
    private float dist, scale;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
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
                interactionUiPos = PlayerInteraction.Instance._possiblePickable.UiPos;

                interactionCue.transform.parent = PlayerInteraction.Instance._possiblePickable.gameObject.transform;
                interactionCue.transform.position = interactionUiPos.position;
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
            dist = Vector3.Distance(cam.transform.position, interactionUiPos.position);
            scale = interactionUiPos.localScale.x;
        }
        else
        {
            interactionCue.SetActive(false);
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