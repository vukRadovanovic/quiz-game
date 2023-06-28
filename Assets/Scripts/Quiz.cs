using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly = true;

    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    
    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;

    public bool isComplete;

    void Start() {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questions.Count + 1;
        progressBar.value = 0;
    }

    void Update() {
        timerImage.fillAmount = timer.fillFraction;
        if(timer.loadNextQuestion) {
            timer.loadNextQuestion = false;
            hasAnsweredEarly = false;
            getNextQuestion();
        }
        else if (!hasAnsweredEarly && !timer.isAnsweringQuestion) {
            displayAnswer(-1);
            setButtonState(false);
        }
    }

    public void OnAnswerSelected(int index) {
        hasAnsweredEarly = true;
        displayAnswer(index);        
        setButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";

        if (progressBar.value == progressBar.maxValue) {
            isComplete = true;
        }
    }

    void displayAnswer(int index) {
        if(index == currentQuestion.GetCorrectAnswerIndex()){
            questionText.text = "Correct!";
            scoreKeeper.incrementCorrectAnswers();
        }
        else
        {
            questionText.text = "Better luck next time...";    
        }
        Image buttonImage = answerButtons[currentQuestion.GetCorrectAnswerIndex()].GetComponent<Image>();
        buttonImage.sprite = correctAnswerSprite;

        questionText.text += "\n\"" + currentQuestion.GetAnswer(currentQuestion.GetCorrectAnswerIndex()) + "\" was the right answer.";
    }

    void getNextQuestion() {
        if (questions.Count > 0) {
            getRandomQuestion();
            displayQuestion();
            setDefaultButtonSprites();
            setButtonState(true);
            progressBar.value++;
            scoreKeeper.incrementQuestionsSeen();
        }
    }

    void getRandomQuestion() {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];

        if (questions.Contains(currentQuestion)) {
            questions.Remove(currentQuestion);
        }
    }

    void displayQuestion() {
        questionText.text = currentQuestion.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++) {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    void setDefaultButtonSprites() {
        for (int i = 0; i < answerButtons.Length; i++) {
            Image buttonImg = answerButtons[i].GetComponent<Image>();
            buttonImg.sprite = defaultAnswerSprite;
        }
    }

    void setButtonState(bool state) {
        for (int i = 0; i < answerButtons.Length; i++) {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

}
