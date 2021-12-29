using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppManager : MonoBehaviour
{
    public Dropdown TestDropdown;
    public GameObject Pages;

    public Text QuestionText;
    public Text AnswerTextA;
    public Text AnswerTextB;
    public Text AnswerTextC;
    public Text AnswerTextD;
    public Text ResultText;

    private int testIndex = 0;
    private double result = 0;
    private int questionNumber = 0;
    private string rightAnswer = "";

    void Start()
    {
        ToMenu();
    }    

    public void RunTest()
    {
        ChangePage(1);
        ChangeQuestion(0);
    }

    public void ChangeQuestion(int number)
    {
        if (number < testsArray[testIndex].GetUpperBound(0) + 1)
        {
            QuestionText.text = testsArray[testIndex][number, 0];
            AnswerTextA.text = testsArray[testIndex][number, 1];
            AnswerTextB.text = testsArray[testIndex][number, 2];
            AnswerTextC.text = testsArray[testIndex][number, 3];
            AnswerTextD.text = testsArray[testIndex][number, 4];
            rightAnswer = testsArray[testIndex][number, 5];
        }
        else
        {
            ChangePage(2);
            ResultText.text = "ÏÐÀÂÈËÜÍÛÕ ÎÒÂÅÒÎÂ: " + (result / (testsArray[testIndex].GetUpperBound(0) + 1) * 100) + "%";
        }
    }

    public void ChangeTest()
    {
        testIndex = TestDropdown.value;
    }

    private void ChangePage(int pageIndex)
    {
        for (int i = 0; i < Pages.transform.childCount; i++)
        {
            Pages.transform.GetChild(i).gameObject.SetActive(false);
        }
        Pages.transform.GetChild(pageIndex).gameObject.SetActive(true);
    }

    public void CheckRightAnswer(int AnswerButtonIndex)
    {
        string answerText = "";
        switch (AnswerButtonIndex)
        {
            case 0:
                answerText = AnswerTextA.text;
                break;
            case 1:
                answerText = AnswerTextB.text;
                break;
            case 2:
                answerText = AnswerTextC.text;
                break;
            case 3:
                answerText = AnswerTextD.text;
                break;
        }
        if (answerText == rightAnswer) result++;
        questionNumber++;
        ChangeQuestion(questionNumber);
    }

    public void ToMenu()
    {
        ChangePage(0);
        result = 0;
        questionNumber = 0;
}

    //Áëîê òåñòîâ
    string[][,] testsArray = new string[3][,]
    {
        new string[,] 
        {
            {"You are gay?1", "Yes", "No", "Maybe", "Never", "Yes"},
            {"You are lie?", "Yes", "No", "Maybe", "Never", "Yes"},
            {"You are student of politech?", "Yes", "No", "Maybe", "Never", "Yes"}
        },
        new string[,] 
        {
            {"You are gay?2", "Yes", "No", "Maybe", "Never", "Yes"},
            {"You are lie?", "Yes", "No", "Maybe", "Never", "Yes"},
            {"You are student of politech?", "Yes", "No", "Maybe", "Never", "Yes"}
        },
        new string[,]
        {
            {"You are gay?3", "Yes", "No", "Maybe", "Never", "Yes"},
            {"You are lie?", "Yes", "No", "Maybe", "Never", "Yes"},
            {"You are student of politech?", "Yes", "No", "Maybe", "Never", "Yes"}
        },
    };
}
