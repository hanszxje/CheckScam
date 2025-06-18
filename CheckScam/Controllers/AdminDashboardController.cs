using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CheckScam.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.IO;

namespace CheckScam.Controllers
{
    [Authorize(Roles = "Superuser")]
    public class AdminDashboardController : Controller
    {
        private readonly CheckScamDbContext _context;

        public AdminDashboardController(CheckScamDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var scamPosts = await _context.ScamPosts
                .Where(p => p.Status == "pending")
                .ToListAsync();
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
            }
            return View(scamPosts);
        }

        public async Task<IActionResult> Result(int id)
        {
            var scam = await _context.ScamPosts
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (scam == null)
            {
                return NotFound();
            }
            return View(scam);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveOrReject(int id, string action)
        {
            var scam = await _context.ScamPosts.FindAsync(id);
            if (scam == null)
            {
                return NotFound();
            }

            if (action == "approve")
            {
                scam.Status = "approved";
                TempData["Message"] = $"✅ Đã duyệt bài tố cáo: {scam.NameScam}";
            }
            else if (action == "reject")
            {
                scam.Status = "rejected";
                TempData["Message"] = $"❌ Đã từ chối bài tố cáo: {scam.NameScam}";
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var scam = await _context.ScamPosts
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (scam == null)
            {
                TempData["Message"] = $"❌ Không tìm thấy bài tố cáo!";
                return RedirectToAction("Index");
            }

            try
            {
                // Xóa file hình ảnh vật lý
                foreach (var image in scam.Images)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", image.ImagePath.TrimStart('/'));
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                // Xóa bài và hình ảnh trong database
                _context.ScamImages.RemoveRange(scam.Images);
                _context.ScamPosts.Remove(scam);
                await _context.SaveChangesAsync();

                TempData["Message"] = $"🗑️ Đã xóa bài tố cáo: {scam.NameScam}";
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"❌ Lỗi khi xóa bài: {ex.Message}";
            }

            return RedirectToAction("Index");
        }
    }
}