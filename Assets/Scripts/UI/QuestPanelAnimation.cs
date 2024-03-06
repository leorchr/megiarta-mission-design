using UnityEngine;

public class QuestPanelAnimation : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void OpenClose()
    {
        _animator.SetTrigger("OpenClose");
    }

}