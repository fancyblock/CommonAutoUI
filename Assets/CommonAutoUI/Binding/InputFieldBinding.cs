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


    protected override void onDataChange<T>(T val) 
    {
        m_inputField.text = val.ToString();
    }


    private void onInputfieldValueChanged(string newValue)
    {
        //if(m_bindingObject != null && !string.IsNullOrEmpty(m_bindingField))
        //{
        //    Type t = m_bindingObject.GetType();
        //    PropertyInfo propertyInfo = t.GetProperty(m_bindingField);

        //    propertyInfo.SetValue(m_bindingObject, m_inputField.text);
        //}
    }

}
