using Microsoft.EntityFrameworkCore;
using Section26_BoardMVC.Models;

namespace Section26_BoardMVC.Data
{
    public class AppDbContext : DbContext
    {
        // 부모(DbContext) 생성자에게 연결 설정(options)을 그대로 전달
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSet<Post> = "Posts" 테이블과 매핑되는 진입점
        // 이걸 통해 _context.Posts.Add(...), _context.Posts.ToList() 등을 사용
        public DbSet<Post> Posts { get; set; }
    }
}