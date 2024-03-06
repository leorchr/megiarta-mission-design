using UnityEngine;

public class QuestPanelAnimation : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

    public void OpenClose()
    {
        _animator.SetTrigger("OpenClose");
    }
}