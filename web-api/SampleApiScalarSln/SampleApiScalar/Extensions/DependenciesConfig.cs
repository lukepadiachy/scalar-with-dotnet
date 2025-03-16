namespace SampleApiScalar.Extensions
{
    public static class DependenciesConfig
    {
        public static void AddDependencies(this WebApplicationBuilder builder) 
        {
            builder.Services.AddOpenApiServices();
        }
    }
}