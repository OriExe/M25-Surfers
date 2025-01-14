using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    Key key = new Key(); //Score class
    [SerializeField]
    float score = 0;

    [SerializeField]
    float currentGameDuration = 0;

    [SerializeField]
    float timeMultiplier = 1;

    [SerializeField]
    TMP_Text ScoreText;

    // Start is called before the first frame update
    public static ScoreManager instance;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Duplicate Detected");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.gameStarted)
            return;
        //Counts score gained
        currentGameDuration += Time.deltaTime;

        score += currentGameDuration * timeMultiplier;
        ScoreText.text = Mathf.RoundToInt(score).ToString();
    }

    public float GetHighScore()
    {
        if (PlayerPrefs.HasKey(key.HighScoreKey))
            return PlayerPrefs.GetFloat(key.HighScoreKey);
        else
            return 0;
    }
    public void CheckCurrentScore()
    {
        if (score > GetHighScore())
            SaveCurrentScore();
    }
    public void SaveCurrentScore()
    {
        PlayerPrefs.SetFloat(key.HighScoreKey, score);
        PlayerPrefs.Save();
    }
    public void ClearCurrentHighScore()
    {
        PlayerPrefs.SetFloat (key.HighScoreKey, 0);
        PlayerPrefs.Save();
    }

    public int getScore()
    {
        return Mathf.RoundToInt(score);
    }
}
