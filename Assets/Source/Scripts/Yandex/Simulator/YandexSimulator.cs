using System;
using System.IO;

namespace ArenaHero.Yandex.Simulator
{
    public class YandexSimulator
    {
        private readonly string _saveSimPath = "Assets/Source/Scripts/Yandex/Simulator/SaveSim.json";

        public void Init(Action<string> action)
        {
            string data = File.ReadAllText(_saveSimPath);

            action?.Invoke(data);
        }

        public void Save(string save)
        {
            File.WriteAllText(_saveSimPath, save);
        }
    }
}