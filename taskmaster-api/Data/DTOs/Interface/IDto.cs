namespace taskmaster_api.Data.DTOs.Interface
{
    public interface IDto<TDestination>
    {
        TDestination ToEntity();
    }
}
