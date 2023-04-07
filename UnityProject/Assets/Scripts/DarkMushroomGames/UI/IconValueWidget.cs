using CCGKit;
using TMPro;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// A widget that uses an icon and a text value label to indicate the current value of a tracked runtime variable.
    /// </summary>
    public class IconValueWidget : MonoBehaviour
    {
        [SerializeField,Tooltip("The label that will show the value of the tracked runtime variable.")]
        private TextMeshProUGUI valueLabel;

        [SerializeField,Tooltip("The runtime variable that this widget should track changes for and is supposed to represent.")] 
        private IntVariable trackedRuntimeVariable;

        public void Awake()
        {
            if (valueLabel == null)
            {
                valueLabel = GetComponentInChildren<TextMeshProUGUI>();
                if (valueLabel != null)
                {
                    Debug.LogWarning("The value label was not assigned but a suitable component was found.", gameObject);
                }
                else
                {
                    Debug.LogError(
                        "The value label could not be correctly assigned. Please make sure the widget is setup properly.");
                }
            }
        
            Debug.Assert(trackedRuntimeVariable != null,
                "Please make sure to set the runtime variable that this widget should track.", gameObject);
        }

        public void Start()
        {
            SetValue(trackedRuntimeVariable.Value);
        }
    
        /// <summary>
        /// The event handler for when the tracked runtime variable's value changes.
        /// </summary>
        /// <param name="value">The new value of the tracked variable.</param>
        public void OnTrackedVariableChange(int value)
        {
            SetValue(value);
        }

        /// <summary>
        /// Sets the text field of the value to the new value.
        /// </summary>
        /// <param name="value">The new value to display.</param>
        private void SetValue(int value)
        {
            valueLabel.text = value.ToString();
        }
    }
}