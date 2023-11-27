using System;
using UnityEngine;
using UnityEngine.UI;

namespace ArenaHero.Battle
{
	public class HealthView : MonoBehaviour
	{
		[SerializeField] private Slider _healthSlider;
		
		private ICharacter _character;
		
		private void Awake()
		{
			_character = GetComponent<ICharacter>();
			
			_healthSlider.value = _character.Data.MaxHealth;
		}

		private void OnEnable()
		{
			_character.HealthChanged += OnHealthChanged;
		}

		private void OnDisable()
		{
			_character.HealthChanged -= OnHealthChanged;
		}

		private void OnHealthChanged(float currentHealth)
		{
			_healthSlider.value = currentHealth / _character.Data.MaxHealth;
		}
	}
}