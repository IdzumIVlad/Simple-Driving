using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private float scoreMultiplier = 1;

    private float score;
    public const string BestScoreKey = "BestScore";

    void Update()
    {
        score += scoreMultiplier * Time.deltaTime;
        scoreText.text = ((int)score).ToString();
    }

    private void OnDestroy()
    {
        int currentBestScore = PlayerPrefs.GetInt(BestScoreKey, 0);

        if (score > currentBestScore) PlayerPrefs.SetInt(BestScoreKey, (int)score);

    }
}
