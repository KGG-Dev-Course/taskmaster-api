namespace taskmaster_api.Utilities
{
    public static class EntityHelpers
    {
        public static TDestination ToDto<TSource, TDestination>(TSource source)
            where TDestination : new()
        {
            var destination = new TDestination();
            var sourceProperties = typeof(TSource).GetProperties();
            var destinationProperties = typeof(TDestination).GetProperties();

            foreach (var sourceProp in sourceProperties)
            {
                var destinationProp = destinationProperties.FirstOrDefault(x => x.Name == sourceProp.Name);

                if (destinationProp != null && destinationProp.CanWrite)
                {
                    var value = sourceProp.GetValue(source);
                    destinationProp.SetValue(destination, value);
                }
            }

            return destination;
        }
    }

}
