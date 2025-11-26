namespace SmartFileManager.CompositionRoot.DI
{
    public class SimpleContainer : IContainer
    {
        private readonly Dictionary<Type, object> singletons = new();


        public void RegisterSingleton<TService, TImplementation>() where TImplementation : TService, new()
        {
            // Реализация Singleton
            if (!singletons.ContainsKey(typeof(TService))) {
                TService instance = new TImplementation();
                singletons[typeof(TService)] = instance;
            }
        }

        public TService Resolve<TService>()
        {
            if (singletons.ContainsKey(typeof(TService))) {
                return (TService)singletons[typeof(TService)];
            }

            throw new InvalidOperationException($"No registration for type {typeof(TService)}");
        }
    }
}
