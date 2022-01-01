using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Dropdown QuizDropdown;
    public int QuizID;
    public string[] QuizFilesPaths;

    private List<string> quizDropdownOptions = new List<string>();
    public AppManager appManager;

    public void Start()
    {
        QuizFilesPaths = Directory.GetFiles(Application.dataPath + "/Resources/QuizDB", "*.csv");
        LoadQuizNames();
    }

    public void LoadQuizNames()
    {
        QuizDropdown.ClearOptions();

        for (int i = 0; i < QuizFilesPaths.Length; i++)
        {
            quizDropdownOptions.Add(Path.GetFileNameWithoutExtension(QuizFilesPaths[i]));
        }

        QuizDropdown.AddOptions(quizDropdownOptions);
    }

    public void StartQuiz()
    {
        QuizID = QuizDropdown.value;
        appManager.ToQuiz();
    }
}
