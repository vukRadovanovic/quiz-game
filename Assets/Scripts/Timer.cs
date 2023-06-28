using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteQuestion = 15f;
    [SerializeField] float timeToReviewAnswer = 5f;
    private float timerValue;

    public bool isAnsweringQuestion = false;
    public bool loadNextQuestion = false;
    public float fillFraction;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();   
    }

    public void CancelTimer()
    {
        timerValue = 0;
    }


    void UpdateTimer() 
    {
        timerValue -= Time.deltaTime;


        if (isAnsweringQuestion) {

            fillFraction = timerValue / timeToCompleteQuestion;

            if (timerValue <= 0) {
                isAnsweringQuestion = false;
                timerValue = timeToReviewAnswer;
            }

        } else {

            fillFraction = timerValue / timeToReviewAnswer;

            if (timerValue <= 0) {
                loadNextQuestion = true;
                isAnsweringQuestion = true;
                timerValue = timeToCompleteQuestion;
            }

        }


        Debug.Log(isAnsweringQuestion + ": " + timerValue + " = " + fillFraction);
    }
}
