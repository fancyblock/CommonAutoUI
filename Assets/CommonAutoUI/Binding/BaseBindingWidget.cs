using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseBindingWidget : MonoBehaviour, IBindingWidget
{
    public void Bind<T>(IBindingData data, string fieldName)
    {
        //TODO 
    }

    public void Unbind()
    {
        //TODO 
    }


    protected virtual void updateWidgetValue() { }
}
