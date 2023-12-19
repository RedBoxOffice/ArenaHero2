using System;

namespace ArenaHero.Utils.StateMachine
{
	public struct Subscription
	{
		public readonly ISubject Subject;
		public readonly Action Observer;

		public Subscription(ISubject subject, Action observer)
		{
			Subject = subject;
			Observer = observer;

			subject.ActionEnded += observer;
		}
	}
}