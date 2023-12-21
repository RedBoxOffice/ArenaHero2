using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ArenaHero.UI.Animations
{
    public class HeroDiedAnimation : MonoBehaviour
    {
        private const float UnVisible = 0;
        private const float Visible = 1;

        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Image _billboard;
        [SerializeField] private float _delayTime;
        [SerializeField] private float _waitTime;
       
        public void Play(Action endCallback) =>
            StartCoroutine(DiedAnimation(endCallback));
        
        private IEnumerator DiedAnimation(Action endCallback)
        {
            yield return StartCoroutine(FadeIn((alpha) =>
            {
                _billboard.color = ChangeAlpha(_billboard.color, alpha);
                _text.color = ChangeAlpha(_text.color, alpha);
            },
            UnVisible,
            Visible));

            yield return new WaitForSeconds(_waitTime);

            yield return StartCoroutine(FadeIn((alpha) =>
            {
                _billboard.color = ChangeAlpha(_billboard.color, alpha);
                _text.color = ChangeAlpha(_text.color, alpha);
            },
            Visible,
            UnVisible));
            
            endCallback?.Invoke();
        }

        private IEnumerator FadeIn(Action<float> changeColor, float current, float target)
        {
            float currentTime = 0;

            while (currentTime <= _delayTime)
            {               
                var normalTime = currentTime / _delayTime;
                var alpha = Mathf.Lerp(current, target, normalTime);
               
                changeColor(alpha);               
                currentTime += Time.deltaTime;
                yield return null;
            }
        } 
        
        private Color ChangeAlpha(Color currentColor, float targetAlpha)
        {
            currentColor.a = targetAlpha;
            return currentColor;
        }
    }
}

