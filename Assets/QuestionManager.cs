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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
