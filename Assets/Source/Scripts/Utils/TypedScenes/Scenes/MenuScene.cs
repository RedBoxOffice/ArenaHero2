//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ArenaHero.Utils.TypedScenes
{
    using UnityEngine.SceneManagement;
    using ArenaHero.Utils.StateMachine;
    
    
    public class MenuScene : TypedScene<ArenaHero.Utils.StateMachine.GameStateMachine>
    {
        
        private const string _sceneName = "MenuScene";
        
        public static void Load<TState, T>(GameStateMachine machine, T argument = default, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
            where TState : State<GameStateMachine>
        
        {
            LoadScene<TState, T>(_sceneName, loadSceneMode, machine, argument = default);
        }
    }
}
