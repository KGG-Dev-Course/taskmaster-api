namespace taskmaster_api.Data.Entities.Interface
{
    public interface IEntity<TDestination>
    {
        TDestination ToDto();
    }
}
