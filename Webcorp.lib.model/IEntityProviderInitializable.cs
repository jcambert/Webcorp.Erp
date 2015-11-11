namespace Webcorp.Model
{
    public interface IEntityProviderInitializable<T, TKey> where T : Entity
    {

        void InitializeProvider(EntityProvider<T, TKey> entityProvider);
    }
}