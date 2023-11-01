using ArenaHero.Battle.PlayableCharacter;
using ArenaHero.InputSystem;
using Reflex.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace ArenaHero
{
    public class InputHandlerInstaller : MonoBehaviour
    {
        public IInputHandler InstallBindings(Hero hero)
        {
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

            return hero.gameObject.AddComponent<DesktopInputHandler>();
        }
    }
}
