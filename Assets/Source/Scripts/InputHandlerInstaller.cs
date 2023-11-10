using ArenaHero.Battle.PlayableCharacter;
using ArenaHero.InputSystem;
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
