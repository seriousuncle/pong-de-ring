﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class StartCountdownView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(Action onComplete)
    {
        Countdown(3, () => {
            Countdown(2, () => {
                Countdown(1, () => {
                    onComplete?.Invoke();

                    Destroy(gameObject);
                });
            });
        });
    }

    void Countdown(int count, TweenCallback onComplete)
    {
        text.text = count.ToString();

        var seq = DOTween.Sequence();
        var rectTransform = text.GetComponent<RectTransform>();
        seq.Append(rectTransform.DOScale(Vector3.one, 0f));
        seq.Join(text.DOFade(1f, 0f));
        seq.Append(rectTransform.DOScale(Vector3.one * 4, 0.5f));
        seq.Join(text.DOFade(0f, 0.5f));
        seq.onComplete = onComplete;
        seq.Play();
    }
}
