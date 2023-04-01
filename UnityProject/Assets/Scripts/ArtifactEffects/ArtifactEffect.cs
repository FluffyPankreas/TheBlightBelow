using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace ArtifactEffects
{
    /// <summary>
    /// The base effect that all artifact effects should inherit from.
    /// </summary>
    [Serializable]
    public class Effect
    {
        [SerializeField] public string Name;
        [SerializeField] public int Number;
        public virtual void Resolve()
        {
            // This would be better as an abstract class but serialization is forcing this.
            Debug.LogError("This method should be overridden, something hasn't done that.");
        }
    }
}
