using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Slider m_healthSlider;
    [SerializeField] private IntValue m_maxHealth;
    [SerializeField] private IntValue m_currentHealth;

    private void Awake() => m_healthSlider.maxValue = m_maxHealth.value;

    public void OnPlayerHealthChange() => m_healthSlider.value = m_currentHealth.value;
}
