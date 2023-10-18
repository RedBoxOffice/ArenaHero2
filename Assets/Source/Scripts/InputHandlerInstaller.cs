using Game.Input;
using Reflex.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Game
{
    internal class InputHandlerInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerDescriptor descriptor)
        {
            IInputHandler inputHandler;

            if (Application.isMobilePlatform)
            {
                //var mobileInputHandler = new GameObject(nameof(MobileInputHandler)).AddComponent<MobileInputHandler>();
                //_mobileControl.gameObject.SetActive(true);
                //mobileInputHandler.Init(_mobileControl);

                //inputHandler = mobileInputHandler;
            }
            else
            {

            }

            inputHandler = new GameObject(nameof(DesktopInputHandler)).AddComponent<DesktopInputHandler>();

            descriptor.AddInstance(inputHandler, typeof(IInputHandler));
        }
    }
}
