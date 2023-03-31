using TMPro;
using UnityEngine;
using CCGKit;
public class IconWidget : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField,Tooltip("The runtime variable that this widget should track changes for and is supposed to represent.")] 
    private IntVariable trackedRuntimeVariable;

    public void Awake()
    {
        Debug.Assert(trackedRuntimeVariable != null,
            "Please make sure to set the runtime variable that this widget should track.", gameObject);
    }

    public void Start()
    {
        SetValue(trackedRuntimeVariable.Value);
    }
    
    public void OnTrackedVariableChange(int value)
    {
        SetValue(value);
    }

    private void SetValue(int value)
    {
        text.text = value.ToString();
    }

    
}