using System;
using System.Reflection;


public class BaseBindingData : IBindingData
{
    public event Action<string,object> ON_DATA_CHANGED;

    public object GetField(string name)
    {
        Type t = this.GetType();
        FieldInfo fieldInfo = t.GetField(name);

        return fieldInfo.GetValue(this);
    }

    public void SetField(string name, object val) 
    {
        Type t = this.GetType();
        FieldInfo fieldInfo = t.GetField(name);

        fieldInfo.SetValue(this, val);

        ON_DATA_CHANGED.Invoke(name, val);
    }

    public Type GetFieldType(string name)
    {
        Type t = this.GetType();
        FieldInfo fieldInfo = t.GetField(name);

        return fieldInfo.FieldType;
    }
}
