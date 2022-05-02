using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public int score { get; private set; }

    public void AddPoints(int amount)
    {
        score += amount;
    }
}
