using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseBindingData : IBindingData
{
    public event Action ON_DATA_CHANGED;

    public object GetField(string name)
    {
        throw new NotImplementedException();
    }

    public void SetField(string name, object val)
    {
        //TODO 
    }

}
