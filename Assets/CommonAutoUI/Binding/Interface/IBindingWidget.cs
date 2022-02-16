

public interface IBindingWidget 
{
    void Bind<T>(IBindingData bindingObject, string bindingField) where T : struct;

    void Unbind();
}
