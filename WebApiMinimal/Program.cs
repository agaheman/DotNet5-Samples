using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

Host.CreateDefaultBuilder(args)
              .ConfigureWebHostDefaults(webBuilder =>
              {
                  webBuilder.UseStartup<Startup>();
              })
              .Build().Run();

public class Startup
{
    public void Configure(IApplicationBuilder app)
    {
        app.UseRouting()
           .UseEndpoints(endpoints =>
               {
                   endpoints.MapGet("/{id:int?}", async context =>
                   {
                       await context.Response.WriteAsJsonAsync(new { Response = "Data Recieved", ReceivedData = context.Request.Query });
                   });
                   endpoints.MapPost("/", ReceiveData);
               });
    }

    public record User(string FirstName, string LastName);
    private async Task ReceiveData(HttpContext context)
    {
        var user = await context.Request.ReadFromJsonAsync<User>();

        await context.Response.WriteAsJsonAsync(new { RawInput = user, FullName = $"{user.FirstName} {user.LastName}" });
    }
}