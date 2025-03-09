using Farm.Models.Data;
using Farm.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Farm.Controllers;

// using Farm.API.Models.Data;
// using Starter.API.Repositories;
// using Starter.API.Service;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = null; // NOTE: set upload limit to unlimited, or specify the limit in number of bytes
});
// NOTE: set a very large limit for multipart/form-data encoded forms; this should be added regardless of setting the limit for a controller, action or the whole server
builder.Services.Configure<FormOptions>(options => options.MultipartBodyLengthLimit = long.MaxValue);

ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
    builder.AddDebug();
});

// builder.Services.AddAuthentication().AddCookie();

// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(options =>
//     {
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuer = true,
//             ValidateAudience = true,
//             ValidateLifetime = true,
//             ValidateIssuerSigningKey = true,
//             ValidIssuer = "FamrAPI", // Replace with your issuer
//             ValidAudience = "Public", // Replace with your audience
//             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("C1CF4B7DC4C4175B6618DE4F55CA4"))  // Replace with your secret key
//     };
// });

// builder.Services.AddAuthorization(options =>
// {
//     options.AddPolicy("ViewEmployee",
//         policy => policy.RequireClaim("View Employee")
//                         .RequireClaim("View Employee"));
// });

// Add services to the container.

builder.Services.AddControllers();

// builder.Services.AddControllers(config =>
// {
//     config.Filters.Add<AuthorizeRoleAttribute>();
// });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FarmDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("Starter")));
builder.Services.AddScoped <IAuthorisationRepository,AuthorisationRepository>();
builder.Services.AddScoped <IBroadcast_MessageRepository,Broadcast_MessageRepository>();
builder.Services.AddScoped <ICampaign_TypesRepository,Campaign_TypesRepository>();
builder.Services.AddScoped <ICampaignsRepository,CampaignsRepository>();
builder.Services.AddScoped <ICrop_VarietyRepository,Crop_VarietyRepository>();
builder.Services.AddScoped <IDistrictsRepository,DistrictsRepository>();
builder.Services.AddScoped <IDocument_TypeRepository,Document_TypeRepository>();
builder.Services.AddScoped <IDocumentsRepository,DocumentsRepository>();
builder.Services.AddScoped <IEmployee_RolesRepository,Employee_RolesRepository>();
builder.Services.AddScoped <IEmployee_TypesRepository,Employee_TypesRepository>();
builder.Services.AddScoped <IEmployeesRepository,EmployeesRepository>();
builder.Services.AddScoped <IFarm_DiseasesRepository,Farm_DiseasesRepository>();
builder.Services.AddScoped <IFarmer_GroupRepository,Farmer_GroupRepository>();
builder.Services.AddScoped <IFarmer_login_visit_logsRepository,Farmer_login_visit_logsRepository>();
builder.Services.AddScoped <IFarmer_trip_sheetsRepository,Farmer_trip_sheetsRepository>();
builder.Services.AddScoped <IFarmersRepository,FarmersRepository>();
builder.Services.AddScoped <IFarmers_LoginRepository,Farmers_LoginRepository>();
builder.Services.AddScoped <IFarmFieldRepository,FarmFieldRepository>();
builder.Services.AddScoped <IField_VisitRepository,Field_VisitRepository>();
builder.Services.AddScoped <IGenderRepository,GenderRepository>();
builder.Services.AddScoped <ILoginsRepository,LoginsRepository>();
builder.Services.AddScoped <ILogins_LogRepository,Logins_LogRepository>();
builder.Services.AddScoped <IMandal_BlocksRepository,Mandal_BlocksRepository>();
builder.Services.AddScoped <INursaryRepository,NursaryRepository>();
builder.Services.AddScoped <INursary_BatchesRepository,Nursary_BatchesRepository>();
builder.Services.AddScoped <IPhotosRepository,PhotosRepository>();
builder.Services.AddScoped <IPlantationIdentificationRepository,PlantationIdentificationRepository>();
builder.Services.AddScoped <IPoaching_FFBRepository,Poaching_FFBRepository>();
builder.Services.AddScoped <IPrivilegesRepository,PrivilegesRepository>();
builder.Services.AddScoped <IReferral_SourceRepository,Referral_SourceRepository>();
builder.Services.AddScoped <IRole_PrivilegesRepository,Role_PrivilegesRepository>();
builder.Services.AddScoped <IRolesRepository,RolesRepository>();
builder.Services.AddScoped <IStatesRepository,StatesRepository>();
builder.Services.AddScoped <ITraining_VideosRepository,Training_VideosRepository>();
builder.Services.AddScoped <IVillagesRepository,VillagesRepository>();
builder.Services.AddScoped <IWorkflowRepository,WorkflowRepository>();
builder.Services.AddScoped <IAuthenticateRepository,AuthenticateRepository>();
builder.Services.AddScoped <IUtilityHelper,UtilityHelper>();
builder.Services.AddScoped <INursary_ActivityRepository,Nursary_ActivityRepository>();
builder.Services.AddScoped <IUsersRepository,UsersRepository>();
builder.Services.AddScoped <IUsers_TypesRepository,Users_TypesRepository>();

builder.Services.AddTransient<AuthorizationMiddleware>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddCors(options =>
    {
        options.AddPolicy("allow-cors", policy =>
        {
            policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
        });
    });

    string corsDomains = "http://localhost:4200";
    string[] domains = corsDomains.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
    builder.Services.AddCors(o => o.AddPolicy("AppCORSPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials()
               .WithOrigins(domains);
    }));

var app = builder.Build();

app.UseMiddleware<AuthenticationMiddleware>();
app.UseMiddleware<AuthorizationMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment() || app.Environment.IsProduction() || app.Environment.IsStaging())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("../swagger/v1/swagger.json", "My API v1");
            // c.OAuthClientId("Client Id");
            // c.OAuthClientSecret("Client Secret Key");
            // c.OAuthUseBasicAuthenticationWithAccessCodeGrant();
            c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
        });
    }
    app.UseDeveloperExceptionPage();
    // app.UseAuthentication();
    // app.UseAuthorization();
    app.MapControllers();

    app.UseCors("allow-cors");
    app.UseCors("AppCORSPolicy");

app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapControllers();

app.Run();
