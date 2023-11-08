using System.IO;
using UnityEditor;

namespace ArenaHero.Utils.TypedScenes.Editor.Loader
{
    public static class TypedLoaderStorage
    {
        public static void SaveClass(string fileName, string sourceCode)
        {
            var path = TypedLoaderSettings.SavingDirectoryClass + fileName + TypedSceneSettings.ClassExtension;
            Directory.CreateDirectory(TypedLoaderSettings.SavingDirectoryClass);

            if (File.Exists(path) && File.ReadAllText(path) == sourceCode)
                return;

            File.WriteAllText(path, sourceCode);
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
        }

        public static void DeleteClass(string className)
        {
            var path = TypedLoaderSettings.SavingDirectoryClass + className + TypedLoaderSettings.ClassExtension;

            if (File.Exists(path))
            {
                AssetDatabase.DeleteAsset(path);
                AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
            }
        }
    }
}