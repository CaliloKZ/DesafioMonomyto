using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private GameEvent m_onScoreChanged;
    [SerializeField] private Score m_score;

    public void OnBoxBreak() => AddScore(m_score.scoreToAddOnBoxBreak);
    public void OnEnemyDeath() => AddScore(m_score.scoreToAddOnEnemyDeath);
    public void OnOtherPlayerDeath() => AddScore(m_score.scoreToAddOnPlayerDeath);

    private void AddScore(int amount)
    {
        m_score.value += amount;
        m_onScoreChanged.Raise();
    }

    public void ResetScore()
    {
        m_score.value = 0;
        m_onScoreChanged.Raise();
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt("Score", m_score.value);

        if(m_score.bestScore < m_score.value)
            PlayerPrefs.SetInt("BestScore", m_score.value);

        m_score.LoadBestScore();
    }

}
