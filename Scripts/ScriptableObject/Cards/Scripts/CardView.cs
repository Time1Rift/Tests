using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    [SerializeField] private Card _card;

    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _damage;
    [SerializeField] private TextMeshProUGUI _mana;
    [SerializeField] private TextMeshProUGUI _health;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _text;

    private void Start()
    {
        _icon.sprite = _card.Icon;
        _damage.text = _card.Damage.ToString();
        _mana.text = _card.Mana.ToString();
        _health.text = _card.Health.ToString();
        _name.text = _card.Name;
        _text.text = _card.Text;
    }
}