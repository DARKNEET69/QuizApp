using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    public int CSVCellsCount = 7;
    public int QuestionNumber = 0;
    public List<QuizQuestion> QuizQuestionsList = new List<QuizQuestion>();

    public MenuManager menuManager;
    public QuizViewer quizViewer;
    private float result = 0;
    private int rightAnswer = 0;

    private void OnEnable()
    {
        result = 0;
        QuestionNumber = 0;
        rightAnswer = 0;
        ReadCSV(menuManager.QuizID);
        ChangeQuestion(0);
    }

    public void ReadCSV(int fileID)
    {
        QuizQuestionsList.Clear();
        var quizDb = Resources.Load("QuizDB/" + Path.GetFileNameWithoutExtension(menuManager.QuizFilesPaths[fileID])) as TextAsset;
        string[] data = quizDb.text.Split(new string[] { ";", "\n" }, System.StringSplitOptions.None);
        int questionCount = data.Length / CSVCellsCount - 1;

        for (int i = 0; i < questionCount; i++)
        {
            QuizQuestionsList.Add(new QuizQuestion());
            QuizQuestionsList[i].Type = int.Parse(data[(i + 1) * CSVCellsCount + 0]);

            switch (QuizQuestionsList[i].Type)
            {
                case 0:
                    //
                    QuizQuestionsList[i].Question = data[(i + 1) * CSVCellsCount + 1];
                    QuizQuestionsList[i].RightAnswer = int.Parse(data[(i + 1) * CSVCellsCount + 2]);
                    QuizQuestionsList[i].AnswerA = data[(i + 1) * CSVCellsCount + 3];
                    QuizQuestionsList[i].AnswerB = data[(i + 1) * CSVCellsCount + 4];
                    break;

                case 1:
                    //
                    QuizQuestionsList[i].Question = data[(i + 1) * CSVCellsCount + 1];
                    QuizQuestionsList[i].RightAnswer = int.Parse(data[(i + 1) * CSVCellsCount + 2]);
                    QuizQuestionsList[i].AnswerA = data[(i + 1) * CSVCellsCount + 3];
                    QuizQuestionsList[i].AnswerB = data[(i + 1) * CSVCellsCount + 4];
                    QuizQuestionsList[i].AnswerC = data[(i + 1) * CSVCellsCount + 5];
                    QuizQuestionsList[i].AnswerD = data[(i + 1) * CSVCellsCount + 6];
                    break;

                case 2:
                    //
                    break;
            }
        }
    }

    public void ChangeQuestion(int number)
    {
        if (number < QuizQuestionsList.Count)
        {
            rightAnswer = QuizQuestionsList[number].RightAnswer;
            quizViewer.ViewQuestion(QuizQuestionsList[number]);
        }
        else
        {
            quizViewer.ViewResult("ÏÐÀÂÈËÜÍÛÕ ÎÒÂÅÒÎÂ: " + Mathf.Round(result / QuizQuestionsList.Count * 100) + "%");
        }
    }

    public void CheckRightAnswer(int AnswerButtonIndex)
    {
        if (AnswerButtonIndex == rightAnswer) result++;

        QuestionNumber++;
        ChangeQuestion(QuestionNumber);
    }
}
