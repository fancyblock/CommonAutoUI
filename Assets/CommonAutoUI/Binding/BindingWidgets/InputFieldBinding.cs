using System;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(InputField))]
public class InputFieldBinding : BaseBindingWidget
{
    private InputField m_inputField;


    void Awake()
    {
        m_inputField = GetComponent<InputField>();
        m_inputField.onValueChanged.AddListener(onInputfieldValueChanged);
    }


    protected override void onDataChange(object val) 
    {
        string strVal = val.ToString();

        if(m_inputField.text != strVal)
            m_inputField.text = strVal;
    }


    private void onInputfieldValueChanged(string newValue)
    {
        string val = BINDING_DATA.ToString();

        if (val != newValue)
        {
            object convertedValue = null;

            try
            {
                convertedValue = Convert.ChangeType(newValue, m_fieldType);
                m_bindingObject.SetField(m_bindingField, convertedValue);
            }
            catch(Exception e)
            {
                Debug.LogError($"Value {newValue} can not be convert to {m_fieldType.Name} for {m_bindingObject.GetType().Name}.{m_bindingField}");
            }
        }
    }
}
