//Script created by IT17100076
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizGameUI : MonoBehaviour
{

    // Declare varibales
    public string levelToLoad;
#pragma warning disable 649
    [SerializeField] private QuizManager quizManager;               //refernace to the QuizManager script
    [SerializeField] private Text scoreText, timerText;
    [SerializeField] private List<Image> lifeImageList;
    [SerializeField] private GameObject gameOverPanel, mainMenu, gamePanel, gameCompletePanel;
    [SerializeField] private Color correctCol, wrongCol, normalCol; //Color of buttons
    [SerializeField] private Image questionImg;                     //Image component to show image
    [SerializeField] private UnityEngine.Video.VideoPlayer questionVideo;   //To show video in a question
    [SerializeField] private AudioSource questionAudio;             //Audio source for audio clip in a question
    [SerializeField] private Text questionInfoText;                 //Text to show in a question
    [SerializeField] private List<Button> options;                  //options button reference
    [SerializeField] private List<Button> uiButtons;
#pragma warning restore 649

    private float audioLength;          //Store audio file
    private Question question;          //Store current question data
    private bool answered = false;      //Bool value to keep track if answered or not

    public Text TimerText { get => timerText; }                     //getter
    public Text ScoreText { get => scoreText; }                     //getter
    public GameObject GameOverPanel { get => gameOverPanel; }       //getter
    public GameObject GameCompletePanel { get => gameCompletePanel; }

    private void Start()
    {   //Add the listner to all the buttons
        for (int i = 0; i < options.Count; i++)
        {
            Button localBtn = options[i];
            localBtn.onClick.AddListener(() => OnClick(localBtn));
        }

        for (int i = 0; i < uiButtons.Count; i++)
        {
            Button localBtn = uiButtons[i];
            localBtn.onClick.AddListener(() => OnClick(localBtn));
        }

    }
    /// <summary>
    /// Method which populate the question on the screen
    /// </summary>
    /// <param name="question"></param>
    public void SetQuestion(Question question)
    {
        //Set the question
        this.question = question;
        //Switch case use to check for questionType instead if-else statement
        switch (question.questionType)
        {
            case QuestionType.TEXT:
                questionImg.transform.parent.gameObject.SetActive(false);   //Deactivate image holder
                break;
            case QuestionType.IMAGE:
                questionImg.transform.parent.gameObject.SetActive(true);    //Activate image holder in the quiz panel
                questionVideo.transform.gameObject.SetActive(false);        //Deactivate questionVideo in the quiz panel
                questionImg.transform.gameObject.SetActive(true);           //Activate questionImg in the quiz panel
                questionAudio.transform.gameObject.SetActive(false);        //Deactivate questionAudio in the quiz panel

                questionImg.sprite = question.questionImage;                //Set the image sprite
                break;
            case QuestionType.AUDIO:
                questionVideo.transform.parent.gameObject.SetActive(true);  //Activate image holder in the quiz panel
                questionVideo.transform.gameObject.SetActive(false);        //Deactivate questionVideo in the quiz panel
                questionImg.transform.gameObject.SetActive(false);          //Deactivate questionImg in the quiz panel
                questionAudio.transform.gameObject.SetActive(true);         //Activate questionAudio in the quiz panel
                
                audioLength = question.audioClip.length;                    //Set audio clip in the quiz panel
                StartCoroutine(PlayAudio());                                //Start Coroutine in the quiz panel
                break;
            case QuestionType.VIDEO:
                questionVideo.transform.parent.gameObject.SetActive(true);  //Activate image holder in the quiz panel
                questionVideo.transform.gameObject.SetActive(true);         //Activate questionVideo in the quiz panel
                questionImg.transform.gameObject.SetActive(false);          //Deactivate questionImg in the quiz panel
                questionAudio.transform.gameObject.SetActive(false);        //Deactivate questionAudio in the quiz panel

                questionVideo.clip = question.videoClip;                    //Set video clip
                questionVideo.Play();                                       //Play video
                break;
        }

        questionInfoText.text = question.questionInfo;                      //Set the question text

        //Suffle the list of answer options
        List<string> ansOptions = ShuffleList.ShuffleListItems<string>(question.options);

        //Assign options to respective option buttons
        for (int i = 0; i < options.Count; i++)
        {
            //Set the child text
            options[i].GetComponentInChildren<Text>().text = ansOptions[i];
            options[i].name = ansOptions[i];    //Set the name of button
            options[i].image.color = normalCol; //Set color of button to normal
        }

        answered = false;                       

    }

    public void ReduceLife(int remainingLife)
    {
        lifeImageList[remainingLife].color = Color.red;
    }

    /// <summary>
    /// IEnumerator to repeate the audio after some time
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayAudio()
    {
        //If questionType is audio
        if (question.questionType == QuestionType.AUDIO)
        {
            //PlayOneShot
            questionAudio.PlayOneShot(question.audioClip);
            //wait for few seconds
            yield return new WaitForSeconds(audioLength + 0.5f);
            //play again
            StartCoroutine(PlayAudio());
        }
        else //If questionType is not audio
        {
            //Stop the Coroutine
            StopCoroutine(PlayAudio());
            //Return null
            yield return null;
        }
    }

    /// <summary>
    /// Method assigned to the buttons
    /// </summary>
    /// <param name="btn">ref to the button object</param>
    void OnClick(Button btn)
    {
        if (quizManager.GameStatus == GameStatus.PLAYING)
        {
            //If answered is false
            if (!answered)
            {
                //Set answered true
                answered = true;
                //get the bool value
                bool val = quizManager.Answer(btn.name);

                //If its true
                if (val)
                {
                    //Set color to correct
                    btn.image.color = correctCol;
                }
                else
                {
                    //Else set it to wrong color
                    btn.image.color = wrongCol;
                }
            }
        }

        switch (btn.name)
        {
            case "Normal":
                quizManager.StartGame(0);
                mainMenu.SetActive(false);
                gamePanel.SetActive(true);
                break;
            case "Medium":
                quizManager.StartGame(1);
                mainMenu.SetActive(false);
                gamePanel.SetActive(true);
                break;
            case "Complex":
                quizManager.StartGame(2);
                mainMenu.SetActive(false);
                gamePanel.SetActive(true);
                break;
        }
    }

    public void RestryButton()
    {
        Application.LoadLevel(levelToLoad);
    }

    public void ExitButton()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("MemoryHomePage");
        
    }

    public void NextLevelButton()
    {
        SceneManager.LoadScene("Levels");
        
    }

}
// End of the QuizGameUI script