using ArenaHero.Utils.TypedScenes;
using System.Collections.Generic;
using UnityEngine;

namespace ArenaHero.Utils.StateMachine
{
    public class GameSceneInitializer : MonoBehaviour, ISceneLoadHandlerOnState<GameStateMachine>
    {
        [SerializeField] private List<Window> _windows;

        public void OnSceneLoaded<TState>(GameStateMachine machine) where TState : State<GameStateMachine>
        {
            WindowsInit(machine);

            machine.EnterIn<TState>();
        }

        private void WindowsInit(GameStateMachine machine)
        {
            foreach (var window in _windows)
            {
                var state = machine.Window.TryGetState<WindowState>(window);

                state.Init(window);
            }
        }
    }
}