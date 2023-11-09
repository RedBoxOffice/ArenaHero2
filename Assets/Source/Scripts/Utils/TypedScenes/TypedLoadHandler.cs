using ArenaHero.Utils.StateMachine;
using ArenaHero.Utils.TypedScenes.Loader.Generated;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace ArenaHero.Utils.TypedScenes.Loader
{
    public class TypedLoadHandler : MonoBehaviour
    {
        private static Dictionary<string, Type> _loaders = new Dictionary<string, Type>();

        public static void Load(string loaderName)
        {
            if (_loaders.TryGetValue(loaderName, out Type loader))
            {
                MethodInfo methodInfo = loader.GetMethod("Load");



                var windowStateMachine = new WindowStateMachine(() =>
                {
                    return new Dictionary<Type, State<WindowStateMachine>>()
                    {
                        [typeof(FightWindowState)] = new FightWindowState(),
                        [typeof(OverWindowState)] = new OverWindowState()
                    };
                });

                var gameStateMachine = new GameStateMachine(windowStateMachine, () =>
                {
                    return new Dictionary<Type, State<GameStateMachine>>()
                    {
                        [typeof(FightState)] = new FightState(windowStateMachine),
                        [typeof(OverState)] = new OverState(windowStateMachine)
                    };
                });
            }
        }

        public static void UpdateLoaders()
        {
            _loaders.Clear();

            IEnumerable<Type> types = GetTypesInNamespace(Assembly.GetExecutingAssembly(), "ArenaHero.Utils.TypedScenes.Loader.Generated");

            foreach (var type in types)
            {
                if (type.GetInterface(nameof(ITypedLoader)) == null) continue;

                _loaders.Add(type.Name, type);
            }

            //Load(nameof(FightSceneLoader));
        }

        private static IEnumerable<Type> GetTypesInNamespace(Assembly assembly, string nameSpace) =>
            from type in assembly.GetTypes()
            where string.Equals(type.Namespace, nameSpace, StringComparison.Ordinal)
            where type.BaseType == typeof(object)
            select type;
    }
}