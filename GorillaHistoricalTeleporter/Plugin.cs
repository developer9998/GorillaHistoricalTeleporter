using BepInEx;
using HarmonyLib;

namespace GorillaHistoricalTeleporter
{
    [BepInPlugin(Constants.GUID, Constants.Name, Constants.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public void Awake()
        {
            Harmony.CreateAndPatchAll(typeof(Plugin).Assembly, Constants.GUID);
        }
    }
}
