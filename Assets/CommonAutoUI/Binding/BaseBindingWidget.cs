using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseBindingWidget : MonoBehaviour, IBindingWidget
{
    private IBindingData m_bindData;


    public void Bind<T>(IBindingData data, string fieldName)
    {
        Unbind();

        m_bindData = data;

        m_bindData.ON_DATA_CHANGED += onDataChanged;

        m_bindingObject = bindingObject;
        m_bindingField = bindingField;

        bindUpdateValue();

        updateValue();
    }

    public void Unbind()
    {
        //TODO 
    }


    private void onDataChanged()
    {
        //TODO 
    }

    private void bindUpdateValue()
    {
        Type t = m_bindingObject.GetType();
        EventInfo eventInfo = t.GetEvent("ON_DATA_CHANGED");

        eventInfo.AddEventHandler(m_bindingObject, new Action(updateValue));
    }


    protected virtual void updateWidgetValue() { }
}
