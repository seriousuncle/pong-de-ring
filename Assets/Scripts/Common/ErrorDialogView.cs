﻿using System;
using UnityEngine;
using UnityEngine.UI;

public class ErrorDialogView : MonoBehaviour
{
    public static GameObject Prefab
    {
        get
        {
            return Resources.Load<GameObject>("Prefabs/Common/ErrorDialogView");
        }
    }

    public static ErrorDialogView Show(string title, string message, Action onClickRetry, bool cancelable = false)
    {
        var view = Instantiate(Prefab).GetComponent<ErrorDialogView>();
        if (cancelable)
        {
            view.Initialize(title, message, onClickRetry, () => {
            });
        }
        else
        {
            view.Initialize(title, message, onClickRetry, null);
        }
        return view;
    }

    public static ErrorDialogView Show(string title, string message, Action onClickRetry, Action onClickCancel)
    {
        var view = Instantiate(Prefab).GetComponent<ErrorDialogView>();
        view.Initialize(title, message, onClickRetry, onClickCancel);
        return view;
    }

    [SerializeField] Text titleText;
    [SerializeField] Text messageText;
    [SerializeField] Button retryButton;
    [SerializeField] Button cancelButton;

    void Initialize(string title, string message, Action onClickRetry, Action onClickCancel)
    {
        titleText.text = title;
        messageText.text = message;
        retryButton.onClick.AddListener(() => {
            Close();
            onClickRetry();
        });

        if (onClickCancel != null)
        {
            cancelButton.onClick.AddListener(() => {
                Close();
                onClickCancel();
            });
        }
        else
        {
            var retryButtonPosition = retryButton.transform.localPosition;
            retryButtonPosition.x = 0f;
            retryButton.transform.localPosition = retryButtonPosition;
            cancelButton.gameObject.SetActive(false);
        }
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}
