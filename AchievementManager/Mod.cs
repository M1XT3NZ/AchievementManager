using Colossal.Logging;
using Colossal.PSI.Common;

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

            if (GameManager.instance?.modManager?.TryGetExecutableAsset(this, out var asset) == true)
                LOG.Info($"Current mod asset at {asset.path}");


            var myModSetting = new AchievementManagerSettings(Mod.Instance);
            myModSetting.RegisterInOptionsUI();
            GameManager.instance?.localizationManager?.AddSource("en-US", new LocaleEN(myModSetting));

            // must use explicit null check for assignment
            if (PlatformManager.instance != null)
                PlatformManager.instance.achievementsEnabled = true;

        }

        public void OnDispose()
        {
            LOG.Info(nameof(OnDispose));
        }
    }
}
