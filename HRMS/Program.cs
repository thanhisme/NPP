using HRMS;
using HRMS.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterAppServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials());

app.UseMiddleware<GlobalExceptionHandler>();

app.UseMiddleware<CheckTokenInBlackList>();

app.UseAuthentication();

app.UseAuthorization();

// authentication Token
app.MapControllers().RequireAuthorization();

app.Run();
