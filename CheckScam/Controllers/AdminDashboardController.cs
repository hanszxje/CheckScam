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
            var scamUrls = await _context.ScamUrls
                .Where(u => u.Status == "pending")
                .ToListAsync();
            ViewBag.ScamPosts = scamPosts;
            ViewBag.ScamUrls = scamUrls;
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
            }
            return View();
        }

        public async Task<IActionResult> Result(int id, string type)
        {
            if (type == "post")
            {
                var scam = await _context.ScamPosts
                    .Include(p => p.Images)
                    .FirstOrDefaultAsync(p => p.Id == id);
                if (scam == null)
                {
                    return NotFound();
                }
                return View("Result", scam);
            }
            else if (type == "url")
            {
                var scamUrl = await _context.ScamUrls
                    .FirstOrDefaultAsync(u => u.Id == id);
                if (scamUrl == null)
                {
                    return NotFound();
                }
                return View("ResultUrl", scamUrl);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ApproveOrReject(int id, string type, string action)
        {
            if (type == "post")
            {
                var scam = await _context.ScamPosts.FindAsync(id);
                if (scam == null)
                {
                    return NotFound();
                }

                if (action == "approve")
                {
                    scam.Status = "approved";
                    TempData["Message"] = $"✅ Đã duyệt bài tố cáo: {scam.StkScam ?? scam.SdtScam ?? "không xác định"}";
                }
                else if (action == "reject")
                {
                    scam.Status = "rejected";
                    TempData["Message"] = $"❌ Đã từ chối bài tố cáo: {scam.StkScam ?? scam.SdtScam ?? "không xác định"}";
                }

                await _context.SaveChangesAsync();
            }
            else if (type == "url")
            {
                var scamUrl = await _context.ScamUrls.FindAsync(id);
                if (scamUrl == null)
                {
                    return NotFound();
                }

                if (action == "approve")
                {
                    scamUrl.Status = "approved";
                    TempData["Message"] = $"✅ Đã duyệt URL tố cáo: {scamUrl.Url}";
                }
                else if (action == "reject")
                {
                    scamUrl.Status = "rejected";
                    TempData["Message"] = $"❌ Đã từ chối URL tố cáo: {scamUrl.Url}";
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, string type)
        {
            if (type == "post")
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
                    foreach (var image in scam.Images)
                    {
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", image.ImagePath.TrimStart('/'));
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
                    }

                    _context.ScamImages.RemoveRange(scam.Images);
                    _context.ScamPosts.Remove(scam);
                    await _context.SaveChangesAsync();

                    TempData["Message"] = $"🗑️ Đã xóa bài tố cáo: {scam.StkScam ?? scam.SdtScam ?? "không xác định"}";
                }
                catch (Exception ex)
                {
                    TempData["Message"] = $"❌ Lỗi khi xóa bài: {ex.Message}";
                }
            }
            else if (type == "url")
            {
                var scamUrl = await _context.ScamUrls
                    .FirstOrDefaultAsync(u => u.Id == id);
                if (scamUrl == null)
                {
                    TempData["Message"] = $"❌ Không tìm thấy URL tố cáo!";
                    return RedirectToAction("Index");
                }

                try
                {
                    _context.ScamUrls.Remove(scamUrl);
                    await _context.SaveChangesAsync();

                    TempData["Message"] = $"🗑️ Đã xóa URL tố cáo: {scamUrl.Url}";
                }
                catch (Exception ex)
                {
                    TempData["Message"] = $"❌ Lỗi khi xóa URL: {ex.Message}";
                }
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UrlDetail(int id)
        {
            var scamUrl = await _context.ScamUrls
                .FirstOrDefaultAsync(u => u.Id == id);
            if (scamUrl == null)
            {
                return NotFound();
            }
            return View(scamUrl);
        }
    }
}