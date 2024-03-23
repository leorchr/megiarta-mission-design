using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    [Serializable]
    public struct QuestionAnswer
    {
        public Question question;
        public Question.enumChoice goodAnswer;
    }

    public List<QuestionAnswer> answerList;

    public DialogueSC dialogueWrong;

    public void ResetQuestion()
    {
        foreach (QuestionAnswer qa in answerList)
        {
            qa.question.resetToggle();
        }
    }

    public void Submit()
    {
        bool isValid = true;
        foreach (QuestionAnswer qa in answerList)
        {
            if (qa.goodAnswer != qa.question.getChoice())
            {
                isValid = false;
                break;
            }
        }
        if (isValid)
        {
            Time.timeScale = 1;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            PlayerController.instance.unlockPlayer();
            GameManager.instance.EndGame();
            Destroy(gameObject);
        }
        else
        {
            ResetQuestion();
            DialogueManager.instance.PlayDialogue(dialogueWrong);
        }
    }
}
