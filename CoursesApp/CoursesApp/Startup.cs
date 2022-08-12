using CoursesApp.Data;
using CoursesApp.MapperProfile;
using CoursesApp.Repository;
using CoursesApp.Services;
using CoursesApp.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoursesApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CourseProfile));
            services.AddAutoMapper(typeof(StudentGroupProfile));
            services.AddAutoMapper(typeof(StudentProfile));
            services.AddControllers();

            //var dbConnectionString = Configuration.GetConnectionString("default");
            services.AddDbContext<AppDBContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:default"]));


            services.AddControllersWithViews();
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<IStudGroupService, StudGroupService>();
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<ICourseRepository, CourseRepository>();
            services.AddTransient<IStudGroupRepository, StudGroupRepository>();
            services.AddTransient<IStudentRepository, StudentRepository>();

            services.AddControllersWithViews().AddNewtonsoftJson(option => 
                                    option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
