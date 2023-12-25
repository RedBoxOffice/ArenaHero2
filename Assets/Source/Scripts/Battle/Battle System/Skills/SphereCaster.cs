using ArenaHero.Battle.Enemies.BehaviorTree;
using ArenaHero.InputSystem;
using Reflex.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ArenaHero.Battle.Skills
{
    public class SphereCaster : Skill
    {
        [SerializeField] private float _maxDistance;
        [SerializeField] private float _radius; 
        [SerializeField] private ShockWave _shockWave;
        [SerializeField] private Vampire _vampire;
       
        public readonly List<Character> _characters = new List<Character>();
        private Vector3 _direction = Vector3.up;        
        private RaycastHit[] _enemies;
        private IActionsInputHandlerOnlyPlayer _inputHandler;
        [Inject]
        private void inject(IActionsInputHandlerOnlyPlayer inputHandler)
        {
            _inputHandler = inputHandler;
            _inputHandler.Skill += FindBot;
        }

        private void FindBot()
        {
            _characters.Clear();
            _enemies = null;

            _enemies = Physics.SphereCastAll(transform.position, _radius, _direction, _maxDistance);

            if (_enemies != null)
            {
                foreach (var bot in _enemies)
                {
                    if (bot.transform.gameObject.TryGetComponent(out Bot enemy))
                    {
                        Character character = enemy.gameObject.GetComponent<Character>();
                        _characters.Add(character);
                    }
                }

                int randomSkill = Random.Range(1, 3);

                switch (randomSkill)
                {
                    case 1:
                        _shockWave.Attack(_characters);
                        Debug.Log("ShockWave");
                        break;
                    case 2:
                        _vampire.AddHealth();
                        Debug.Log("Vampire");
                        break;
                }  
            }
        }       
    }
}
