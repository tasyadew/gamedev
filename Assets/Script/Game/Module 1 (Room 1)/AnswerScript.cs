using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManager quizManager;

    // Check the answer is correct or not
    public void Answer()
    {
        Debug.Log("Answer: " + isCorrect);
        quizManager.DisplayAnswerResult(isCorrect);
    }
}
