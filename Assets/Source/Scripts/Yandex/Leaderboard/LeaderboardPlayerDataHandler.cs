using ArenaHero.Utils.Object;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ArenaHero.Yandex.Leaderboard
{
    public class LeaderboardPlayerDataHandler : MonoBehaviour, IPoolingObject<LeaderboardPlayerData>
    {
        [SerializeField] private TMP_Text _rank;
        [SerializeField] private Image _avatar;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _score;

        public Type SelfType => GetType();
        public GameObject SelfGameObject => gameObject;

        public event Action<IPoolingObject<LeaderboardPlayerData>> Disabled;

        private void OnDisable() =>
            Disabled?.Invoke(this);

        public void Init(LeaderboardPlayerData data)
        {
            _rank.text = data.Rank;
            _avatar.sprite = data.Avatar;
            _name.text = data.Name;
            _score.text = data.Score;
        }
    }
}