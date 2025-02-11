using System.Text;
using System.Text.Json.Serialization;
using api.Data;
using api.Repositories.Admins;
using api.Repositories.BannerDetails;
using api.Repositories.Banners;
using api.Repositories.Brands;
using api.Repositories.Categories;
using api.Repositories.Feedbacks;
using api.Repositories.NewsDetails;
using api.Repositories.Newss;
using api.Repositories.Orders;
using api.Repositories.Products;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
}).AddJsonOptions(options =>
    {
        // Serialize enums as strings
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });


builder.Services.AddDbContext<ShopAppDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IBrandRepository,BrandRepository>();
builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped<IAdminRepository,AdminRepository>();
builder.Services.AddScoped<IFeedbackRepository,FeedbackRepository>();
builder.Services.AddScoped<INewsRepository,NewsRepository>();
builder.Services.AddScoped<INewsDetailRepository,NewsDetailRepository>();
builder.Services.AddScoped<IBannerRepository,BannerRepository>();
builder.Services.AddScoped<IBannerDetailRepository,BannerDetailRepository>();
builder.Services.AddScoped<IOrderRepository,OrderRepository>();



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:5000") // Replace with allowed origin(s)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters  
    {  
        ValidateIssuer = true,  
        ValidateAudience = true,  
        ValidateLifetime = true,  
        ValidateIssuerSigningKey = true,  
        ValidIssuer = builder.Configuration["Jwt:Issuer"],  
        ValidAudience = builder.Configuration["Jwt:Audience"],  
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))  
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigins");
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();

app.Run();

