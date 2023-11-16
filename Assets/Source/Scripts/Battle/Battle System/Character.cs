using UnityEngine;

namespace ArenaHero.Battle
{
    public class Character : MonoBehaviour, ICharacter
    {
        [SerializeField] private CharacterType _characterType;
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _health;

        public CharacterType Type => _characterType;

        public Vector3 Position => transform.position;

        private void Awake()
        {
            _health = _maxHealth;
        }

        public void TakeDamage(float damage)
        {
            _health -= damage;
        }
    }
}
