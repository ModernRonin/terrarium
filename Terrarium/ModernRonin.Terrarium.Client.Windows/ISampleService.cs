namespace ModernRonin.Terrarium.Client.Windows
{
    public interface ISampleService
    {
        string Message { get; }
    }

    public class ConcreteService : ISampleService
    {
        public string Message => "Concretely implemented";
    }
}