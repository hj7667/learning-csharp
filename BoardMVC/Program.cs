using Microsoft.EntityFrameworkCore;
using Section26_BoardMVC.Data;

var builder = WebApplication.CreateBuilder(args);

// ── ConfigureServices 역할 (DI 컨테이너에 서비스 등록) ──
builder.Services.AddControllersWithViews();

// EF Core + SQL Server 연결 설정 등록
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);
var app = builder.Build();

// ── Configure 역할 (미들웨어 파이프라인 구성) ──
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // wwwroot 안의 css/js/이미지 서빙

app.UseRouting();

app.UseAuthorization();

// 최신 방식의 라우팅 등록 (예전 UseMvc + MapRoute의 후신)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();