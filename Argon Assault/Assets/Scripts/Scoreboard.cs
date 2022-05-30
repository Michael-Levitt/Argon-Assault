using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    int score;
    TMP_Text scoreText;

    void Start()
    {
        scoreText = GetComponent<TMP_Text>();
        scoreText.text = "READY"; // Default text before score is added
    }

    public void IncreaseScore(int scoreIncrease)
    {
        score += scoreIncrease;
        scoreText.text = $"Score: {score}";
    }
}