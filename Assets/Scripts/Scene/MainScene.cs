﻿using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour
{
    [SerializeField] InGame inGame;
    [SerializeField] LeaderboardRequester leaderboardRequester;

    // Start is called before the first frame update
    void Start()
    {
        inGame.OnGameOver = OnGameOver;

        PlayFabLoginManagerSingleton.Instance.TryLogin(OnLoginSuccess, OnLoginFailure);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnLoginSuccess()
    {
        ShowTitle();

        PlayFabPlayerEventManagerSingleton.Instance.Write(PlayFabPlayerEventManagerSingleton.GameStartEventName);
    }

    void OnLoginFailure(string report)
    {
        ErrorDialogView.Show("Login failed", report, () => {
            SceneManager.LoadScene(this.GetType().Name);
        });
    }

    void ShowTitle()
    {
        TitleView view = null;
        view = TitleView.Show(
            () => {
                inGame.StartGame();
                Destroy(view.gameObject);
            },
            () => {
                ResultView resultView = null;
                resultView = ResultView.Show(() => {
                    ShowTitle();
                    Destroy(resultView.gameObject);
                });
                Destroy(view.gameObject);
            });
    }

    void OnGameOver()
    {
        PlayFabPlayerEventManagerSingleton.Instance.Write(PlayFabPlayerEventManagerSingleton.GameOverEventName);
        leaderboardRequester.UpdatePlayerStatistic(inGame.Score, () => {
            
            StartCoroutine("WaitAndResult");
        });
    }

    IEnumerator WaitAndResult()
    {
        yield return new WaitForSeconds(1f);

        ResultView view = null;
        view = ResultView.Show(inGame.Score, () => {
            ShowTitle();
            Destroy(view.gameObject);
        });
    }
}