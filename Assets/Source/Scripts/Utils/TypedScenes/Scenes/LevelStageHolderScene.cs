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
    
    
    public class LevelStageHolderScene : TypedScene<ArenaHero.Utils.StateMachine.GameStateMachine>
    {
        
        private const string _sceneName = "LevelStageHolderScene";
        
        public static void Load<T>(T argument, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        
        {
            LoadScene<T>(_sceneName, loadSceneMode, argument);
        }
        
        public static UnityEngine.AsyncOperation LoadAsync<T>(T argument, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        
        {
            return LoadScene<T>(_sceneName, loadSceneMode, argument);
        }
        
        public static void Load<TState, T>(GameStateMachine machine, T argument, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
            where TState : State<GameStateMachine>
        
        {
            LoadScene<TState, T>(_sceneName, loadSceneMode, machine, argument);
        }
        
        public static UnityEngine.AsyncOperation LoadAsync<TState, T>(GameStateMachine machine, T argument, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
            where TState : State<GameStateMachine>
        
        {
            return LoadScene<TState, T>(_sceneName, loadSceneMode, machine, argument);
        }
        
        public static void Load<TState>(GameStateMachine machine, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
            where TState : State<GameStateMachine>
        {
            LoadScene<TState>(_sceneName, loadSceneMode, machine);
        }
        
        public static UnityEngine.AsyncOperation LoadAsync<TState>(GameStateMachine machine, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
            where TState : State<GameStateMachine>
        {
            return LoadScene<TState>(_sceneName, loadSceneMode, machine);
        }
    }
}
