using DeliciousSlimeSR2.Slimes;
using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DeliciousSlimeSR2.Components
{
    internal class DeliciousTransformation : MonoBehaviour
    {
        void OnCollisionEnter(Collision collision)
        {
            GameObject obj = collision.gameObject;

            if (!obj.GetComponent<Rigidbody>())
                return;

            if (!obj.GetComponent<IdentifiableActor>())
                return;

            if (obj.GetComponent<IdentifiableActor>().identType == Utility.Get<IdentifiableType>("PomegraniteFruit"))
            {
                SRBehaviour.SpawnAndPlayFX(Utility.Get<GameObject>("slimePink").GetComponent<SlimeEat>().TransformFX, transform.position, transform.rotation);
                SRBehaviour.InstantiateActor(PomegraniteSlime.pomegraniteSlime.prefab, SRSingleton<SceneContext>.Instance.RegionRegistry.CurrentSceneGroup, transform.position, transform.rotation);
                Destroyer.DestroyActor(obj, "DeliciousTransformation.OnCollisionEnter");
                Destroyer.DestroyActor(gameObject, "DeliciousTransformation.OnCollisionEnter");
            }
            else if (obj.GetComponent<IdentifiableActor>().identType == Utility.Get<IdentifiableType>("MoondewNectar"))
            {
                SRBehaviour.SpawnAndPlayFX(Utility.Get<GameObject>("slimePink").GetComponent<SlimeEat>().TransformFX, transform.position, transform.rotation);
                SRBehaviour.InstantiateActor(MoondewSlime.moondewSlime.prefab, SRSingleton<SceneContext>.Instance.RegionRegistry.CurrentSceneGroup, transform.position, transform.rotation);
                Destroyer.DestroyActor(obj, "DeliciousTransformation.OnCollisionEnter");
                Destroyer.DestroyActor(gameObject, "DeliciousTransformation.OnCollisionEnter");
            }
        }
    }
}
