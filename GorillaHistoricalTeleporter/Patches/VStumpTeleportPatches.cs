using GorillaHistoricalTeleporter.Behaviours;
using HarmonyLib;
using UnityEngine;

namespace GorillaHistoricalTeleporter.Patches
{
    [HarmonyPatch(typeof(VirtualStumpTeleporter)), HarmonyWrapSafe]
    public class VStumpTeleportPatches
    {
        [HarmonyPatch(nameof(VirtualStumpTeleporter.OnEnable)), HarmonyPrefix]
        public static void EnablePatch(VirtualStumpTeleporter __instance)
        {
            if (__instance.entrancePoint == GTZone.forest && !__instance.GetComponent<VStumpTeleportAddon>())
                __instance.gameObject.AddComponent<VStumpTeleportAddon>();
        }

        [HarmonyPatch(nameof(VirtualStumpTeleporter.OnDisable)), HarmonyPrefix]
        public static void DisablePatch(VirtualStumpTeleporter __instance)
        {
            if (__instance.TryGetComponent(out VStumpTeleportAddon component))
                Object.Destroy(component);
        }

        [HarmonyPatch(nameof(VirtualStumpTeleporter.ShowHandHolds)), HarmonyPrefix]
        public static bool ShowHandHoldsPatch(VirtualStumpTeleporter __instance)
        {
            return !__instance.TryGetComponent(out VStumpTeleportAddon component) || component.ShowHandHolds();
        }

        [HarmonyPatch(nameof(VirtualStumpTeleporter.HideHandHolds)), HarmonyPrefix]
        public static bool HideHandHoldsPatch(VirtualStumpTeleporter __instance)
        {
            return !__instance.TryGetComponent(out VStumpTeleportAddon component) || component.HideHandHolds();
        }

        [HarmonyPatch(nameof(VirtualStumpTeleporter.ShowCountdownText)), HarmonyPrefix]
        public static bool ShowTextPatch(VirtualStumpTeleporter __instance)
        {
            return !__instance.TryGetComponent(out VStumpTeleportAddon component) || component.ShowCountdownText();
        }

        [HarmonyPatch(nameof(VirtualStumpTeleporter.HideCountdownText)), HarmonyPrefix]
        public static bool HideTextPatch(VirtualStumpTeleporter __instance)
        {
            return !__instance.TryGetComponent(out VStumpTeleportAddon component) || component.HideCountdownText();
        }

        [HarmonyPatch(nameof(VirtualStumpTeleporter.UpdateCountdownText)), HarmonyPrefix]
        public static bool UpdateTextPatch(VirtualStumpTeleporter __instance)
        {
            return !__instance.TryGetComponent(out VStumpTeleportAddon component) || component.UpdateCountdownText();
        }

        [HarmonyPatch(nameof(VirtualStumpTeleporter.OnUGCEnabled)), HarmonyPrefix]
        public static void UGCEnablePatch(VirtualStumpTeleporter __instance)
        {
            if (__instance.TryGetComponent(out VStumpTeleportAddon component))
                component.OnUGCEnabled();
        }

        [HarmonyPatch(nameof(VirtualStumpTeleporter.OnUGCDisabled)), HarmonyPrefix]
        public static void UGCDisablePatch(VirtualStumpTeleporter __instance)
        {
            if (__instance.TryGetComponent(out VStumpTeleportAddon component))
                component.OnUGCDisabled();
        }
    }
}
