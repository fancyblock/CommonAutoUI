using System;


public interface IBindingData 
{
    event Action ON_DATA_CHANGED;

    void SetField<T>(string name, T val);

    T GetField<T>(string name);

}
