using System.Linq;
using UnityEngine;

namespace ArenaHero.Utils.TypedScenes
{
    public class TypedProcessor : MonoBehaviour
    {
        private void Awake()
        {
            foreach(var handler in FindObjectsOfType<MonoBehaviour>().OfType<ITypedAwakeHandler>())
            {
                handler.OnSceneAwake();
            }

            LoadingProcessor.Instance.ApplyLoadingModel();
        }
    }
}
