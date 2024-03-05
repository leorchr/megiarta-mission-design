using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InteractionHelper : MonoBehaviour
{
    public static InteractionHelper Instance;

    public GameObject interactionCue;
    private GameObject cam;
    public float dist, scale;


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
                interactionCue = PlayerInteraction.Instance._possiblePickable.gameObject;
            }
            else
            {
                interactionCue = PlayerInteraction.Instance._possibleInteractive.gameObject;
            }
            interactionCue.SetActive(true);
            dist = Vector3.Distance(cam.transform.position, interactionCue.gameObject.transform.GetChild(0).position);
            scale = interactionCue.transform.localScale.x;
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
            interactionCue.transform.Rotate(new Vector3(90, 0, 0));
            float interactionScale = scale * dist * 0.5f;
            interactionCue.transform.localScale = new Vector3(interactionScale, interactionScale, interactionScale);
        }
    }
}