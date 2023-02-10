using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MyCell.Utilities
{
    public static class GameObjectUtilities
    {
        public static List<T> AddComponentsToGO<T>(this GameObject gameObject, List<Type> components) where T : Component
        {
            List<T> currentComps = gameObject.GetComponents<T>().ToList();
            List<T> addedComps = new List<T>();

            foreach (Type component in components)
            {
                // See if dependency has already been added
                if (addedComps.FirstOrDefault(item => item.GetType() == component) != null) continue;

                // Check if dependency was already on GO
                var comp = currentComps.FirstOrDefault(item => item.GetType() == component);

                if (comp == null) // Component was not already on GO
                {
                    // Add comp to GO -- save ref to remove from lists
                    comp = (T) gameObject.AddComponent(component);
                }
                else
                {
                    // Remove comp from list as all comps in list will be destoryed later to remove uneeded comps
                    currentComps.Remove(comp);
                }

                addedComps.Add(comp);
            }

            foreach (T comp in currentComps)
            {
#if UNITY_EDITOR
                Object.DestroyImmediate(comp);
#else
                Object.Destroy(comp);
#endif
            }

            return addedComps;
        }
    }
}