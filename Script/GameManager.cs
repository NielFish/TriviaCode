using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
 public Question[] questions;
    private static List<Question> unansweredQuestions;

    private Question currentQuestion;

    [SerializeField] private Text factText, livesText;
    [SerializeField] private GameObject correctObject, incorrectObject, gameOverObject, winObject;
    [SerializeField] private float timeBetweenQuestions = 0.1f;
    [SerializeField] private int lives = 3;
    [SerializeField] private int correctAnswersNeededToWin = 5;
    private int correctAnswers = 0;

    void UpdateLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
    }   

    void Start()
    {
        UpdateLivesText();

        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<Question>();
        }

        SetCurrentQuestion();
    }

    void SetCurrentQuestion()
    {
        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];

        factText.text = currentQuestion.fact;
    }

    IEnumerator TransitionToNextQuestion()
    {
        unansweredQuestions.Remove(currentQuestion);

        yield return new WaitForSeconds(timeBetweenQuestions);

        if (unansweredQuestions.Count == 0)
        {
            // All questions have been answered, show game over object
            GameOver();
        }
        else
        {
            SetCurrentQuestion();
        }
    }

    void ShowObjectForSeconds(GameObject obj, float seconds)
    {
        obj.SetActive(true);
        StartCoroutine(HideObjectAfterSeconds(obj, seconds));
    }

    IEnumerator HideObjectAfterSeconds(GameObject obj, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        obj.SetActive(false);
    }

    void GameOver()
    {
        gameOverObject.SetActive(true);
    }



    public void UserSelectTrue()
    {
        if (currentQuestion.isTrue)
        {
            Debug.Log("Correct!");
            correctAnswers++;
            Debug.Log(correctAnswers);
            if (correctAnswers == correctAnswersNeededToWin)
            {
                winObject.SetActive(true);
            }
            else
            {
                ShowObjectForSeconds(correctObject, 1f);
            }

            
        }
        else
        {
            Debug.Log("Wrong!");
            lives--;
            UpdateLivesText();
            Debug.Log(lives);
            if (lives == 0)
            {
                GameOver();
            }
            else
            {
                ShowObjectForSeconds(incorrectObject, 1f);
            }
        }

        StartCoroutine(TransitionToNextQuestion());
    }

    public void UserSelectFalse()
    {
        if (!currentQuestion.isTrue)
        {
            Debug.Log("Correct!");
            correctAnswers++;
            Debug.Log(correctAnswers);
            if (correctAnswers == correctAnswersNeededToWin)
            {
                winObject.SetActive(true);
            }
            else
            {
                ShowObjectForSeconds(correctObject, 1f);
            }
        }
        else
        {
            Debug.Log("Wrong!");
            lives--;
            UpdateLivesText();
            if (lives == 0)
            {
                GameOver();
            }
            else
            {
                ShowObjectForSeconds(incorrectObject, 1f);
            }
        }
        StartCoroutine(TransitionToNextQuestion());
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
