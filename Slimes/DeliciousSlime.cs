using DeliciousSlimeSR2.Components;
using HarmonyLib;
using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DeliciousSlimeSR2.Slimes
{
    internal class DeliciousSlime
    {
        internal static SlimeDefinition deliciousSlime;

        public static void Initialize()
        {
            deliciousSlime = ScriptableObject.CreateInstance<SlimeDefinition>();
            deliciousSlime.name = "Delicious";
            deliciousSlime.hideFlags |= HideFlags.HideAndDontSave;
            deliciousSlime.color = new Color32(255, 255, 204, 255);
        }

        public static void Load(string sceneName)
        {
            switch (sceneName)
            {
                case "SystemCore":
                    {
                        break;
                    }
                case "GameCore":
                    {
                        deliciousSlime.localizedName = HarmonyPatches.LocalizationDirectorLoadTablePatch.AddTranslation("Actor", "l.delicious_slime", "Delicious Slime");

                        #region DELICIOUS_SLIME
                        deliciousSlime.prefab = Utility.PrefabUtils.CopyPrefab(Utility.Get<GameObject>("slimePink"));
                        deliciousSlime.prefab.name = "slimeDelicious";

                        deliciousSlime.prefab.AddComponent<DeliciousTransformation>();
                        deliciousSlime.prefab.GetComponent<Identifiable>().identType = deliciousSlime;
                        deliciousSlime.prefab.GetComponent<SlimeEat>().slimeDefinition = deliciousSlime;
                        deliciousSlime.prefab.GetComponent<PlayWithToys>().slimeDefinition = deliciousSlime;
                        deliciousSlime.prefab.GetComponent<ReactToToyNearby>().slimeDefinition = deliciousSlime;

                        deliciousSlime.Diet = UnityEngine.Object.Instantiate(Utility.Get<SlimeDefinition>("Pink")).Diet;
                        deliciousSlime.Diet.MajorFoodGroups = Array.Empty<SlimeEat.FoodGroup>();
                        deliciousSlime.Diet.MajorFoodIdentifiableTypeGroups = Array.Empty<IdentifiableTypeGroup>();
                        deliciousSlime.Diet.ProduceIdents = Array.Empty<IdentifiableType>();
                        deliciousSlime.Diet.AdditionalFoodIdents = Array.Empty<IdentifiableType>();
                        deliciousSlime.Diet.FavoriteIdents = Array.Empty<IdentifiableType>();
                        deliciousSlime.Diet.RefreshEatMap(SRSingleton<GameContext>.Instance.SlimeDefinitions, deliciousSlime);

                        deliciousSlime.icon = Utility.CreateSprite(Utility.LoadImage("Assets.iconSlimeDelicious"));
                        deliciousSlime.properties = UnityEngine.Object.Instantiate(Utility.Get<SlimeDefinition>("Pink").properties);
                        deliciousSlime.defaultPropertyValues = UnityEngine.Object.Instantiate(Utility.Get<SlimeDefinition>("Pink")).defaultPropertyValues;

                        SlimeAppearance slimeAppearance = UnityEngine.Object.Instantiate(Utility.Get<SlimeAppearance>("PinkDefault"));
                        SlimeAppearanceApplicator slimeAppearanceApplicator = deliciousSlime.prefab.GetComponent<SlimeAppearanceApplicator>();
                        slimeAppearance.name = "DeliciousDefault";
                        slimeAppearanceApplicator.Appearance = slimeAppearance;
                        slimeAppearanceApplicator.SlimeDefinition = deliciousSlime;

                        Material deliciousMaterial = UnityEngine.Object.Instantiate(slimeAppearance.Structures[0].DefaultMaterials[0]);
                        deliciousMaterial.hideFlags |= HideFlags.HideAndDontSave;
                        deliciousMaterial.SetColor("_TopColor", deliciousSlime.color);
                        deliciousMaterial.SetColor("_MiddleColor", deliciousSlime.color);
                        deliciousMaterial.SetColor("_BottomColor", new Color32(255, 255, 153, 255));
                        deliciousMaterial.SetColor("_SpecColor", new Color32(255, 255, 153, 255));
                        slimeAppearance.Structures[0].DefaultMaterials[0] = deliciousMaterial;

                        slimeAppearance.Face = UnityEngine.Object.Instantiate(Utility.Get<SlimeAppearance>("PinkDefault").Face);
                        slimeAppearance.Face.name = "DeliciousFace";

                        SlimeExpressionFace[] expressionFaces = new SlimeExpressionFace[0];
                        foreach (SlimeExpressionFace slimeExpressionFace in slimeAppearance.Face.ExpressionFaces)
                        {
                            Material slimeEyes = null;
                            Material slimeMouth = null;

                            if (slimeExpressionFace.Eyes)
                                slimeEyes = UnityEngine.Object.Instantiate(slimeExpressionFace.Eyes);
                            if (slimeExpressionFace.Mouth)
                                slimeMouth = UnityEngine.Object.Instantiate(slimeExpressionFace.Mouth);

                            if (slimeEyes)
                            {
                                slimeEyes.SetColor("_EyeRed", new Color32(255, 255, 204, 255));
                                slimeEyes.SetColor("_EyeGreen", new Color32(255, 255, 204, 255));
                                slimeEyes.SetColor("_EyeBlue", new Color32(255, 255, 204, 255));
                            }
                            if (slimeMouth)
                            {
                                slimeMouth.SetColor("_MouthBot", new Color32(255, 255, 153, 255));
                                slimeMouth.SetColor("_MouthMid", new Color32(255, 255, 153, 255));
                                slimeMouth.SetColor("_MouthTop", new Color32(255, 255, 153, 255));
                            }
                            slimeExpressionFace.Eyes = slimeEyes;
                            slimeExpressionFace.Mouth = slimeMouth;
                            expressionFaces = expressionFaces.AddToArray(slimeExpressionFace);
                        }
                        slimeAppearance.Face.ExpressionFaces = expressionFaces;
                        slimeAppearance.Face.OnEnable();

                        slimeAppearance.Icon = Utility.CreateSprite(Utility.LoadImage("Assets.iconSlimeDelicious"));
                        slimeAppearance.SplatColor = deliciousSlime.color;
                        slimeAppearance.ColorPalette = new SlimeAppearance.Palette
                        {
                            Ammo = deliciousSlime.color,
                            Top = deliciousSlime.color,
                            Middle = new Color32(255, 255, 153, 255),
                            Bottom = deliciousSlime.color
                        };
                        deliciousSlime.AppearancesDefault = new SlimeAppearance[] { slimeAppearance };
                        slimeAppearance.hideFlags |= HideFlags.HideAndDontSave;
                        #endregion
                        break;
                    }
                case "zoneCore":
                    {
                        SRSingleton<SceneContext>.Instance.SlimeAppearanceDirector.RegisterDependentAppearances(Utility.Get<SlimeDefinition>("Delicious"), Utility.Get<SlimeDefinition>("Delicious").AppearancesDefault[0]);
                        SRSingleton<SceneContext>.Instance.SlimeAppearanceDirector.UpdateChosenSlimeAppearance(Utility.Get<SlimeDefinition>("Delicious"), Utility.Get<SlimeDefinition>("Delicious").AppearancesDefault[0]);
                        SRSingleton<GameContext>.Instance.SlimeDefinitions.Slimes = SRSingleton<GameContext>.Instance.SlimeDefinitions.Slimes.AddItem(deliciousSlime).ToArray();
                        SRSingleton<GameContext>.Instance.SlimeDefinitions.slimeDefinitionsByIdentifiable.TryAdd(deliciousSlime, deliciousSlime);
                        break;
                    }
            }
        }
    }
}
