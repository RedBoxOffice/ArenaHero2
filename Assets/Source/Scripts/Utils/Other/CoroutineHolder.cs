using System;
using System.Collections;
using UnityEngine;

namespace ArenaHero.Utils.Other
{
	public class CoroutineHolder
	{
		private readonly MonoBehaviour _context;
		private readonly Func<bool> _isWhile;
		private readonly Action _action;
		private readonly YieldInstruction _instruction;
		private readonly Func<bool> _actionCondition;

		private Coroutine _coroutine;

		public CoroutineHolder(MonoBehaviour context, Func<bool> isWhile, Action action, YieldInstruction instruction, Func<bool> actionCondition = null)
		{
			_context = context;
			_isWhile = isWhile;
			_action = action;
			_instruction = instruction;

			_actionCondition = actionCondition ?? (() => true);
		}

		public void Reset()
		{
			Stop();
			Start();
		}

		public void Stop()
		{
			if (_coroutine != null)
			{
				_context.StopCoroutine(_coroutine);
			}
		}

		public void Start() =>
			_coroutine = _context.StartCoroutine(Coroutine());

		private IEnumerator Coroutine()
		{
			while (_isWhile())
			{
				if (_actionCondition())
				{
					_action();
				}

				yield return _instruction;
			}
		}
	}
}