namespace DemoMiddleWare.GenericClasses;

public class GenericBonjour<TMessage> where TMessage : class, IComparable<TMessage>
{
    public string BonjourAll(TMessage message)
    {
        
        return $"Bonjour {message.ToString()}";
    }
}
