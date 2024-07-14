using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
    [Header("MCQ Quiz List")]
    public List<List<QuizAndAnswer>> questionList;
    [SerializeField] List<QuizAndAnswer> question1Pool;
    [SerializeField] List<QuizAndAnswer> question2Pool;
    [SerializeField] List<QuizAndAnswer> question3Pool;
    [SerializeField] List<QuizAndAnswer> question4Pool;
    [SerializeField] List<QuizAndAnswer> question5Pool;
    [SerializeField] GameObject[] optionsButton;

    //[SerializeField] GameObject MCQQuiz;
    [SerializeField] GameObject allAnswerText;
    [SerializeField] GameObject quizAnswerStand;
    
    [Header("Game Object")]
    [SerializeField] GameObject quizPanel;
    [SerializeField] GameObject correctAnswerText;
    [SerializeField] GameObject wrongAnswerText;
    [SerializeField] TMP_Text showCorrectAnswer;
    [SerializeField] GameObject UIStartQuizPanel;
    [SerializeField] GameObject UIStartQuizTimerPanel;
    public int startQuizTimer = 3;
    [SerializeField] TMP_Text startQuizTimerText;
    [SerializeField] GameObject UIEndQuizPanel;
    [SerializeField] GameObject UIQuizCounterPanel;
    // [SerializeField] GameObject instruction;

    [Header("Text")]
    [SerializeField] TMP_Text[] answerText;
    //[SerializeField] TMP_Text answer;
    [SerializeField] TMP_Text questionText;
    [SerializeField] TMP_Text questionNoText;
    [SerializeField] TMP_Text scoreText;

    [Header("Current Question")]
    [SerializeField] int questionVariable;
    int questionCounter;
    int totalQuestion = 5;
    int score;
    public int counter = 0;
    public int attachCounter = 0;
    GameObject textShow;

    // Start is called before the first frame update
    void Start()
    {
        questionList = new List<List<QuizAndAnswer>>();
        questionList.Add(question1Pool);
        questionList.Add(question2Pool);
        questionList.Add(question3Pool);
        questionList.Add(question4Pool);
        questionList.Add(question5Pool);
        questionCounter = 0;
        quizPanel.SetActive(false);
        //MCQQuiz.SetActive(false);
        // ObjectQuiz.SetActive(false);
        correctAnswerText.SetActive(false);
        wrongAnswerText.SetActive(false);
        showCorrectAnswer.gameObject.SetActive(false);
        UIEndQuizPanel.SetActive(false);
        UIStartQuizPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // Start the quiz
    public void StartQuiz()
    {
        UIStartQuizPanel.SetActive(false);
        StartCoroutine(IEStartQuizTimer());
    }

    // Restart the quiz
    public void RestartQuiz()
    {
        questionCounter = 0;
        UIEndQuizPanel.SetActive(false);
        startQuizTimer = 3;
        StartCoroutine(IEStartQuizTimer());
    }

    // End the quiz
    public void EndQuiz()
    {
        quizPanel.SetActive(false);
        UIEndQuizPanel.SetActive(true);
        scoreText.text = "Score: " + score.ToString() + " / " + totalQuestion.ToString();
    }

    public IEnumerator IEStartQuizTimer()
    {
        UIStartQuizTimerPanel.SetActive(true);
        while(startQuizTimer > 0)
        {
            yield return new WaitForSeconds(1f);
            startQuizTimer--;
            startQuizTimerText.text = startQuizTimer.ToString(); 
        }
        UIStartQuizTimerPanel.SetActive(false);
        quizPanel.SetActive(true);
        GenerateQuestions();
    }

    // Display result after choosing an answer
    public IEnumerator IEDisplayAnswerResult(bool answerState)
    {
        Debug.Log("IEDisplay:" + answerState);
        allAnswerText.SetActive(false);
        quizAnswerStand.SetActive(false);
        showCorrectAnswer.gameObject.SetActive(true);

        if(answerState)
        {
            score++;
            correctAnswerText.SetActive(true);
            yield return new WaitForSeconds(3f);
            correctAnswerText.SetActive(false);
        }

        else
        {
            wrongAnswerText.SetActive(true);
            yield return new WaitForSeconds(3f);
            wrongAnswerText.SetActive(false);
        }

        questionCounter++;
        GenerateQuestions();
        quizAnswerStand.SetActive(true);
        showCorrectAnswer.gameObject.SetActive(false);
    }

    public void DisplayAnswerResult(bool answerBool)
    {
        Debug.Log("Display:" + answerBool);
        StartCoroutine(IEDisplayAnswerResult(answerBool));
    }

    // Generate the quiz questions
    public void GenerateQuestions()
    {
        Debug.Log("question counter: " + questionCounter);
        if(questionCounter == 5)
        {
            EndQuiz();
            return;
        }

        questionVariable = Random.Range(0, questionList[questionCounter].Count);
        questionText.text = questionList[questionCounter][questionVariable].questionText;
        questionNoText.text = "Q" + (questionCounter + 1).ToString() + ":";

        SetAnswers();

        quizPanel.SetActive(true);
        allAnswerText.SetActive(true);
    }

    // Set the answer
    void SetAnswers()
    {
        for(int i = 0; i < optionsButton.Length; i++)
        {
            optionsButton[i].GetComponent<AnswerScript>().isCorrect = false;   
            answerText[i].text = questionList[questionCounter][questionVariable].answers[i];

            if(questionList[questionCounter][questionVariable].correctAnswer == i)
            {
                optionsButton[i].GetComponent<AnswerScript>().isCorrect = true;
                showCorrectAnswer.text = "The answer is " + answerText[i].text + ".";
            }
        }
    }
}
