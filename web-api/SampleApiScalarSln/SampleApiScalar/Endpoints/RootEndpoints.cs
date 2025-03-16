namespace SampleApiScalar.Endpoints
{
    public static class RootEndpoints
    {
        public static void AddRootEndpoints(this WebApplication app) 
        {
            app.MapGet("/", () => "Hello World!");
        }
    }
}
