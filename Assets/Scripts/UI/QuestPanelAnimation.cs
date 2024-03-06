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
        Debug.Log(_animator);
        _animator.SetTrigger("OpenClose");
    }

}