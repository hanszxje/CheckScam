using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using CheckScam.Models;
using CheckScam.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using System.Net.Http;
using System.Text;

namespace CheckScam.Controllers
{
    public class UrlScamController : Controller
    {
        private readonly CheckScamDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public UrlScamController(CheckScamDbContext context, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index(int? page, string q)
        {
            var scamUrls = _context.ScamUrls
                .Where(u => u.Status == "approved");

            if (!string.IsNullOrEmpty(q))
            {
                scamUrls = scamUrls.Where(u => u.Url.Contains(q));
            }

            int pageSize = 6;
            int pageNumber = page ?? 1;
            var paginatedUrls = await scamUrls
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.PageNumber = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling((double)await scamUrls.CountAsync() / pageSize);
            return View(paginatedUrls);
        }

        [Authorize]
        public IActionResult Report()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Report(PostScamUrlDto model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "❌ Dữ liệu không hợp lệ!";
                return View(model);
            }

            var scamUrl = new ScamUrl
            {
                Url = model.Url,
                NoiDung = model.NoiDung,
                Status = "pending"
            };
            _context.ScamUrls.Add(scamUrl);
            await _context.SaveChangesAsync();

            TempData["Success"] = "✅ Tố cáo URL đã được gửi và đang chờ duyệt!";
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CheckUrl(string url)
        {
            if (string.IsNullOrEmpty(url) || !Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                return Json(new { success = false, message = "URL không hợp lệ!" });
            }

            try
            {
                var client = _httpClientFactory.CreateClient();
                var requestBody = new
                {
                    client = new { clientId = "CheckScam", clientVersion = "1.0" },
                    threatInfo = new
                    {
                        threatTypes = new[] { "MALWARE", "SOCIAL_ENGINEERING" },
                        threatEntryTypes = new[] { "URL" },
                        threatEntries = new[] { new { url } }
                    }
                };
                var apiKey = _configuration["GoogleSafeBrowsingApiKey"];
                var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"https://safebrowsing.googleapis.com/v4/threatMatches:find?key={apiKey}", content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var matches = JsonSerializer.Deserialize<Dictionary<string, object>>(result);
                    bool isThreat = matches.ContainsKey("matches") && matches["matches"] != null;
                    return Json(new { success = true, isThreat, data = result });
                }
                return Json(new { success = false, message = "Không thể kiểm tra URL!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Lỗi hệ thống: {ex.Message}" });
            }
        }

        public async Task<IActionResult> Detail(int id)
        {
            var scamUrl = await _context.ScamUrls
                .FirstOrDefaultAsync(u => u.Id == id);
            if (scamUrl == null)
            {
                return NotFound();
            }
            return View(scamUrl);
        }

        [Authorize(Roles = "Superuser")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var scamUrl = await _context.ScamUrls
                .FirstOrDefaultAsync(u => u.Id == id);
            if (scamUrl == null)
            {
                TempData["Error"] = $"❌ Không tìm thấy URL tố cáo!";
                return RedirectToAction("Index");
            }

            try
            {
                _context.ScamUrls.Remove(scamUrl);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                TempData["Error"] = $"❌ Lỗi khi xóa URL: {ex.Message}";
            }

            return RedirectToAction("Index");
        }
    }
}