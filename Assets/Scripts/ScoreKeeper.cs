using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{

    private int correctAnswers = 0;
    public int getCorrectAnswers() {
        return correctAnswers;
    }
    public void incrementCorrectAnswers() {
        correctAnswers++;
    }

    int questionsSeen = 0;
    public int getQuestionsSeen() {
        return questionsSeen;
    }
    public void incrementQuestionsSeen() {
        questionsSeen++;
    }

    public int CalculateScore() {
        return Mathf.RoundToInt(correctAnswers / (float)questionsSeen * 100);
    }

}
