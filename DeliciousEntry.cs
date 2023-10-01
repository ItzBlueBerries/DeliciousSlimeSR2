using DeliciousSlimeSR2.Components;
using DeliciousSlimeSR2.Slimes;
using HarmonyLib;
using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using MelonLoader;

[assembly: MelonInfo(typeof(DeliciousSlimeSR2.DeliciousEntry), "Delicious Slimes", "1.0", "FruitsyOG")]
[assembly: MelonGame("MonomiPark", "SlimeRancher2")]
namespace DeliciousSlimeSR2
{
    public class DeliciousEntry : MelonMod
    {
        public override void OnInitializeMelon()
        {
            ClassInjector.RegisterTypeInIl2Cpp<DeliciousTransformation>();
            MoondewSlime.Initialize();
            PomegraniteSlime.Initialize();
            DeliciousSlime.Initialize();
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            MoondewSlime.Load(sceneName);
            PomegraniteSlime.Load(sceneName);
            DeliciousSlime.Load(sceneName);
            OnSceneAddSpawners(sceneName);
        }

        public static void OnSceneAddSpawners(string sceneName)
        {
            switch (sceneName.Contains("zoneStrand"))
            {
                case true:
                    {
                        IEnumerable<DirectedSlimeSpawner> source = UnityEngine.Object.FindObjectsOfType<DirectedSlimeSpawner>();
                        foreach (DirectedSlimeSpawner directedSlimeSpawner in source)
                        {
                            foreach (DirectedActorSpawner.SpawnConstraint spawnConstraint in directedSlimeSpawner.constraints)
                            {
                                SlimeSet.Member[] members =
                                {
                                    new SlimeSet.Member
                                    {
                                        prefab = MoondewSlime.moondewSlime.prefab,
                                        identType = MoondewSlime.moondewSlime,
                                        weight = 0.008f
                                    },
                                    new SlimeSet.Member
                                    {
                                        prefab = PomegraniteSlime.pomegraniteSlime.prefab,
                                        identType = PomegraniteSlime.pomegraniteSlime,
                                        weight = 0.008f
                                    },
                                    new SlimeSet.Member
                                    {
                                        prefab = DeliciousSlime.deliciousSlime.prefab,
                                        identType = DeliciousSlime.deliciousSlime,
                                        weight = 0.004f
                                    }
                                };

                                spawnConstraint.slimeset.members = spawnConstraint.slimeset.members.Concat(members).ToArray();
                            }
                        }
                        break;
                    }
            }

            switch (sceneName.Contains("zoneGorge"))
            {
                case true:
                    {
                        IEnumerable<DirectedSlimeSpawner> source = UnityEngine.Object.FindObjectsOfType<DirectedSlimeSpawner>();
                        foreach (DirectedSlimeSpawner directedSlimeSpawner in source)
                        {
                            foreach (DirectedActorSpawner.SpawnConstraint spawnConstraint in directedSlimeSpawner.constraints)
                            {
                                SlimeSet.Member[] members =
                                {
                                    new SlimeSet.Member
                                    {
                                        prefab = PomegraniteSlime.pomegraniteSlime.prefab,
                                        identType = PomegraniteSlime.pomegraniteSlime,
                                        weight = 0.008f
                                    },
                                    new SlimeSet.Member
                                    {
                                        prefab = DeliciousSlime.deliciousSlime.prefab,
                                        identType = DeliciousSlime.deliciousSlime,
                                        weight = 0.004f
                                    }
                                };

                                spawnConstraint.slimeset.members = spawnConstraint.slimeset.members.Concat(members).ToArray();
                            }
                        }
                        break;
                    }
            }

            switch (sceneName.Contains("zoneFields"))
            {
                case true:
                    {
                        IEnumerable<DirectedSlimeSpawner> source = UnityEngine.Object.FindObjectsOfType<DirectedSlimeSpawner>();
                        foreach (DirectedSlimeSpawner directedSlimeSpawner in source)
                        {
                            foreach (DirectedActorSpawner.SpawnConstraint spawnConstraint in directedSlimeSpawner.constraints)
                            {
                                SlimeSet.Member[] members =
                                {
                                    new SlimeSet.Member
                                    {
                                        prefab = DeliciousSlime.deliciousSlime.prefab,
                                        identType = DeliciousSlime.deliciousSlime,
                                        weight = 0.004f
                                    }
                                };

                                spawnConstraint.slimeset.members = spawnConstraint.slimeset.members.Concat(members).ToArray();
                            }
                        }
                        break;
                    }
            }

            switch (sceneName.Contains("zoneBluffs"))
            {
                case true:
                    {
                        IEnumerable<DirectedSlimeSpawner> source = UnityEngine.Object.FindObjectsOfType<DirectedSlimeSpawner>();
                        foreach (DirectedSlimeSpawner directedSlimeSpawner in source)
                        {
                            foreach (DirectedActorSpawner.SpawnConstraint spawnConstraint in directedSlimeSpawner.constraints)
                            {
                                SlimeSet.Member[] members =
                                {
                                    new SlimeSet.Member
                                    {
                                        prefab = DeliciousSlime.deliciousSlime.prefab,
                                        identType = DeliciousSlime.deliciousSlime,
                                        weight = 0.004f
                                    }
                                };

                                spawnConstraint.slimeset.members = spawnConstraint.slimeset.members.Concat(members).ToArray();
                            }
                        }
                        break;
                    }
            }
        }
    }
}