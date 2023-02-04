using Bielu.Content.Populator.Integration.Example;
using Bielu.Content.Populator.Services;
using Bielu.Content.Populator.Umbraco;
using Bielu.Content.Populator.Umbraco.Services;
using Bielu.Content.Populator.Umbraco.Visitors;

namespace Bielu.Content.Populator.TestProject.Umbraco
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup" /> class.
        /// </summary>
        /// <param name="webHostEnvironment">The web hosting environment.</param>
        /// <param name="config">The configuration.</param>
        /// <remarks>
        /// Only a few services are possible to be injected here https://github.com/dotnet/aspnetcore/issues/9337.
        /// </remarks>
        public Startup(IWebHostEnvironment webHostEnvironment, IConfiguration config)
        {
            _env = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <remarks>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940.
        /// </remarks>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddContentPopulator(configuration =>
            {
                configuration.AddExample();
            });
            services.AddUmbraco(_env, _config)
                .AddBackOffice()
                .AddWebsite()
                .AddComposers()
                .Build();
            services.AddScoped<IExportService, UmbracoExportService>();
            services.AddScoped<IContentDefinitionVisitor, ContentDefVIsitor>();
            services.AddScoped<IContentVisitor, ContentVisitor>();
            services.AddScoped<IMediaVisitor, MediaVisitor>();
            services.AddScoped<IContentTypeVisitor, ContentTypeVisitor>();
            //services.AddScoped<Imedia, ContentTypeVisitor>();
            services.AddScoped<IPropertyVisitor, PropertyVisitor>();
            services.AddScoped<IDataTypeVisitor, DataTypeVisitor>();
            services.AddScoped<IExportService, UmbracoExportService>();
        }

        /// <summary>
        /// Configures the application.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="env">The web hosting environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseUmbraco()
                .WithMiddleware(u =>
                {
                    u.UseBackOffice();
                    u.UseWebsite();
                })
                .WithEndpoints(u =>
                {
                    u.UseInstallerEndpoints();
                    u.UseBackOfficeEndpoints();
                    u.UseWebsiteEndpoints();
                });
        }
    }
}
