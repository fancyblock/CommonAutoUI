using System;
using System.Reflection;


public class BaseBindingData : IBindingData
{
    public event Action ON_DATA_CHANGED;

    public T GetField<T>(string name)
    {
        Type t = this.GetType();
        FieldInfo fieldInfo = t.GetField(name);

        return (T)fieldInfo.GetValue(this);
    }

    public void SetField<T>(string name, T val) 
    {
        Type t = this.GetType();
        FieldInfo propertyInfo = t.GetField(name);

        propertyInfo.SetValue(this, val);
    }

}
