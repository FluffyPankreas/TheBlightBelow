// Copyright (C) 2019-2022 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using UnityEngine;

namespace CCGKit
{
    [CreateAssetMenu(
        menuName = "Single-Player CCG Kit/Architecture/Variables/Integer",
        fileName = "Variable",
        order = 0)]
    public class IntVariable : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = string.Empty;
#endif

        public GameEventInt ValueChangedEvent;

        [SerializeField,Tooltip("The value stored by this integer variable.")]
        private int _value;

        public int Value
        {
            get { return _value;}
            set
            {
                _value = value; 
                ValueChangedEvent?.Raise(_value);
            }
        }

        public void SetValue(int newValue)
        {
            Value = newValue;
        }
    }
}