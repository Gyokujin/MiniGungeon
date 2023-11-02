using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    void Start()
    {
        int scoreLog = (int)Score.score;
        scoreText.text = ($"SCORE : {scoreLog}");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}