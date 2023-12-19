using ArenaHero.Yandex.SaveSystem;
using ArenaHero.Yandex.SaveSystem.Data;

namespace ArenaHero.UI
{
	public class NextStageButton : EventTriggerButton
	{
		public override void OnClick()
		{
			var currentLevelStage = GameDataSaver.Instance.Get<CurrentLevelStage>().Value;
			
			GameDataSaver.Instance.Set(new CurrentLevelStage(currentLevelStage + 1));
			
			base.OnClick();
		}
	}
}