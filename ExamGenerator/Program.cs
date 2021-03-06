using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ExamGenerator.Data;
using ExamGenerator.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ExamGeneratorDbContextConnection");builder.Services.AddDbContext<ExamGeneratorDbContext>(options =>
    options.UseSqlite(connectionString));builder.Services.AddDefaultIdentity<ExamGeneratorUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ExamGeneratorDbContext>();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ListExams}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
