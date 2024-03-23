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
            GameManager.instance.EndGame();
        }
        else
        {
            ResetQuestion();
            DialogueManager.instance.PlayDialogue(dialogueWrong);
        }
    }
}
