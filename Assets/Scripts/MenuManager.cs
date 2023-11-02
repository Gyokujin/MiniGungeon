using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private float startDelay = 1f;

    public void GameStart()
    {
        Invoke("StartProcess", startDelay);
    }

    void StartProcess()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void GameExit()
    {
        Application.Quit();
    }
}