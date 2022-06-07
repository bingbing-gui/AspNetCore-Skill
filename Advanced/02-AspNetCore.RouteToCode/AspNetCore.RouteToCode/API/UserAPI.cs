namespace AspNetCore.RouteToCode.API
{
    public class UserAPI
    {

        public static void Map(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/user/{id}", async context =>
            {
                // Get user logic...
            });

            endpoints.MapGet("/user", async context =>
            {
                // Get all users logic...
            });
        }
    }
}
