using System;
using System.Collections.Generic;
using ArenaHero.Data;
using ArenaHero.Debugs;
using ArenaHero.Utils.StateMachine;
using ArenaHero.Utils.TypedScenes;
using ArenaHero.Yandex.Saves;
using ArenaHero.Yandex.Saves.Data;
using Reflex.Attributes;
using UnityEngine;

namespace ArenaHero.Game.Level
{
    public class LevelLoader : MonoBehaviour, ISceneLoadHandlerOnState<GameStateMachine, object>
    {
        [SerializeField] private List<LevelData> _levels;

        private Func<CurrentLevel> _getCurrentLevel;
        private GameStateMachine _gameStateMachine;
        private SceneLoader _sceneLoader;

        [Inject]
        private void Inject(ISaver saver, SceneLoader sceneLoader)
        {
            _getCurrentLevel = () => saver.Get<CurrentLevel>();
            _sceneLoader = sceneLoader;
        }

        public void OnFightButtonClick() =>
            _sceneLoader.LoadFight<FightState, LevelData>(_gameStateMachine, _levels[_getCurrentLevel().Value]);

        public void OnSceneLoaded<TState>(GameStateMachine machine, object argument = default)
            where TState : State<GameStateMachine> =>
            _gameStateMachine = machine;
    }
}