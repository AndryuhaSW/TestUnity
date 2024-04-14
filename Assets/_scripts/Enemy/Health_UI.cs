using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Health_UI : MonoBehaviour
{
    [SerializeField] private Slider _sliderFirst;
    [SerializeField] private Slider _sliderSecond;

    private Health _health;
    private int _waitForSecondSlider = 1;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _health.HealthChanged += DrowHealth;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= DrowHealth;
    }

    private void DrowHealth()
    {
        StopAllCoroutines();
        StartCoroutine(Drow());
    }

    private IEnumerator Drow()
    {
        float currentHealth = _health.currentHealth;
        float maxHealth = _health.maxHealth;
        float percentage = (currentHealth / maxHealth);

        _sliderFirst.value = percentage;

        yield return new WaitForSeconds(_waitForSecondSlider);

        _sliderSecond.value = percentage;
    }
}
