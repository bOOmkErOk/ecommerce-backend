namespace E_Commerce.Extensions.Core;

public static class ApplicationExtensions
{
    public static WebApplication UseCoreApplication(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.MapGraphQL("/graphql");

        return app;
    }
}