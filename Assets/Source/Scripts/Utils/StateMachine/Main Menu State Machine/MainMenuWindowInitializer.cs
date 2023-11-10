using ArenaHero.Utils.StateMachine.States;
using Reflex.Attributes;

namespace ArenaHero.Utils.StateMachine
{
	public class MainMenuWindowInitializer : WindowInitializer
	{
		[Inject]
		private void Inject(MainMenuWindowStateMachine machine)
		{
			WindowsInit(machine);
			
			machine.EnterIn<EquipmentWindowState>();
		}
	}
}