using System.Collections.Generic;
using UnityEngine;

namespace ArenaHero.Utils.StateMachine
{
	public class WindowInitializer : MonoBehaviour
	{
		[SerializeField] private List<Window> _windows;
		
		protected void WindowsInit(WindowStateMachine machine)
		{
			foreach (var window in _windows)
			{
				var state = machine.TryGetState<WindowState>(window);

				state.Init(window);
				state.Exit();
			}
		}
	}
}