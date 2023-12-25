using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArenaHero.Battle.Skills
{
    public class Vampire : MonoBehaviour
    {
        [SerializeField] private float _healthUp;
        [SerializeField] private Character character;

        public void AddHealth()
        {           
            character.AddHealth(_healthUp);
        }
    }
}

