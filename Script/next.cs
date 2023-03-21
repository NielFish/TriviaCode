using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class next : MonoBehaviour
{
    public void Tutorial()
    {
        SceneManager.LoadScene("tutorial");
    }

    public void Trivia()
    {
        SceneManager.LoadScene("Trivia");
    }
}
