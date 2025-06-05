using UnityEngine;

namespace GorillaHistoricalTeleporter.Behaviours
{
    [DisallowMultipleComponent]
    public class VStumpExternalTrigger : MonoBehaviour
    {
        public VirtualStumpTeleporter Teleporter;

        public void OnTriggerEnter(Collider collider)
        {
            Teleporter?.OnTriggerEnter(collider);
        }

        public void OnTriggerStay(Collider collider)
        {
            Teleporter?.OnTriggerStay(collider);
        }

        public void OnTriggerExit(Collider collider)
        {
            Teleporter?.OnTriggerExit(collider);
        }
    }
}
