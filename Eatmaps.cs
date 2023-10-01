using DeliciousSlimeSR2.Slimes;
using HarmonyLib;
using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliciousSlimeSR2
{
    [HarmonyPatch(typeof(SlimeDiet), "RefreshEatMap")]
    class EatMapPatch
    {
        static void Postfix(SlimeDiet __instance, SlimeDefinitions definitions, SlimeDefinition definition)
        {
            if (definition != MoondewSlime.moondewSlime && definition != PomegraniteSlime.pomegraniteSlime && definition != DeliciousSlime.deliciousSlime)
            {
                __instance.EatMap.RemoveAll((Il2CppSystem.Predicate<SlimeDiet.EatMapEntry>)(x => x.eatsIdent == MoondewSlime.moondewSlime));
                __instance.EatMap.Add(new SlimeDiet.EatMapEntry()
                {
                    producesIdent = Utility.Get<IdentifiableType>("MoondewNectar"),
                    eatsIdent = MoondewSlime.moondewSlime,
                    minDrive = 3
                });

                __instance.EatMap.RemoveAll((Il2CppSystem.Predicate<SlimeDiet.EatMapEntry>)(x => x.eatsIdent == PomegraniteSlime.pomegraniteSlime));
                __instance.EatMap.Add(new SlimeDiet.EatMapEntry()
                {
                    producesIdent = Utility.Get<IdentifiableType>("PomegraniteFruit"),
                    eatsIdent = PomegraniteSlime.pomegraniteSlime,
                    minDrive = 3
                });

                __instance.EatMap.RemoveAll((Il2CppSystem.Predicate<SlimeDiet.EatMapEntry>)(x => x.eatsIdent == DeliciousSlime.deliciousSlime));
                __instance.EatMap.Add(new SlimeDiet.EatMapEntry()
                {
                    producesIdent = Utility.Get<IdentifiableType>("PomegraniteFruit"),
                    eatsIdent = DeliciousSlime.deliciousSlime,
                    minDrive = 3
                });
            }

            if (definition == DeliciousSlime.deliciousSlime)
            {
                __instance.EatMap.RemoveAll((Il2CppSystem.Predicate<SlimeDiet.EatMapEntry>)(x => x.eatsIdent == Utility.Get<IdentifiableType>("PomegraniteFruit")));
                __instance.EatMap.Add(new SlimeDiet.EatMapEntry()
                {
                    becomesIdent = PomegraniteSlime.pomegraniteSlime,
                    eatsIdent = Utility.Get<IdentifiableType>("PomegraniteFruit"),
                    minDrive = 1
                });

                __instance.EatMap.RemoveAll((Il2CppSystem.Predicate<SlimeDiet.EatMapEntry>)(x => x.eatsIdent == Utility.Get<IdentifiableType>("MoondewNectar")));
                __instance.EatMap.Add(new SlimeDiet.EatMapEntry()
                {
                    becomesIdent = MoondewSlime.moondewSlime,
                    eatsIdent = Utility.Get<IdentifiableType>("MoondewNectar"),
                    minDrive = 1
                });
            }
        }
    }
}