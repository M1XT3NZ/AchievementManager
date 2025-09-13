using System;
using System.Collections.Generic;
using System.Linq;

using Colossal;
using Colossal.IO.AssetDatabase;
using Colossal.PSI.Common;

using Game.Achievements;
using Game.Modding;
using Game.Settings;
using Game.UI.Widgets;

using UnityEngine;

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
        public string SelectedAchievementKey { get; set; } = "MyFirstCity";

        private IAchievement SelectedAchievement =>
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

        [SettingsUIButton]
        public bool UnlockAllAchievements
        {
            set
            {
                try
                {
                    foreach (var achievement in typeof(Achievements).GetFields())
                    {
                        Debug.LogWarning($"Unlocking {achievement.Name}");
                        // Get the achievement attribute
                        var attribute = achievement.GetCustomAttributes(typeof(AchievementAttribute), false)
                            .FirstOrDefault() as AchievementAttribute;
                        if (attribute == null)
                        {
                            Debug.LogWarning($"Field {achievement.Name} does not have an AchievementAttribute.");
                            continue;
                        }
                        // Check if the achievement is incremental
                        AchievementId achievementId = new AchievementId(attribute.id);
                        if (attribute.unlocksAt > 0)
                        {
                            // Unlock the achievement and set its progress to the maximum
                            PlatformManager.instance.UnlockAchievement(achievementId);
                            PlatformManager.instance.IndicateAchievementProgress(achievementId, attribute.unlocksAt, IndicateType.Absolute);
                            Debug.Log($"Unlocked achievement {attribute.internalName} with max progress {attribute.unlocksAt}.");
                        }
                        else
                        {
                            // Unlock the achievement without progress
                            PlatformManager.instance.UnlockAchievement(achievementId);
                            Debug.Log($"Unlocked achievement {attribute.internalName} without progress.");
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        // [SettingsUIHideByCondition(typeof(AchievementManagerSettings), nameof(_isprogress))]
        // [SettingsUISlider(min = 0, max = 24000000, step = 1)]
        // public int AchievementProgress { get; set; }
        //
        // [SettingsUIButton]
        // public bool SetAchievementProgress
        // {
        //     set
        //     {
        //         if (SelectedAchievement != null && _isprogress)
        //         {
        //             if (SelectedAchievement.maxProgress != AchievementProgress)
        //             {
        //                 // Ensure the progress is within the defined min and max range
        //                 AchievementProgress = Mathf.Clamp(AchievementProgress, 0, SelectedAchievement.maxProgress);
        //                 // Set the progress for the selected achievement
        //                 PlatformManager.instance.IndicateAchievementProgress(SelectedAchievement.id, AchievementProgress);
        //             }
        //             else
        //             {
        //                 Debug.LogWarning($"Achievement {SelectedAchievement.internalName} already has the maximum progress of {SelectedAchievement.maxProgress}.");
        //             }
        //             // Ensure the progress is within the defined min and max range
        //             if(SelectedAchievement.isIncremental)
        //                 PlatformManager.instance.IndicateAchievementProgress(SelectedAchievement.id,AchievementProgress, IndicateType.Absolute);
        //
        //
        //         }
        //     }
        // }


        //We want to be able to edit the dropdown items individually so that means
        //if there is an Achievement like "Making a Mark" which is 0 out of 5
        //we can set it to any value we want out of the 5
        private void Test()
        {
            foreach (var field in typeof(Achievements).GetFields())

                switch (field.Name)
                {
                    case "MyFirstCity":
                        break;
                    case "TheInspector":
                        break;
                    case "HappytobeofService":
                        break;
                    case "RoyalFlush":
                        break;
                    case "KeyToTheCity":
                        break;
                    case "SixFigures":
                        break;
                    case "GoAnywhere":
                        break;
                    case "TheSizeOfGolfBalls":
                        break;
                    case "OutforaSpin":
                        break;
                    case "NowTheyreAllAshTrees":
                        break;
                    case "ZeroEmission":
                        break;
                    case "UpAndAway":
                        break;
                    case "MakingAMark":
                        break;
                    case "EverythingTheLightTouches":
                        break;
                    case "CallingtheShots":
                        break;
                    case "WideVariety":
                        break;
                    case "ExecutiveDecision":
                        break;
                    case "AllSmiles":
                        break;
                    case "YouLittleStalker":
                        break;
                    case "IMadeThis":
                        break;
                    case "Cartography":
                        break;
                    case "TheExplorer":

                        break;
                    case "TheLastMileMarker":
                        break;
                    case "FourSeasons":
                        break;
                    case "Spiderwebbing":
                        break;
                    case "Snapshot":
                        break;
                    case "ThisIsNotMyHappyPlace":
                        break;
                    case "TheArchitect":
                        break;
                    case "SimplyIrresistible":
                        break;
                    case "TopoftheClass":
                        break;
                    case "TheDeepEnd":
                        break;
                    case "Groundskeeper":
                        break;
                    case "ColossalGardener":
                        break;
                    case "StrengthThroughDiversity":
                        break;
                    case "SquasherDowner":
                        break;
                    case "ALittleBitofTLC":
                        break;
                    case "WelcomeOneandAll":
                        break;
                    case "OneofEverything":
                        break;
                    case "HowMuchIsTheFish":
                        break;
                    case "ShipIt":
                        break;
                    case "ADifferentPlatformer":
                        break;
                    case "DrawMeLikeOneOfYourLiftBridges":
                        break;
                    case "ItsPronouncedKey":
                        break;
                    case "Pierfect":
                        break;
                }
        }


        public override void SetDefaults()
        {
            //dropdownItemsVersion = 0;
        }

        public DropdownItem<string>[] GetAchievementDropdownItems()
        {
            // Sort achievements alphabetically by internalName, user-friendly list.
            // (case-insensitive ordering so "alpha" and "Alpha" group together)
            var items = PlatformManager.instance?.EnumerateAchievements()?
                .OrderBy(a => a.internalName, StringComparer.OrdinalIgnoreCase)
                .Select(a => new DropdownItem<string>
                {
                    value = a.internalName,
                    displayName = a.internalName // or use localization if needed
                })
                .ToArray();

            return items ?? Array.Empty<DropdownItem<string>>();    // null-safe path
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
                { m_Setting.GetSettingsLocaleID(), "Achievement Manager" },     // space added so name shows nicely in Options list
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
                },
                {
                    m_Setting.GetOptionLabelLocaleID(nameof(AchievementManagerSettings.UnlockAllAchievements)),
                    "Unlock All Achievements"
                },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(AchievementManagerSettings.UnlockAllAchievements)),
                    "Unlock all achievements, setting progress to maximum for incremental achievements."
                },
                // {
                //     m_Setting.GetOptionLabelLocaleID(nameof(AchievementManagerSettings.AchievementProgress)),
                //     "Achievement Progress"
                // },
                // {
                //     m_Setting.GetOptionDescLocaleID(nameof(AchievementManagerSettings.AchievementProgress)),
                //     "Set the progress for the selected achievement (0-5)."
                // },
                // {
                //     m_Setting.GetOptionLabelLocaleID(nameof(AchievementManagerSettings.SetAchievementProgress)),
                //     "Set Progress"
                // },
                // {
                //     m_Setting.GetOptionDescLocaleID(nameof(AchievementManagerSettings.SetAchievementProgress)),
                //     "Set the progress for the selected achievement."
                // }
            };
        }

        public void Unload()
        {
        }
    }
}