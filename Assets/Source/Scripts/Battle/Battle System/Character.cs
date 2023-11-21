using Source.GameData.Characters;
using UnityEngine;

namespace ArenaHero.Battle
{
    public class Character : MonoBehaviour, ICharacter
    {
        [SerializeField] private float _currentHealth;
        [SerializeField] private CharacterData _data;

        public CharacterData Data => _data;
        
        public Vector3 Position => transform.position;

        private void Awake()
        {
            _currentHealth = _data.MaxHealth;
        }

        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;
        }
    }
}
