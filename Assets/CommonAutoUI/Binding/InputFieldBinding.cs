using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(InputField))]
public class InputFieldBinding : BaseBindingWidget
{
    private InputField m_inputField;
    //TODO 


    void Awake()
    {
        m_inputField = GetComponent<InputField>();
        m_inputField.onValueChanged.AddListener(onInputfieldValueChanged);
    }


    protected override void onDataChange<T>(T val) 
    {
        m_inputField.text = val.ToString();
    }

    protected override void onBind() 
    {
        m_bindingObject.ON_DATA_CHANGED += onDataChanged;
    }

    protected override void onUnbind() 
    {
        m_bindingObject.ON_DATA_CHANGED -= onDataChanged;
    }


    private void onInputfieldValueChanged(string newValue)
    {
        m_bindingObject.SetField(m_bindingField, newValue);
    }

    private void onDataChanged(object newValue)
    {
        m_inputField.text = newValue.ToString();
    }

}
