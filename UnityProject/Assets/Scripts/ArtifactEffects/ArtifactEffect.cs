using System;
using UnityEngine;

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
        }
    }
}
