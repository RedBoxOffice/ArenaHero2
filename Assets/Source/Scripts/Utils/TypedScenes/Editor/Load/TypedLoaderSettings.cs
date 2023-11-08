using ArenaHero.Utils.StateMachine;
using ArenaHero.Utils.TypedScenes.Loader;

namespace ArenaHero.Utils.TypedScenes.Editor.Loader
{
    public static class TypedLoaderSettings
    {
        public const string Namespace = "ArenaHero.Utils.TypedScenes.Loader.Generated";
        public const string BaseClass = "ITypedLoader";
        public const string SavingDirectoryClass = "Assets/Source/Scripts/Utils/TypedScenes/LoaderScripts/";
        public const string SavingDirectoryPrefabs = "Assets/Source/Scripts/Utils/TypedScenes/LoaderPrefabs/";
        public const string ClassExtension = ".cs";
        public const string SceneExtension = ".unity";
        public const string MetaExtension = ".meta";
        public const string PrefabExtension = ".prefab";
        public const string ClassPostName = "Loader";
    }
}
