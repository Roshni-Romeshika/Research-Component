//Script created by IT17100076
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    //Declaring varibles
#pragma warning disable 649
    //referance to the QuizGameUI script
    [SerializeField] private QuizGameUI quizGameUI;
    //referance to the scriptableobject file
    [SerializeField] private List<QuizDataScriptable> quizDataList;
    [SerializeField] private float timeInSeconds;
#pragma warning restore 649
    //Questions data
    private List<Question> questions;
    //Current question data
    private Question selectedQuetion = new Question();
    private int gameScore;
    private int lifesRemaining;
    private float currentTime;
    private QuizDataScriptable dataScriptable;

    private GameStatus gameStatus = GameStatus.NEXT;

    public GameStatus GameStatus { get { return gameStatus; } }

    public void StartGame(int categoryIndex)
    {
        gameScore = 0;
        lifesRemaining = 3;
        currentTime = timeInSeconds;
        //Set the questions data
        questions = new List<Question>();
        dataScriptable = quizDataList[categoryIndex];
        questions.AddRange(dataScriptable.questions);
        //Select the question
        SelectQuestion();
        gameStatus = GameStatus.PLAYING;
    }

    /// <summary>
    /// Method used to randomly select the question form questions data
    /// </summary>
    private void SelectQuestion()
    {
        //Get the random number
        int val = UnityEngine.Random.Range(0, questions.Count);
        //Set the selectedQuetion
        selectedQuetion = questions[val];
        //Send the question to quizGameUI
        quizGameUI.SetQuestion(selectedQuetion);

        questions.RemoveAt(val);
    }

    private void Update()
    {
        if (gameStatus == GameStatus.PLAYING)
        {
            currentTime -= Time.deltaTime;
            SetTime(currentTime);
        }
    }

    void SetTime(float value)
    {
        TimeSpan time = TimeSpan.FromSeconds(currentTime);      //Set the time value
        quizGameUI.TimerText.text = time.ToString("mm':'ss");   //Convert time to Time format

        if (currentTime <= 0)
        {
            //Game Over
            gameStatus = GameStatus.NEXT;
            quizGameUI.GameOverPanel.SetActive(true);
        }
    }

    /// <summary>
    /// Method called to check the answer is correct or not
    /// </summary>
    /// <param name="selectedOption">answer string</param>
    /// <returns></returns>
    public bool Answer(string selectedOption)
    {
        //Set default value to false
        bool correct = false;
        //If selected answer is similar to the correctAns
        if (selectedQuetion.correctAns == selectedOption)
        {
            //Yes, Ans is correct
            correct = true;
            gameScore += 50;
            quizGameUI.ScoreText.text = "Score:" + gameScore;
        }
        
        else
        {
            //No, Ans is wrong
            //Reduce Life
            lifesRemaining--;
            quizGameUI.ReduceLife(lifesRemaining);

            if (lifesRemaining == 0)
            {
                gameStatus = GameStatus.NEXT;
                quizGameUI.GameOverPanel.SetActive(true);
            }
        }

        if (gameStatus == GameStatus.PLAYING)
        {
            if (questions.Count > 0)
            {
                //Call SelectQuestion method again after 1s
                Invoke("SelectQuestion", 0.4f);
                
            }
            else
            {
                gameStatus = GameStatus.NEXT;
                quizGameUI.GameCompletePanel.SetActive(true);
            }
        }
        
        if(gameScore == 200)
        {
            gameStatus = GameStatus.NEXT;
            quizGameUI.GameCompletePanel.SetActive(true);
        }
        //return the value of correct bool
        return correct;
    }
}

//Datastructure for storeing the quetions data
[System.Serializable]
public class Question
{
    public string questionInfo;                     //Question text
    public QuestionType questionType;               //Type
    public Sprite questionImage;                    //Image for Image Type
    public AudioClip audioClip;                     //Audio for audio type
    public UnityEngine.Video.VideoClip videoClip;   //Video for video type
    public List<string> options;                    //Options to select
    public string correctAns;                       //Correct option
}

[System.Serializable]
public enum QuestionType
{
    TEXT,
    IMAGE,
    AUDIO,
    VIDEO
}

[SerializeField]
public enum GameStatus
{
    PLAYING,
    NEXT
}
// End of the QuizManager script