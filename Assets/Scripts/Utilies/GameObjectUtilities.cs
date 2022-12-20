using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyCell.Utilies
{
    public class GameObjectUtilities : MonoBehaviour
    {
        public static List<T> AddComponentsToGo<T>(GameObject gameObject, List<Type> components) where T : Component
        {
            List<T> currentComps = gameObject.GetComponents<T>().ToList();
            List<T> addedComps = new List<T>();

            foreach (Type component in components)
            {
                // See if component has already been added
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
                UnityEngine.Object.DestroyImmediate(comp);
#else
                UnityEngine.Object.Destroy(comp);
#endif
            }

            return addedComps;
        }
    }
}