using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ArenaHero.UI
{
    public class DarkSoulsAnimation : MonoBehaviour
    {
        private const float Unvisible = 0;
        private const float Visible = 1;

        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Image _billboard;
        [SerializeField] private float _delayTime;
        [SerializeField] private float _waitTime;
        [SerializeField] private GameObject _window;
       
        private void OnEnable() =>
            StartCoroutine(DiedAnimation());

        private IEnumerator FadeIn(Action<float> changeColor, float current, float target)
        {
            float currentTime = 0;
            float normalizeTime;

            while (currentTime <= _delayTime)
            {               
                normalizeTime = currentTime / _delayTime;
                var alpha = Mathf.Lerp(current, target, normalizeTime);
               
                changeColor(alpha);               
                currentTime += Time.deltaTime;
                yield return null;
            }
        } 
        
        private IEnumerator DiedAnimation()
        {
            yield return StartCoroutine(FadeIn((alpha) =>
            {
                _billboard.color = ChangeAlpha(_billboard.color, alpha);
                _text.color = ChangeAlpha(_text.color, alpha);
            },
            Unvisible,
            Visible));

            yield return new WaitForSeconds(_waitTime);

            yield return StartCoroutine(FadeIn((alpha) =>
            {
                _billboard.color = ChangeAlpha(_billboard.color, alpha);
                _text.color = ChangeAlpha(_text.color, alpha);
            },
            Visible,
            Unvisible));

            _window.SetActive(true);
        }

        private Color ChangeAlpha(Color currentColor, float targetAlpha)
        {
            currentColor.a = targetAlpha;
            return currentColor;
        }
    }
}

