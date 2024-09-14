namespace MCE_ASP_NET_MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add mvc services.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            app.MapDefaultControllerRoute();
            app.UseStaticFiles();
            app.Run();
        }
    }
}