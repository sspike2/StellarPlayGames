using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreText : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        EventManager.UpdateScoreUI += UIUpdateListerner;
    }

    private void OnDisable()
    {
        EventManager.UpdateScoreUI -= UIUpdateListerner;
    }

    private void UIUpdateListerner(int score)
    {
        scoreText.text = $"Score : {score}";
    }
}
