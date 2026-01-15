using BusinessLayer.Abstract;
using BusinessLayer.Managers;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Dependency Injection - DAL
builder.Services.AddScoped<ICampaignDal, EfCampaingDal>();
builder.Services.AddScoped<IUserDal, EfUserDal>();
builder.Services.AddScoped<ICampaignAssignmentDal, EfCampainAssignmentDal>();
builder.Services.AddScoped<IUserMetricDal, EfUserMetricDal>();
builder.Services.AddScoped<INotificationDal, EfNotificationDal>();

// Dependency Injection - Business Layer
builder.Services.AddScoped<ICampaignService, CampaignManager>();
builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<ICampaignAssignmentService, CampaignAssignmentManager>();
builder.Services.AddScoped<IUserMetricsService, UserMetricsManager>();
builder.Services.AddScoped<INotifiticationService, NotificationManager>();
builder.Services.AddScoped<IMockDataService, MockDataManager>();
builder.Services.AddScoped<ICampaignRecommendationService, CampaignRecommendationManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
