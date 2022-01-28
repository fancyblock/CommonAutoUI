using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IBindingWidget 
{
    void Bind<T>(IBindingData data, string fieldName);

    void Unbind();
}
