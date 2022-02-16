

public interface IBindingWidget 
{
    void Bind<T>(IBindingData bindingObject, string bindingField);

    void Unbind();
}
