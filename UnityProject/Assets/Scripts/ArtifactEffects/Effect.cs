using System;
using UnityEngine;

namespace ArtifactEffects
{
    /// <summary>
    /// The base effect that all artifact effects should inherit from.
    /// </summary>
    [Serializable]
    public abstract class Effect : ScriptableObject
    {
        [SerializeField] public string Name;

        public abstract void Resolve();
    }
}
