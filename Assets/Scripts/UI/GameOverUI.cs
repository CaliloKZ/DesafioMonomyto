using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using MEC;  

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_scoreText;
    [SerializeField] private TextMeshProUGUI m_bestScoreText;
    [SerializeField] private GameObject m_youDiedTextObject;
    [SerializeField] private Button m_respawnButton;
    [SerializeField] private Color m_youDiedStartingColor;

    [SerializeField] private GameObject m_container;

    [SerializeField] private Score m_score;

    public void OnPlayerDeath()
    {
        m_container.SetActive(true);
        m_youDiedTextObject.GetComponent<TextMeshProUGUI>().color = m_youDiedStartingColor;
        m_youDiedTextObject.transform.localScale = Vector3.zero;
        m_respawnButton.interactable = false;
        Timing.RunCoroutine(YouDiedAnim().CancelWith(gameObject));
    }

    IEnumerator<float> YouDiedAnim()
    {
        m_youDiedTextObject.GetComponent<Image>().DOFade(255f, 1f);
        m_youDiedTextObject.transform.DOScale(1.2f, 1f);
        yield return Timing.WaitForSeconds(1f);

        m_scoreText.text = ($"Score: {m_score.value}");
        m_bestScoreText.text = ($"Best Score: {m_score.bestScore}");
        yield return Timing.WaitForSeconds(0.5f);

        m_respawnButton.interactable = true;
    }



}
