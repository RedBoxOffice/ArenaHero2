using System;
using ArenaHero.Utils.StateMachine.States;

namespace ArenaHero.Utils.StateMachine.Windows
{
	public class EquipmentWindow : Window
	{
		public override Type WindowType => typeof(EquipmentWindowState);
	}
}