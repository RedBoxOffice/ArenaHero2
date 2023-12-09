using System.Collections;
using UnityEngine;

namespace ArenaHero.Battle.Skills
{
    public class CircleFireSkill : Skill
    {     
        [SerializeField] private GameObject _circle;
        [SerializeField] private float _delayTime;

        private Coroutine _coroutine;   

        private void OnEnable()
        {
            ChangeDynamicElement();
        }

        private void ChangeDynamicElement()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);          
          
            _coroutine = StartCoroutine(UpdateElement(_circle));
        }

        private IEnumerator UpdateElement(GameObject element)
        {
            Vector3 startScale = element.transform.localScale;
            Vector3 endScale = new Vector3(5f, 5f, 0f);                    
            float currentTime = 0;
            float targetTime = 0;

            while (targetTime <= _delayTime)
            {          
                targetTime = currentTime / _delayTime;              
                element.transform.localScale = Vector3.Lerp(startScale, endScale, targetTime);
                currentTime += Time.deltaTime;
                yield return null;
            }
        }              
    }
}

