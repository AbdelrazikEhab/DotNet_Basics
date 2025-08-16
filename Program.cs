

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCors",
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5000", "http://localhost:8080")
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();
                      });
    options.AddPolicy("ProdCprs",
                  policy =>
                  {
                      policy.WithOrigins("http://MyProductSite.com")
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials();
                  });
});

// services.AddResponseCaching();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseCors("DevCors");
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseCors("ProdCprs");
    app.UseHttpsRedirection();
}

app.MapControllers();
app.Run();