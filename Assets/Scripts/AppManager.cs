using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    public GameObject MenuPage;
    public GameObject QuizPage;

    private void Start()
    {
        ToMenu();
    }

    public void ToMenu()
    {
        QuizPage.SetActive(false);
        MenuPage.SetActive(true);
    }

    public void ToQuiz()
    {
        MenuPage.SetActive(false);
        QuizPage.SetActive(true);
    }
}
