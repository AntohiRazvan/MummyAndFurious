﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text gameLostText;
    public Text gameWonText;

    void Start()
    {
        GameEventManager.GameOver += GameOver;
    }

    void GameOver(bool hasWon)
    {
        //Time.timeScale = 0;
        if(hasWon)
        {
            StartCoroutine(FadeInText(gameWonText, 2.5f));
            StartCoroutine(QuitGameAfter(5f));
        }
        else
        {
            StartCoroutine(FadeInText(gameLostText, 2.5f));
            StartCoroutine(RestartGameAfter(5f));
        }

    }

    IEnumerator RestartGameAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    IEnumerator QuitGameAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Application.Quit();
    }

    IEnumerator FadeInText(Text text, float seconds)
    {
        Color newColor = text.color;
        newColor.a = 1f;
        float t = 0f;

        while( t < 1)
        {
            text.color = Color.Lerp(text.color, newColor, t);
            t += Time.deltaTime/seconds;
            yield return new WaitForSeconds(0.2f);
        }
    }
}