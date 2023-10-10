using LMS.Data;
using LMS.Models;
using LMS.Repository;
using LMS.Services;
using LMS.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<Lms3Context>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmployeeManagementService, EmployeeManagementService>();   
builder.Services.AddScoped<IAdminLoanCardManagementService, AdminLoanCardManagementService>();
builder.Services.AddScoped<IAdminCustomerDataManagementService, AdminCustomerDataManagementService>();
builder.Services.AddScoped<IApplyForLoanService, ApplyForLoanService>();
builder.Services.AddScoped<IAdminLoanRequestService, AdminLoanRequestService>();
builder.Services.AddScoped<IItemDataManagementService,ItemDataManagementService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<EmployeeProvider>();
builder.Services.AddScoped<AdminLoanCardManagementProvider>();
builder.Services.AddScoped<EmployeeManagementProvider>();
builder.Services.AddScoped<AdminCustomerDataManagementProvider>();
builder.Services.AddScoped<ItemMasterProvider>();
builder.Services.AddScoped<LoanRequestProvider>();
builder.Services.AddScoped<AdminLoanRequestListProvider>();
builder.Services.AddScoped<CategoryProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
builder.Services.AddCors();
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.MapControllers();

app.Run();
