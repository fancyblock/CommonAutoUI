using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(InputField))]
public class InputFieldBinding : MonoBehaviour
{
    private InputField m_inputField;

    private object m_bindingObject;
    private string m_bindingField;         // getter setter


    void Awake()
    {
        m_inputField = GetComponent<InputField>();
        m_inputField.onValueChanged.AddListener(onInputfieldValueChanged);
    }


    /// <summary>
    /// Êý¾Ý°ó¶¨
    /// </summary>
    /// <param name="bindingObject"></param>
    /// <param name="bindingField"></param>
    /// <param name="updateMethodName"></param>
    public void Bind(object bindingObject, string bindingField)
    {
        Unbind();

        m_bindingObject = bindingObject;
        m_bindingField = bindingField;

        bindUpdateValue();

        updateValue();
    }

    public void Unbind()
    {
        if (m_bindingObject != null)
            unbindUpdateValue();
    }


    private void updateValue()
    {
        Type t = m_bindingObject.GetType();
        PropertyInfo propertyInfo = t.GetProperty(m_bindingField);

        m_inputField.text = propertyInfo.GetValue(m_bindingObject) as string;
    }

    private void bindUpdateValue() 
    {
        Type t = m_bindingObject.GetType();
        EventInfo eventInfo = t.GetEvent("ON_DATA_CHANGED");

        eventInfo.AddEventHandler(m_bindingObject, new Action(updateValue));
    }

    private void unbindUpdateValue() 
    {
        //TODO 
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
