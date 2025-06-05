using System;
using HarmonyLib;
using Oculus.Platform;

namespace GorillaHistoricalTeleporter.Patches
{
    [HarmonyPatch(typeof(GorillaMetaReport), nameof(GorillaMetaReport.Start))]
    public class MetaReportBoardPatch
    {
        public static void Finalizer(GorillaMetaReport __instance, Exception __exception)
        {
            if (__exception is not null && __instance is not null && __exception is DllNotFoundException)
            {
                AbuseReport.SetReportButtonPressedNotificationCallback(new Message<string>.Callback(__instance.OnReportButtonIntentNotif));
                MothershipClientApiUnity.OnMessageNotificationSocket += __instance.OnNotification;
                __instance.gameObject.SetActive(false);
            }
        }
    }
}
