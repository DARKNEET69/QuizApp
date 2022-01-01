using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizViewer : MonoBehaviour
{
    public GameObject QuizType0;
    public GameObject QuizType1;
    public GameObject QuizType2;
    public GameObject Result;

    public Text QuizType0QuestionText;
    public Text QuizType0AnswerAText;
    public Text QuizType0AnswerBText;

    public Text QuizType1QuestionText;
    public Text QuizType1AnswerAText;
    public Text QuizType1AnswerBText;
    public Text QuizType1AnswerCText;
    public Text QuizType1AnswerDText;

    public Text ResultText;


    public void ViewQuestion(QuizQuestion quiz)
    {
        int quizType = quiz.Type;

        switch (quizType)
        {
            case 0:
                //
                ChangeQuestionTypeTo(QuizType0);
                QuizType0QuestionText.text = quiz.Question;
                QuizType0AnswerAText.text = quiz.AnswerA;
                QuizType0AnswerBText.text = quiz.AnswerB;
                break;
            case 1:
                //
                ChangeQuestionTypeTo(QuizType1);
                QuizType1QuestionText.text = quiz.Question;
                QuizType1AnswerAText.text = quiz.AnswerA;
                QuizType1AnswerBText.text = quiz.AnswerB;
                QuizType1AnswerCText.text = quiz.AnswerC;
                QuizType1AnswerDText.text = quiz.AnswerD;
                break;
            case 2:
                //
                break;
        }
    }

    public void ViewResult(string result)
    {
        ChangeQuestionTypeTo(Result);
        ResultText.text = result;
    }

    public void ChangeQuestionTypeTo(GameObject quizType)
    {
        QuizType0.SetActive(false);
        QuizType1.SetActive(false);
        QuizType2.SetActive(false);
        Result.SetActive(false);
        quizType.SetActive(true);
    }
}
