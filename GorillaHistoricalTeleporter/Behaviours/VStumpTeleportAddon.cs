using System.Linq;
using System.Threading.Tasks;
using GorillaHistoricalTeleporter.Extensions;
using GorillaHistoricalTeleporter.Tools;
using GT_CustomMapSupportRuntime;
using UnityEngine;

namespace GorillaHistoricalTeleporter.Behaviours
{
    [RequireComponent(typeof(VirtualStumpTeleporter)), DisallowMultipleComponent]
    public class VStumpTeleportAddon : MonoBehaviour
    {
        public VirtualStumpTeleporter Teleporter;

        private float stayInTriggerDuration;

        private GameObject teleporterDecor;

        private GameObject treeRoom, stumpVRHeadset;

        public async Task Awake()
        {
            Teleporter = GetComponent<VirtualStumpTeleporter>();
            stayInTriggerDuration = Teleporter.stayInTriggerDuration;
            Teleporter.stayInTriggerDuration = 1f;

            GetComponent<Collider>().enabled = false;

            teleporterDecor = Instantiate(await AssetLoader.LoadAsset<GameObject>("Teleporter"), transform.parent.parent);
            teleporterDecor.transform.localPosition = new Vector3(0.0073f, -0.4491f, 0.0255f);
            teleporterDecor.transform.localScale = Vector3.one * 0.45f;
            teleporterDecor.transform.rotation = Quaternion.identity;
            teleporterDecor.GetComponentsInChildren<MeshRenderer>(true)
                .Where(renderer => renderer is not null && renderer.gameObject.name == "Cylinder")
                .ForEach(renderer =>
                {
                    Material material = renderer.material;
                    Vector2 textureScale = material.GetTextureScale("_Gradient");
                    material.SetTextureScale("_Gradient", new Vector2(textureScale.x / 0.45f, textureScale.y / 0.45f));
                });
            teleporterDecor.GetComponentsInChildren<MeshCollider>(true)
                .ForEach(collider =>
                {
                    if (collider.GetComponent<GorillaSurfaceOverride>() is null)
                        collider.gameObject.AddComponent<GorillaSurfaceOverride>().overrideIndex = (int)SurfaceSoundOverride.metalhandtap;
                });

            teleporterDecor.AddComponent<VStumpExternalTrigger>().Teleporter = Teleporter;

            treeRoom = gameObject.GetParentObjectWithTag(UnityTag.ZoneRoot.ToString(), out stumpVRHeadset);
            if (treeRoom)
            {
                ConfigureTreeRoom(false);
                ConfigureHeadset(false, false);
            }
        }

        public void OnDestroy()
        {
            GetComponent<Collider>().enabled = true;

            Teleporter.stayInTriggerDuration = stayInTriggerDuration;

            if (teleporterDecor)
                Destroy(teleporterDecor);

            ConfigureTreeRoom(true);
            ConfigureHeadset(true, true);
        }

        public void SetActive(bool isActive)
        {
            if (teleporterDecor.transform.Find("Active") is Transform active)
                active.gameObject.SetActive(isActive);
            if (teleporterDecor.transform.Find("Active") is Transform inactive)
                inactive.gameObject.SetActive(!isActive);
        }

        public void ConfigureTreeRoom(bool renderMeshes)
        {
            if (treeRoom is null)
                return;

            if (treeRoom.transform.Find(Constants.CombinedMeshName) is Transform combinedMeshTransform && combinedMeshTransform.TryGetComponent(out MeshRenderer combinedRenderer))
                combinedRenderer.forceRenderingOff = !renderMeshes;
            if (treeRoom.transform.Find(Constants.TempMeshName) is Transform tempMeshTransform && tempMeshTransform.TryGetComponent(out MeshRenderer tempRenderer))
                tempRenderer.forceRenderingOff = !renderMeshes;
        }

        public void ConfigureHeadset(bool renderMeshes, bool enableColliders)
        {
            if (stumpVRHeadset is null)
                return;

            foreach (Transform child in stumpVRHeadset.transform)
            {
                if (child.GetComponent<MeshRenderer>() is MeshRenderer renderer)
                    renderer.forceRenderingOff = !renderMeshes;
                if (child.GetComponent<Collider>() is Collider collider)
                    collider.enabled = enableColliders;
            }
        }

        public bool HideHandHolds()
            => true;

        public bool ShowHandHolds()
        {
            Teleporter.HideHandHolds();
            return false;
        }

        public bool HideCountdownText()
            => true;

        public bool ShowCountdownText()
        {
            Teleporter.HideCountdownText();
            return false;
        }

        public bool UpdateCountdownText()
        {
            Teleporter.HideCountdownText();
            return false;
        }
    }
}
