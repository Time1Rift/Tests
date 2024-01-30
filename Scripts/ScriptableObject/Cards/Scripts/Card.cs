using UnityEngine;

[CreateAssetMenu(fileName = "new Card", menuName = "Card/Create new Card", order = 51)]
public class Card : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _text;
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private int _mana;
    [SerializeField] private Sprite _icon;

    public int Health => _health;
    public int Damage => _damage;
    public int Mana => _mana;
    public string Name => _name;
    public string Text => _text;
    public Sprite Icon => _icon;
}