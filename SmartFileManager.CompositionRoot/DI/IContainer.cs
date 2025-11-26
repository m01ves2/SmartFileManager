namespace SmartFileManager.CompositionRoot.DI
{
    public interface IContainer
    {
        void RegisterSingleton<TService, TImplementation>() where TImplementation : TService, new();

        TService Resolve<TService>();
    }
}
