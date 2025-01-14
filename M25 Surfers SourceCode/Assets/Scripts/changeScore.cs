using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class changeScore : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text HighscoreText;
    private void OnEnable()
    {
        scoreText.text = "Your Score";
        scoreText.text += "\n";
        scoreText.text += ScoreManager.instance.getScore().ToString();

        HighscoreText.text = "High Score";
        HighscoreText.text += "\n";
        HighscoreText.text += Mathf.RoundToInt(PlayerPrefs.GetFloat("highScore")).ToString();
    }
}
