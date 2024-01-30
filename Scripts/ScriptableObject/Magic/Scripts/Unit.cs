using TMPro;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 15;
    [SerializeField] private TextMeshProUGUI _text;

    private float _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
        _text.text = _currentHealth.ToString();
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        _text.text = _currentHealth.ToString();

        if (_currentHealth <= 0)
            Destroy(gameObject);
    }
}