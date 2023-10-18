using Base.TypedScenes;
using System.Collections.Generic;
using UnityEngine;

namespace Base.StateMachine
{
    public class GameSceneInitializer : MonoBehaviour, ISceneLoadHandlerState<GameStateMachine>
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