using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace ArenaHero.Battle.Skills
{
    public class ShockWave : Skill
    {
        [SerializeField] private float _delayTime;
        [SerializeField] private float _damageHero;

        public void Attack(List<Character> characters)
        {
            foreach (var character in characters)
            {
                StartCoroutine(AddForce(character));
                character.TakeDamage(_damageHero);
            }           
        }

        private IEnumerator AddForce(Character character)
        {
            float time = 0;
            Vector3 vectorBetweenObject = character.transform.position - transform.position;
            vectorBetweenObject.y = 0;
            Vector3 startPosition = character.transform.position;
            Vector3 endPosition = character.transform.position + vectorBetweenObject;

            while (time <= _delayTime)
            {
                character.transform.position = Vector3.Lerp(startPosition, endPosition, time / _delayTime);
                time += Time.deltaTime;
                yield return null;
            }
        }
    }
}
