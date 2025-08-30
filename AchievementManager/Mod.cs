using Colossal.Logging;
using Colossal.PSI.Common;
using Colossal.Serialization.Entities;
using Game;
using Game.Modding;
using Game.SceneFlow;

namespace AchievementManager
{
    public class Mod : IMod
    {
        public static readonly ILog LOG = LogManager.GetLogger($"{nameof(AchievementManager)}.{nameof(Mod)}")
            .SetShowsErrorsInUI(false);


        public static Mod Instance { get; private set; }

        public void OnLoad(UpdateSystem updateSystem)
        {
            Instance = this;
            LOG.Info(nameof(OnLoad));

            if (GameManager.instance.modManager.TryGetExecutableAsset(this, out var asset))
                LOG.Info($"Current mod asset at {asset.path}");


            var myModSetting = new AchievementManagerSettings(Mod.Instance);
            myModSetting.RegisterInOptionsUI();
            GameManager.instance.localizationManager.AddSource("en-US", new LocaleEN(myModSetting));
            
            PlatformManager.instance.achievementsEnabled = true;
        }

        public void OnDispose()
        {
            LOG.Info(nameof(OnDispose));
        }
    }
}