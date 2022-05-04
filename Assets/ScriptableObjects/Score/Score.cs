using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Score : ScriptableObject
{
    public int value;

    public int scoreToAddOnBoxBreak;
    public int scoreToAddOnEnemyDeath;
    public int scoreToAddOnPlayerDeath;

    public int bestScore { get; private set; }

    void OnEnable()
    {
        LoadBestScore();
    }

    public void LoadBestScore()
    {
        bestScore = PlayerPrefs.GetInt("BestScore");
    }
}
