using System;
using System.Reflection;
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


    private void updateValue()
    {
        Type t = m_bindingObject.GetType();
        PropertyInfo propertyInfo = t.GetProperty(m_bindingField);

        m_inputField.text = propertyInfo.GetValue(m_bindingObject) as string;
    }

    private void onInputfieldValueChanged(string newValue)
    {
        if(m_bindingObject != null && !string.IsNullOrEmpty(m_bindingField))
        {
            Type t = m_bindingObject.GetType();
            PropertyInfo propertyInfo = t.GetProperty(m_bindingField);

            propertyInfo.SetValue(m_bindingObject, m_inputField.text);
        }
    }

}
