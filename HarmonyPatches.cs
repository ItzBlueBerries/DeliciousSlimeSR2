using Il2Cpp;
using Il2CppMonomiPark.SlimeRancher.DataModel;
using Il2CppMonomiPark.SlimeRancher.Script.Util;
using Il2CppMonomiPark.SlimeRancher.UI.Localization;
using Il2CppMonomiPark.SlimeRancher.UI;
using Il2CppMonomiPark.SlimeRancher;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;
using System.Collections;
using HarmonyLib;
using DeliciousSlimeSR2.Slimes;

namespace DeliciousSlimeSR2
{
    internal class HarmonyPatches
    {
        [HarmonyPatch(typeof(AutoSaveDirector), "Awake")]
        public static class PatchAutoSaveDirectorAwake
        {
            public static void Prefix(AutoSaveDirector __instance)
            {
                Utility.Get<IdentifiableTypeGroup>("BaseSlimeGroup").memberTypes.Add(MoondewSlime.moondewSlime);
                Utility.Get<IdentifiableTypeGroup>("VaccableBaseSlimeGroup").memberTypes.Add(MoondewSlime.moondewSlime);
                Utility.Get<IdentifiableTypeGroup>("SlimesGroup").memberTypes.Add(MoondewSlime.moondewSlime);
                Utility.Get<IdentifiableTypeGroup>("NonSlimesGroup").memberTypes.Add(MoondewSlime.moondewSlime);

                Utility.Get<IdentifiableTypeGroup>("BaseSlimeGroup").memberTypes.Add(PomegraniteSlime.pomegraniteSlime);
                Utility.Get<IdentifiableTypeGroup>("VaccableBaseSlimeGroup").memberTypes.Add(PomegraniteSlime.pomegraniteSlime);
                Utility.Get<IdentifiableTypeGroup>("SlimesGroup").memberTypes.Add(PomegraniteSlime.pomegraniteSlime);
                Utility.Get<IdentifiableTypeGroup>("NonSlimesGroup").memberTypes.Add(PomegraniteSlime.pomegraniteSlime);

                Utility.Get<IdentifiableTypeGroup>("BaseSlimeGroup").memberTypes.Add(DeliciousSlime.deliciousSlime);
                Utility.Get<IdentifiableTypeGroup>("VaccableBaseSlimeGroup").memberTypes.Add(DeliciousSlime.deliciousSlime);
                Utility.Get<IdentifiableTypeGroup>("SlimesGroup").memberTypes.Add(DeliciousSlime.deliciousSlime);
                Utility.Get<IdentifiableTypeGroup>("NonSlimesGroup").memberTypes.Add(DeliciousSlime.deliciousSlime);

                __instance.identifiableTypes.memberTypes.Add(MoondewSlime.moondewSlime);
                __instance.identifiableTypes.memberTypes.Add(PomegraniteSlime.pomegraniteSlime);
                __instance.identifiableTypes.memberTypes.Add(DeliciousSlime.deliciousSlime);
            }
        }

        /*[HarmonyPatch(typeof(SavedGame))]
        internal static class SavedGamePushPatch
        {
            [HarmonyPrefix]
            [HarmonyPatch(nameof(SavedGame.Push), typeof(GameModel))]
            public static void PushGameModel(SavedGame __instance)
            {
                foreach (PediaEntry pediaEntry in Utility.Pedia.addedPedias)
                {
                    if (!__instance.pediaEntryLookup.ContainsKey(pediaEntry.GetPersistenceId()))
                        __instance.pediaEntryLookup.Add(pediaEntry.GetPersistenceId(), pediaEntry);
                }
            }
        }

        [HarmonyPatch(typeof(PediaDirector), "Awake")]
        internal static class PatchPediaDirectorAwake
        {
            public static void Prefix(PediaDirector __instance)
            {
                #region PEDIAS
                Utility.Pedia.AddSlimepedia(Utility.Get<IdentifiableType>("MoondewS"), "MoondewS",
                    "Slimes love it! Moondew-tastic.",
                    "When fed to any slime, they produce moondew nectar! That's about it really, you figure out what you wanna do with this new power.",
                    "(none)",
                    "(none)"
                );
                Utility.Pedia.AddSlimepedia(Utility.Get<IdentifiableType>("PomegraniteS"), "PomegraniteS",
                    "Slimes love it! Pomegranite-tastic.",
                    "When fed to any slime, they produce pomegranite! That's about it really, you figure out what you wanna do with this new power.",
                    "(none)",
                    "(none)"
                );
                Utility.Pedia.AddSlimepedia(Utility.Get<IdentifiableType>("Delicious"), "Delicious",
                    "Slimes love it! Moongranite-tastic.",
                    "When fed to any slime, they produce moondew nectar or pomegranite! That's about it really, you figure out what you wanna do with this new power.",
                    "(none)",
                    "(none)"
                );
                #endregion

                foreach (var pediaEntry in Utility.Pedia.addedPedias)
                {
                    var identPediaEntry = pediaEntry.TryCast<IdentifiablePediaEntry>();
                    if (identPediaEntry && !__instance.identDict.ContainsKey(identPediaEntry.identifiableType))
                        __instance.identDict.Add(identPediaEntry.identifiableType, pediaEntry);
                }
            }
        }*/

        [HarmonyPatch(typeof(LocalizationDirector), "LoadTables")]
        internal static class LocalizationDirectorLoadTablePatch
        {
            public static void Postfix(LocalizationDirector __instance)
            {
                MelonCoroutines.Start(LoadTable(__instance));
            }

            private static IEnumerator LoadTable(LocalizationDirector director)
            {
                WaitForSecondsRealtime waitForSecondsRealtime = new WaitForSecondsRealtime(0.01f);
                yield return waitForSecondsRealtime;
                foreach (Il2CppSystem.Collections.Generic.KeyValuePair<string, StringTable> keyValuePair in director.Tables)
                {
                    if (addedTranslations.TryGetValue(keyValuePair.Key, out var dictionary))
                    {
                        foreach (System.Collections.Generic.KeyValuePair<string, string> keyValuePair2 in dictionary)
                        {
                            keyValuePair.Value.AddEntry(keyValuePair2.Key, keyValuePair2.Value);
                        }
                    }
                }
                yield break;
            }

            public static LocalizedString AddTranslation(string table, string key, string localized)
            {
                System.Collections.Generic.Dictionary<string, string> dictionary;
                if (!addedTranslations.TryGetValue(table, out dictionary))
                {
                    dictionary = new System.Collections.Generic.Dictionary<string, string>(); ;
                    addedTranslations.Add(table, dictionary);
                }
                dictionary.TryAdd(key, localized);
                StringTable table2 = LocalizationUtil.GetTable(table);
                StringTableEntry stringTableEntry = table2.AddEntry(key, localized);
                return new LocalizedString(table2.SharedData.TableCollectionName, stringTableEntry.SharedEntry.Id);
            }

            public static System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, string>> addedTranslations = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, string>>();
        }
    }

    internal class OtherHarmonyPatches
    {
        [HarmonyPatch(typeof(SlimeEat), "EatAndProduce")]
        internal class Patch_SlimeEatProduce
        {
            private static bool Prefix(SlimeEat __instance, SlimeDiet.EatMapEntry em)
            {
                List<IdentifiableType> list = new List<IdentifiableType>()
                {
                    Utility.Get<IdentifiableType>("MoondewNectar"),
                    Utility.Get<IdentifiableType>("PomegraniteFruit")
                };

                if (em.eatsIdent == DeliciousSlime.deliciousSlime)
                    em.producesIdent = list.RandomObject();

                if (__instance.slimeDefinition == DeliciousSlime.deliciousSlime)
                {
                    if (em.eatsIdent == Utility.Get<IdentifiableType>("PomegraniteFruit"))
                    {
                        em.becomesIdent = PomegraniteSlime.pomegraniteSlime;
                        MelonLogger.Msg("This happened (pomegranite)");
                    }
                    if (em.eatsIdent == Utility.Get<IdentifiableType>("MoondewNectar"))
                    {
                        em.becomesIdent = MoondewSlime.moondewSlime;
                        MelonLogger.Msg("This happened (moondew)");
                    }
                }

                return true;
            }
        }
    }
}