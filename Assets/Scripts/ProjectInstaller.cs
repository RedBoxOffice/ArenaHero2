using Reflex.Core;
using UnityEngine;

namespace Game
{
    public class ProjectInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerDescriptor descriptor)
        {
            var playerInput = new PlayerInput();
            playerInput.Enable();

            descriptor.AddInstance(playerInput);
        }
    }
}
