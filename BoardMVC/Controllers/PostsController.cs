using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Section26_BoardMVC.Data;
using Section26_BoardMVC.Models;

namespace Section26_BoardMVC.Controllers
{
    public class PostsController : Controller
    {
        private readonly AppDbContext _context;

        // DI(의존성 주입): Program.cs에 등록해둔 AppDbContext를 자동으로 넣어줌
        public PostsController(AppDbContext context)
        {
            _context = context;
        }

        // ===== Read (목록) =====
        // GET: /Posts 또는 /Posts/Index
        public async Task<IActionResult> Index()
        {
            var posts = await _context.Posts
                .OrderByDescending(p => p.CreatedAt) // 최신순 정렬
                .ToListAsync();

            return View(posts); // Views/Posts/Index.cshtml 로 posts를 넘김
        }

        // ===== Read (상세) =====
        // GET: /Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var post = await _context.Posts.FindAsync(id);
            if (post == null) return NotFound();

            return View(post);
        }

        // ===== Create (작성 폼 보여주기) =====
        // GET: /Posts/Create
        public IActionResult Create()
        {
            return View(); // 빈 폼 화면
        }

        // ===== Create (작성 폼 제출 처리) =====
        // POST: /Posts/Create
        [HttpPost]
        [ValidateAntiForgeryToken] // CSRF 공격 방지 토큰 검증
        public async Task<IActionResult> Create(
            // [Bind]: 화이트리스트 방식으로 "이 필드들만 폼에서 받겠다" 명시 → 오버포스팅 방지
            [Bind("Title,Content,Writer")] Post post)
        {
            if (ModelState.IsValid) // [Required], [MaxLength] 등 데이터 어노테이션 검증 통과 여부
            {
                post.CreatedAt = DateTime.Now; // 서버에서 직접 채움 (사용자 입력 안 받음)
                _context.Add(post);
                await _context.SaveChangesAsync(); // 여기서 실제 DB에 INSERT 실행됨
                return RedirectToAction(nameof(Index));
            }
            // 검증 실패 시, 입력했던 값 그대로 폼에 다시 보여주며 에러 메시지 표시
            return View(post);
        }

        // ===== Update (수정 폼 보여주기) =====
        // GET: /Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var post = await _context.Posts.FindAsync(id);
            if (post == null) return NotFound();

            return View(post);
        }

        // ===== Update (수정 폼 제출 처리) =====
        // POST: /Posts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,Writer,CreatedAt")] Post post)
        {
            if (id != post.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // 주의: Remove + Add가 아니라, 값만 바꾸고 Update
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Posts.Any(e => e.Id == post.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // ===== Delete (삭제 확인 화면) =====
        // GET: /Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var post = await _context.Posts.FindAsync(id);
            if (post == null) return NotFound();

            return View(post); // "정말 삭제하시겠습니까?" 확인 화면
        }

        // ===== Delete (실제 삭제 실행) =====
        // POST: /Posts/Delete/5
        [HttpPost, ActionName("Delete")] // GET Delete와 이름 충돌 안 나게 액션명을 "Delete"로 지정
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}