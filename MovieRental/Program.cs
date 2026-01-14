using MovieRental.Data;
using MovieRental.PaymentProviders;
using MovieRental.Rental;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEntityFrameworkSqlite().AddDbContext<MovieRentalDbContext>();

// Q: The app is throwing an error when we start, please help us. Also, tell us what caused the issue.
// A: Changed this line from AddSingleton to AddScoped because DbContext should have a scoped lifetime.
//    A Scoped service can depend on a singleton, the opposite cannot happen.
builder.Services.AddScoped<IRentalFeatures, RentalFeatures>();

builder.Services.AddScoped<IPaymentProvider, MbWayProvider>();
builder.Services.AddScoped<IPaymentProvider, PayPalProvider>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseExceptionHandler("/error");
app.UseAuthorization();
app.MapControllers();

using (var client = new MovieRentalDbContext())
{
    client.Database.EnsureCreated();
}

app.Run();
