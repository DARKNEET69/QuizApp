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

    private int quizID = 0;
    private float result = 0;
    private int questionNumber = 0;
    private int rightAnswer = 0;

    private string[] quizFilesPaths;
    private List<string> quizDropdownOptions = new List<string>();
    private List<QuizQuestion> QuizQuestionsList = new List<QuizQuestion>();

    public class QuizQuestion
    {
        public string Question;
        public int Type;
        public int RightAnswer;
        public string AnswerA;
        public string AnswerB;
        public string AnswerC;
        public string AnswerD;
    }

    private void Start()
    {        
        ToMenu();
        LoadQuizNames();
    }

    public void LoadQuizNames()
    {
        QuizDropdown.ClearOptions();
        quizFilesPaths = Directory.GetFiles(Application.dataPath + "/Resources/QuizDB", "*.csv");

        for (int i = 0; i < quizFilesPaths.Length; i++)
        {
            quizDropdownOptions.Add(Path.GetFileNameWithoutExtension(quizFilesPaths[i]));
        }

        QuizDropdown.AddOptions(quizDropdownOptions);
    }

    public void ReadCSV(int fileID)
    {
        var quizDb = Resources.Load("QuizDB/" + Path.GetFileNameWithoutExtension(quizFilesPaths[fileID])) as TextAsset;
        string[] data = quizDb.text.Split(new string[] { ";", "\n" }, System.StringSplitOptions.None);
        QuizQuestionsList.Clear();
        int questionCount = data.Length / 7 - 1;

        for (int i = 0; i < questionCount; i++)
        {
            QuizQuestionsList.Add(new QuizQuestion());
            QuizQuestionsList[i].Type = int.Parse(data[(i + 1) * 7 + 0]);
            QuizQuestionsList[i].Question = data[(i + 1) * 7 + 1];
            QuizQuestionsList[i].RightAnswer = int.Parse(data[(i + 1) * 7 + 2]);
            QuizQuestionsList[i].AnswerA = data[(i + 1) * 7 + 3];
            QuizQuestionsList[i].AnswerB = data[(i + 1) * 7 + 4];
            QuizQuestionsList[i].AnswerC = data[(i + 1) * 7 + 5];
            QuizQuestionsList[i].AnswerD = data[(i + 1) * 7 + 6];
        }
    }

    public void RunTest()
    {
        quizID = QuizDropdown.value;
        ReadCSV(quizID);
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
            ResultText.text = "ПРАВИЛЬНЫХ ОТВЕТОВ: " + Mathf.Round(result / QuizQuestionsList.Count * 100) + "%";
        }
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
        if (AnswerButtonIndex == rightAnswer) result++;
        
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
