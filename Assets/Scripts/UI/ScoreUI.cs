using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_scoreText;
    [SerializeField] private Score m_score;

    public void OnScoreChanged() => m_scoreText.text = m_score.value.ToString();
}
