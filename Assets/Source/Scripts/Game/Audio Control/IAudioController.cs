namespace ArenaHero.Game.AudioControl
{
    public interface IAudioController
    {
        public void Play(Audio audio);
        public float VolumePercent { get; set; }
    }
}