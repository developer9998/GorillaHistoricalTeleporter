using UnityEngine;

namespace GorillaHistoricalTeleporter.Extensions
{
    public static class UnityObjectEx
    {
        public static GameObject GetParentObjectWithTag(this GameObject gameObject, string tag, out GameObject lastObject)
        {
            lastObject = null;

            if (gameObject.CompareTag(tag))
                return gameObject;

            Transform transform = gameObject.transform;

            while (transform.parent is not null)
            {
                lastObject = transform.gameObject;
                if (transform.parent.CompareTag(tag))
                    return transform.parent.gameObject;
                transform = transform.parent;
            }

            return null;
        }
    }
}
