using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseBindingWidget : MonoBehaviour, IBindingWidget
{
    private IBindingData m_bindingObject;
    private string m_bindingField;


    public void Bind<T>(IBindingData bindingObject, string bindingField) where T : struct
    {
        Unbind();

        m_bindingObject = bindingObject;
        m_bindingField = bindingField;

        onDataChange<T>(m_bindingObject.GetField<T>(m_bindingField));

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
    protected virtual void onDataChange<T>(T val) where T : struct { }
}
