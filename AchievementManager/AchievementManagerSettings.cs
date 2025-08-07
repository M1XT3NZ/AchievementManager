using System.Collections.Generic;
using System.Linq;
using Colossal;
using Colossal.IO.AssetDatabase;
using Colossal.PSI.Common;
using Game.Modding;
using Game.Settings;
using Game.UI.Widgets;

namespace AchievementManager
{
    [FileLocation("ModsSettings/AchievementManager/")]
    public class AchievementManagerSettings : ModSetting
    {
        public AchievementManagerSettings(IMod mod) : base(mod)
        {
            SetDefaults();
        }

        [SettingsUIDropdown(typeof(AchievementManagerSettings), nameof(GetAchievementDropdownItems))]
        public string SelectedAchievementKey { get; set; } = "First";

        public IAchievement SelectedAchievement =>
            PlatformManager.instance.EnumerateAchievements()
                .FirstOrDefault(a => a.internalName == SelectedAchievementKey);

        [SettingsUIButtonGroup("Achievement Actions")]
        [SettingsUIButton]
        public bool UnlockAchievement
        {
            set => PlatformManager.instance.UnlockAchievement(SelectedAchievement.id);
        }

        [SettingsUIButtonGroup("Achievement Actions")]
        [SettingsUIButton]
        public bool ResetAchievements
        {
            set => PlatformManager.instance.ResetAchievements();
        }


        public override void SetDefaults()
        {
            //dropdownItemsVersion = 0;
        }

        public DropdownItem<string>[] GetAchievementDropdownItems()
        {
            return PlatformManager.instance.EnumerateAchievements()
                .Select(a => new DropdownItem<string>
                {
                    value = a.internalName,
                    displayName = a.internalName // or use localization if needed
                })
                .ToArray();
        }
    }

    public class LocaleEN : IDictionarySource
    {
        private readonly AchievementManagerSettings m_Setting;

        public LocaleEN(AchievementManagerSettings setting)
        {
            m_Setting = setting;
        }

        public IEnumerable<KeyValuePair<string, string>> ReadEntries(IList<IDictionaryEntryError> errors,
            Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                { m_Setting.GetSettingsLocaleID(), "AchievementManager" },
                {
                    m_Setting.GetOptionLabelLocaleID(nameof(AchievementManagerSettings.SelectedAchievementKey)),
                    "Selected Dropdown"
                },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(AchievementManagerSettings.SelectedAchievementKey)),
                    "Select an achievement to unlock or reset."
                },
                {
                    m_Setting.GetOptionLabelLocaleID(nameof(AchievementManagerSettings.UnlockAchievement)),
                    "Unlock Achievement"
                },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(AchievementManagerSettings.UnlockAchievement)),
                    " Unlock the selected achievement."
                },
                {
                    m_Setting.GetOptionLabelLocaleID(nameof(AchievementManagerSettings.ResetAchievements)),
                    "Reset Achievements"
                },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(AchievementManagerSettings.ResetAchievements)),
                    "Reset all achievements."
                }
            };
        }

        public void Unload()
        {
        }
    }
}