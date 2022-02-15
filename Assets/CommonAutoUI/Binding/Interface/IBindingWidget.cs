

public interface IBindingWidget 
{
    void Bind<T>(IBindingData data, string fieldName) where T : struct;

    void Unbind();
}
