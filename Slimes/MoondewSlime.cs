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
    internal class MoondewSlime
    {
        internal static SlimeDefinition moondewSlime;
        
        public static void Initialize()
        {
            moondewSlime = ScriptableObject.CreateInstance<SlimeDefinition>();
            moondewSlime.name = "MoondewS";
            moondewSlime.hideFlags |= HideFlags.HideAndDontSave;
            moondewSlime.color = new Color32(49, 209, 248, 255);
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
                        moondewSlime.localizedName = HarmonyPatches.LocalizationDirectorLoadTablePatch.AddTranslation("Actor", "l.moondew_slime", "Moondew Slime");

                        #region MOONDEW_SLIME
                        moondewSlime.prefab = Utility.PrefabUtils.CopyPrefab(Utility.Get<GameObject>("slimePink"));
                        moondewSlime.prefab.name = "slimeMoondew";

                        moondewSlime.prefab.GetComponent<Identifiable>().identType = moondewSlime;
                        moondewSlime.prefab.GetComponent<SlimeEat>().slimeDefinition = moondewSlime;
                        moondewSlime.prefab.GetComponent<PlayWithToys>().slimeDefinition = moondewSlime;
                        moondewSlime.prefab.GetComponent<ReactToToyNearby>().slimeDefinition = moondewSlime;

                        moondewSlime.Diet = UnityEngine.Object.Instantiate(Utility.Get<SlimeDefinition>("Pink")).Diet;
                        moondewSlime.Diet.MajorFoodGroups = Array.Empty<SlimeEat.FoodGroup>();
                        moondewSlime.Diet.MajorFoodIdentifiableTypeGroups = Array.Empty<IdentifiableTypeGroup>();
                        moondewSlime.Diet.ProduceIdents = Array.Empty<IdentifiableType>();
                        moondewSlime.Diet.AdditionalFoodIdents = Array.Empty<IdentifiableType>();
                        moondewSlime.Diet.FavoriteIdents = Array.Empty<IdentifiableType>();
                        moondewSlime.Diet.RefreshEatMap(SRSingleton<GameContext>.Instance.SlimeDefinitions, moondewSlime);

                        moondewSlime.icon = Utility.CreateSprite(Utility.LoadImage("Assets.iconSlimeMoondew"));
                        moondewSlime.properties = UnityEngine.Object.Instantiate(Utility.Get<SlimeDefinition>("Pink").properties);
                        moondewSlime.defaultPropertyValues = UnityEngine.Object.Instantiate(Utility.Get<SlimeDefinition>("Pink")).defaultPropertyValues;

                        SlimeAppearance slimeAppearance = UnityEngine.Object.Instantiate(Utility.Get<SlimeAppearance>("PinkDefault"));
                        SlimeAppearanceApplicator slimeAppearanceApplicator = moondewSlime.prefab.GetComponent<SlimeAppearanceApplicator>();
                        slimeAppearance.name = "MoondewDefault";
                        slimeAppearanceApplicator.Appearance = slimeAppearance;
                        slimeAppearanceApplicator.SlimeDefinition = moondewSlime;

                        Material moondewMaterial = UnityEngine.Object.Instantiate(slimeAppearance.Structures[0].DefaultMaterials[0]);
                        moondewMaterial.hideFlags |= HideFlags.HideAndDontSave;
                        moondewMaterial.SetColor("_TopColor", moondewSlime.color);
                        moondewMaterial.SetColor("_MiddleColor", moondewSlime.color);
                        moondewMaterial.SetColor("_BottomColor", new Color32(69, 243, 255, 255));
                        moondewMaterial.SetColor("_SpecColor", new Color32(69, 243, 255, 255));
                        slimeAppearance.Structures[0].DefaultMaterials[0] = moondewMaterial;

                        slimeAppearance.Face = UnityEngine.Object.Instantiate(Utility.Get<SlimeAppearance>("RingtailDefault").Face);
                        slimeAppearance.Face.name = "MoondewFace";

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
                                slimeEyes.SetColor("_EyeRed", new Color32(69, 243, 255, 255));
                                slimeEyes.SetColor("_EyeGreen", new Color32(69, 243, 255, 255));
                                slimeEyes.SetColor("_EyeBlue", new Color32(69, 243, 255, 255));
                            }
                            if (slimeMouth)
                            {
                                slimeMouth.SetColor("_MouthBot", new Color32(49, 209, 248, 255));
                                slimeMouth.SetColor("_MouthMid", new Color32(49, 209, 248, 255));
                                slimeMouth.SetColor("_MouthTop", new Color32(49, 209, 248, 255));
                            }
                            slimeExpressionFace.Eyes = slimeEyes;
                            slimeExpressionFace.Mouth = slimeMouth;
                            expressionFaces = expressionFaces.AddToArray(slimeExpressionFace);
                        }
                        slimeAppearance.Face.ExpressionFaces = expressionFaces;
                        slimeAppearance.Face.OnEnable();

                        slimeAppearance.Icon = Utility.CreateSprite(Utility.LoadImage("Assets.iconSlimeMoondew"));
                        slimeAppearance.SplatColor = moondewSlime.color;
                        slimeAppearance.ColorPalette = new SlimeAppearance.Palette
                        {
                            Ammo = moondewSlime.color,
                            Top = moondewSlime.color,
                            Middle = new Color32(69, 243, 255, 255),
                            Bottom = moondewSlime.color
                        };
                        moondewSlime.AppearancesDefault = new SlimeAppearance[] { slimeAppearance };
                        slimeAppearance.hideFlags |= HideFlags.HideAndDontSave;
                        #endregion
                        break;
                    }
                case "zoneCore":
                    {
                        SRSingleton<SceneContext>.Instance.SlimeAppearanceDirector.RegisterDependentAppearances(Utility.Get<SlimeDefinition>("MoondewS"), Utility.Get<SlimeDefinition>("MoondewS").AppearancesDefault[0]);
                        SRSingleton<SceneContext>.Instance.SlimeAppearanceDirector.UpdateChosenSlimeAppearance(Utility.Get<SlimeDefinition>("MoondewS"), Utility.Get<SlimeDefinition>("MoondewS").AppearancesDefault[0]);
                        SRSingleton<GameContext>.Instance.SlimeDefinitions.Slimes = SRSingleton<GameContext>.Instance.SlimeDefinitions.Slimes.AddItem(moondewSlime).ToArray();
                        SRSingleton<GameContext>.Instance.SlimeDefinitions.slimeDefinitionsByIdentifiable.TryAdd(moondewSlime, moondewSlime);
                        break;
                    }
            }
        }
    }
}
