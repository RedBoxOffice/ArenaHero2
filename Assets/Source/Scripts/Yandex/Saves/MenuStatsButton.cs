using ArenaHero.Yandex.Saves.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

namespace ArenaHero.UI
{
    public class MenuStatsButton : MonoBehaviour
    {
        protected int _indexLevel;
        public event UnityAction<TextMeshProUGUI> OnClick; 

        public void OnClickUpLevel(TextMeshProUGUI textMeshProUGUI)
        {
            OnClick?.Invoke(textMeshProUGUI);
        } 
    } 
}

