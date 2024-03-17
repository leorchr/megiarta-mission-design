using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public static PlayerInteraction Instance;
    private PlayerInteractionAnim _anim;
    private InteractionType _possibleInteraction = InteractionType.None;
    private Inventory _inventory;
    public Pickable _possiblePickable;
    public Interactive _possibleInteractive;

    private void Start()
    {
        if (Instance) Destroy(this);
        else Instance = this;

        _anim = GetComponent<PlayerInteractionAnim>();
        _inventory = Inventory.Instance;
    }

    public void Interaction(InputAction.CallbackContext ctx)
    {
        if (_possibleInteraction != InteractionType.None && ctx.started)
        {
            _anim.PlayAnimation(_possibleInteraction);
            if (_possibleInteraction == InteractionType.Pickup && _possiblePickable && IsPickableNeeded())
            {
                Pickup();
                QuestManager.Instance.Notify();
            }
            else if (_possibleInteraction != InteractionType.Pickup)
            {
                Interact();
            }
        }
    }

    private bool IsPickableNeeded()
    {
        foreach (QuestFullData qfd in QuestManager.Instance.questsProgress)
        {
            QuestData quest = qfd.questData;
            foreach (QuestItem item in quest.GetCurrentStep().requirements)
            {
                if (_possiblePickable.item.Equals(item.item)
                    && !Inventory.Instance.HasEvery(item))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void Pickup()
    {
        _possiblePickable.OnPick();
        _inventory.PickupQuestItem(_possiblePickable.item);
        _possiblePickable.gameObject.SetActive(false);
        SetInteraction(InteractionType.None);
    }

    private void Interact()
    {
        _possibleInteractive.OnInteraction();
        if (_possibleInteractive && _possibleInteractive.onlyOnce)
        {
            DisableInteractive();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!PlayerInteractionAnim.AnimationInProgress)
        {
            if (other.transform.CompareTag("Pickable"))
            {
                _possiblePickable = other.GetComponent<Pickable>();
                if (IsPickableNeeded()) SetInteraction(InteractionType.Pickup);
            }
            else if (other.transform.CompareTag("Interactive"))
            {
                Interactive interactive = other.GetComponent<Interactive>();
                if (interactive == null) return;
                //if interaction doesn't need key object or interaction key object is in inventory
                _possibleInteractive = interactive;
                SetInteraction(_possibleInteractive.interactionType);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!PlayerInteractionAnim.AnimationInProgress)
        {
            if (other.transform.CompareTag("Pickable") || other.transform.CompareTag("Interactive") )
            {
                StopInteractive();
            }
        }
    }

    public void StopInteractive()
    {
        SetInteraction(InteractionType.None);
        _possibleInteractive = null;
        _possiblePickable = null;
    }

    private void DisableInteractive()
    {
        _possibleInteractive.GetComponent<SphereCollider>().enabled = false;
        Destroy(_possibleInteractive);
        SetInteraction(InteractionType.None);
    }

    public void SetInteraction(InteractionType interaction)
    {
        _possibleInteraction = interaction;
        InteractionHelper.Instance.Show(interaction);
    }

    private void OnFail()
    {
        _anim.PlayAnimation(InteractionType.FailedAction);
        SetInteraction(InteractionType.FailedAction);
    }
}
