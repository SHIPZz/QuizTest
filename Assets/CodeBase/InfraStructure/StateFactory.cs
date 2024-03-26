using VContainer;

namespace CodeBase.InfraStructure
{
    public class StateFactory : IStateFactory
    {
        private readonly IObjectResolver _objectResolver;

        public StateFactory(IObjectResolver objectResolver) =>
            _objectResolver = objectResolver;

        public IState Create<T>() where T : class, IState => 
            _objectResolver.Resolve<T>();
    }
}