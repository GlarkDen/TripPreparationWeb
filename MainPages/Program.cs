using MainPages.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<TripPreparationContext>();

builder.Services.Configure<RouteOptions>(options =>
{
	// ��������� ��� ������ � ������ �������
	options.LowercaseUrls = true;

	// ������ ������� (������������ � ������ ���������), �������� ID, NAME.. � ������ �������
	options.LowercaseQueryStrings = false;

	// �������� ���� ����� ������� ���������
	options.AppendTrailingSlash = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
