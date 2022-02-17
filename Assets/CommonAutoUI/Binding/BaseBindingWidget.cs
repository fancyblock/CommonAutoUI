using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseBindingWidget : MonoBehaviour, IBindingWidget
{
    protected IBindingData m_bindingObject;
    protected string m_bindingField;


    public void Bind(IBindingData bindingObject, string bindingField) 
    {
        m_bindingObject = bindingObject;
        m_bindingField = bindingField;

        onDataChange<object>(m_bindingObject.GetField(m_bindingField));

        Unbind();
        onBind();
    }

    public void Unbind()
    {
        m_bindingObject = null;
        m_bindingField = null;

        onUnbind();
    }


    protected virtual void onBind() { }
    protected virtual void onUnbind() { }
    protected virtual void onDataChange<T>(T val) { }
}
