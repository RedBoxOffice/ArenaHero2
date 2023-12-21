using System;
using ArenaHero.Battle.PlayableCharacter;
using ArenaHero.Data;
using ArenaHero.UI.Animations;
using ArenaHero.Yandex.SaveSystem;
using ArenaHero.Yandex.SaveSystem.Data;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace ArenaHero.Utils.StateMachine
{
	// TODO NEED REFACTORING
	public class EndLevelWindow : Window
	{
		[SerializeField] private HeroDiedAnimation _heroDiedAnimation;
		[SerializeField] private Image _endLevelIlluminationImage;
		[SerializeField] private GameObject _nextLevel;
		[SerializeField] private GameObject _onlyMainMenu;
		[SerializeField] private GameObject _windowContent;
		[SerializeField] private Color _victoryIlluminationColor;
		[SerializeField] private Color _loseIlluminationColor;
		
		private Hero _hero;
		private LevelData _levelData;
		
		public override Type WindowType => typeof(EndLevelWindowState);

		[Inject]
		private void Inject(Hero hero, LevelData levelData)
		{
			_hero = hero;
			_levelData = levelData;
		}

		private void OnEnable()
		{
			if (_hero.IsDied)
			{
				_heroDiedAnimation.Play(() =>
				{
					_windowContent.SetActive(false);
					_onlyMainMenu.SetActive(true);
					_endLevelIlluminationImage.color = _loseIlluminationColor;
				});
			}
			else
			{
				_windowContent.SetActive(true);
				
				var currentStage = GameDataSaver.Instance.Get<CurrentLevelStage>().Value;

				if (currentStage >= _levelData.StageCount - 1)
				{
					_onlyMainMenu.SetActive(true);
				}
				else
				{
					_nextLevel.SetActive(true);
				}
				
				_endLevelIlluminationImage.color = _victoryIlluminationColor;
			}
		}

		private void OnDisable()
		{
			_windowContent.SetActive(false);
			_onlyMainMenu.SetActive(false);
			_nextLevel.SetActive(false);
		}
	}
}