using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class AppManager : MonoBehaviour
{
    public Dropdown QuizDropdown;
    public GameObject Pages;

    public Text QuestionText;
    public Text AnswerTextA;
    public Text AnswerTextB;
    public Text AnswerTextC;
    public Text AnswerTextD;
    public Text ResultText;

    private int quizIndex = 0;
    private double result = 0;
    private int questionNumber = 0;
    private string rightAnswer = "";   

    public class QuizQuestion
    {
        public string Question;
        public string Type;
        public string RightAnswer;
        public string AnswerA;
        public string AnswerB;
        public string AnswerC;
        public string AnswerD;
    }

    public List<QuizQuestion> QuizQuestionsList = new List<QuizQuestion>();    

    void Start()
    {
        ReadCSV();
        ToMenu();
    }

    private void ReadCSV()
    {
        QuizDropdown.ClearOptions();
        string[] quizFilesPaths = Directory.GetFiles(Application.dataPath + "/Resources/QuizDB", "*.csv");
        List<string> quizDropdownOptions = new List<string>();

        

        for (int i = 0; i < quizFilesPaths.Length; i++)
        {
            quizDropdownOptions.Add(Path.GetFileNameWithoutExtension(quizFilesPaths[i]));            
            var quizDb = Resources.Load("QuizDB/" + Path.GetFileNameWithoutExtension(quizFilesPaths[i])) as TextAsset;
            string[] data = quizDb.text.Split(new string[] { ";", "\n"}, System.StringSplitOptions.None);
            int questionCount = data.Length / 7 - 1;

            for (int j = 0; j < questionCount; j++)
            {
                QuizQuestionsList.Add(new QuizQuestion());
                QuizQuestionsList[j].Question = data[(j + 1) * 7 + 0];
                QuizQuestionsList[j].Type = data[(j + 1) * 7 + 1];
                QuizQuestionsList[j].RightAnswer = data[(j + 1) * 7 + 2];
                QuizQuestionsList[j].AnswerA = data[(j + 1) * 7 + 3];
                QuizQuestionsList[j].AnswerB = data[(j + 1) * 7 + 4];
                QuizQuestionsList[j].AnswerC = data[(j + 1) * 7 + 5];
                QuizQuestionsList[j].AnswerD = data[(j + 1) * 7 + 6];
            }
            
        }

        QuizDropdown.AddOptions(quizDropdownOptions);        
    }

    public void RunTest()
    {
        ChangePage(1);
        ChangeQuestion(0);
    }

    public void ChangeQuestion(int number)
    {
        if (number < QuizQuestionsList.Count)
        {
            QuestionText.text = QuizQuestionsList[number].Question;
            rightAnswer = QuizQuestionsList[number].RightAnswer;
            AnswerTextA.text = QuizQuestionsList[number].AnswerA;
            AnswerTextB.text = QuizQuestionsList[number].AnswerB;
            AnswerTextC.text = QuizQuestionsList[number].AnswerC;
            AnswerTextD.text = QuizQuestionsList[number].AnswerD;
        }
        else
        {
            ChangePage(2);
            ResultText.text = "ÏÐÀÂÈËÜÍÛÕ ÎÒÂÅÒÎÂ: " + (result / QuizQuestionsList.Count * 100) + "%";
        }
    }

    public void ChangeTest()
    {
        quizIndex = QuizDropdown.value;
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

    
}
