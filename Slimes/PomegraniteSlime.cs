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
    internal class PomegraniteSlime
    {
        internal static SlimeDefinition pomegraniteSlime;

        public static void Initialize()
        {
            pomegraniteSlime = ScriptableObject.CreateInstance<SlimeDefinition>();
            pomegraniteSlime.name = "PomegraniteS";
            pomegraniteSlime.hideFlags |= HideFlags.HideAndDontSave;
            pomegraniteSlime.color = new Color32(95, 61, 148, 255);
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
                        pomegraniteSlime.localizedName = HarmonyPatches.LocalizationDirectorLoadTablePatch.AddTranslation("Actor", "l.pomegranite_slime", "Pomegranite Slime");

                        #region POMEGRANITE_SLIME
                        pomegraniteSlime.prefab = Utility.PrefabUtils.CopyPrefab(Utility.Get<GameObject>("slimePink"));
                        pomegraniteSlime.prefab.name = "slimePomegranite";

                        pomegraniteSlime.prefab.GetComponent<Identifiable>().identType = pomegraniteSlime;
                        pomegraniteSlime.prefab.GetComponent<SlimeEat>().slimeDefinition = pomegraniteSlime;
                        pomegraniteSlime.prefab.GetComponent<PlayWithToys>().slimeDefinition = pomegraniteSlime;
                        pomegraniteSlime.prefab.GetComponent<ReactToToyNearby>().slimeDefinition = pomegraniteSlime;

                        pomegraniteSlime.Diet = UnityEngine.Object.Instantiate(Utility.Get<SlimeDefinition>("Pink")).Diet;
                        pomegraniteSlime.Diet.MajorFoodGroups = Array.Empty<SlimeEat.FoodGroup>();
                        pomegraniteSlime.Diet.MajorFoodIdentifiableTypeGroups = Array.Empty<IdentifiableTypeGroup>();
                        pomegraniteSlime.Diet.ProduceIdents = Array.Empty<IdentifiableType>();
                        pomegraniteSlime.Diet.AdditionalFoodIdents = Array.Empty<IdentifiableType>();
                        pomegraniteSlime.Diet.FavoriteIdents = Array.Empty<IdentifiableType>();
                        pomegraniteSlime.Diet.RefreshEatMap(SRSingleton<GameContext>.Instance.SlimeDefinitions, pomegraniteSlime);

                        pomegraniteSlime.icon = Utility.CreateSprite(Utility.LoadImage("Assets.iconSlimePomegranite"));
                        pomegraniteSlime.properties = UnityEngine.Object.Instantiate(Utility.Get<SlimeDefinition>("Pink").properties);
                        pomegraniteSlime.defaultPropertyValues = UnityEngine.Object.Instantiate(Utility.Get<SlimeDefinition>("Pink")).defaultPropertyValues;

                        SlimeAppearance slimeAppearance = UnityEngine.Object.Instantiate(Utility.Get<SlimeAppearance>("PinkDefault"));
                        SlimeAppearanceApplicator slimeAppearanceApplicator = pomegraniteSlime.prefab.GetComponent<SlimeAppearanceApplicator>();
                        slimeAppearance.name = "PomegraniteDefault";
                        slimeAppearanceApplicator.Appearance = slimeAppearance;
                        slimeAppearanceApplicator.SlimeDefinition = pomegraniteSlime;

                        Material pomegraniteMaterial = UnityEngine.Object.Instantiate(slimeAppearance.Structures[0].DefaultMaterials[0]);
                        pomegraniteMaterial.hideFlags |= HideFlags.HideAndDontSave;
                        pomegraniteMaterial.SetColor("_TopColor", pomegraniteSlime.color);
                        pomegraniteMaterial.SetColor("_MiddleColor", pomegraniteSlime.color);
                        pomegraniteMaterial.SetColor("_BottomColor", new Color32(123, 89, 173, 255));
                        pomegraniteMaterial.SetColor("_SpecColor", new Color32(123, 89, 173, 255));
                        slimeAppearance.Structures[0].DefaultMaterials[0] = pomegraniteMaterial;

                        slimeAppearance.Face = UnityEngine.Object.Instantiate(Utility.Get<SlimeAppearance>("BattyDefault").Face);
                        slimeAppearance.Face.name = "PomegraniteFace";

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
                                slimeEyes.SetColor("_EyeRed", new Color32(123, 89, 173, 255));
                                slimeEyes.SetColor("_EyeGreen", new Color32(123, 89, 173, 255));
                                slimeEyes.SetColor("_EyeBlue", new Color32(123, 89, 173, 255));
                            }
                            if (slimeMouth)
                            {
                                slimeMouth.SetColor("_MouthBot", new Color32(95, 61, 148, 255));
                                slimeMouth.SetColor("_MouthMid", new Color32(95, 61, 148, 255));
                                slimeMouth.SetColor("_MouthTop", new Color32(95, 61, 148, 255));
                            }
                            slimeExpressionFace.Eyes = slimeEyes;
                            slimeExpressionFace.Mouth = slimeMouth;
                            expressionFaces = expressionFaces.AddToArray(slimeExpressionFace);
                        }
                        slimeAppearance.Face.ExpressionFaces = expressionFaces;
                        slimeAppearance.Face.OnEnable();

                        slimeAppearance.Icon = Utility.CreateSprite(Utility.LoadImage("Assets.iconSlimePomegranite"));
                        slimeAppearance.SplatColor = pomegraniteSlime.color;
                        slimeAppearance.ColorPalette = new SlimeAppearance.Palette
                        {
                            Ammo = pomegraniteSlime.color,
                            Top = pomegraniteSlime.color,
                            Middle = new Color32(123, 89, 173, 255),
                            Bottom = pomegraniteSlime.color
                        };
                        pomegraniteSlime.AppearancesDefault = new SlimeAppearance[] { slimeAppearance };
                        slimeAppearance.hideFlags |= HideFlags.HideAndDontSave;
                        #endregion
                        break;
                    }
                case "zoneCore":
                    {
                        SRSingleton<SceneContext>.Instance.SlimeAppearanceDirector.RegisterDependentAppearances(Utility.Get<SlimeDefinition>("PomegraniteS"), Utility.Get<SlimeDefinition>("PomegraniteS").AppearancesDefault[0]);
                        SRSingleton<SceneContext>.Instance.SlimeAppearanceDirector.UpdateChosenSlimeAppearance(Utility.Get<SlimeDefinition>("PomegraniteS"), Utility.Get<SlimeDefinition>("PomegraniteS").AppearancesDefault[0]);
                        SRSingleton<GameContext>.Instance.SlimeDefinitions.Slimes = SRSingleton<GameContext>.Instance.SlimeDefinitions.Slimes.AddItem(pomegraniteSlime).ToArray();
                        SRSingleton<GameContext>.Instance.SlimeDefinitions.slimeDefinitionsByIdentifiable.TryAdd(pomegraniteSlime, pomegraniteSlime);
                        break;
                    }
            }
        }
    }
}
