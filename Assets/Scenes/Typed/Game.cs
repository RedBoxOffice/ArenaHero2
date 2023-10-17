//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Base.TypedScenes
{
    using UnityEngine.SceneManagement;
    using Base.StateMachine;
    
    
    public class Game : TypedScene<Base.StateMachine.GameStateMachine>
    {
        
        private const string _sceneName = "Game";
        
        public static void Load(LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            LoadScene(_sceneName, loadSceneMode);
        }
        
        public static UnityEngine.AsyncOperation LoadAsync(LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            return LoadScene(_sceneName, loadSceneMode);
        }
        
        public static void Load<TState>(Base.StateMachine.GameStateMachine argument, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
            where TState : State<GameStateMachine>
        {
            LoadScene(_sceneName, loadSceneMode, argument);
        }
    }
}
