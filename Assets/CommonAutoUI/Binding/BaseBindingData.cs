using System;
using System.Reflection;


public class BaseBindingData : IBindingData
{
    public event Action ON_DATA_CHANGED;

    public object GetField(string name)
    {
        Type t = this.GetType();
        FieldInfo fieldInfo = t.GetField(name);

        return fieldInfo.GetValue(this);
    }

    public void SetField(string name, object val) 
    {
        Type t = this.GetType();
        FieldInfo propertyInfo = t.GetField(name);

        propertyInfo.SetValue(this, val);
    }

}
