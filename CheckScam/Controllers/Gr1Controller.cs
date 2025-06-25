using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CheckScam.Models;
using CheckScam.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Text.RegularExpressions;
using System.Text.Json;
using System.Net.Http;
using System.IO;
using System.Security.Claims;

namespace CheckScam.Controllers
{
    public class Gr1Controller : Controller
    {
        private readonly CheckScamDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public Gr1Controller(CheckScamDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index(int? pagePhone, int? pageUrl, string q, string tab = "phone")
        {
            // Validate tab parameter
            tab = (tab == "url") ? "url" : "phone";

            var scamPosts = _context.ScamPosts
                .Where(p => p.Status == "approved");

            var scamUrls = _context.ScamUrls
                .Where(u => u.Status == "approved");

            if (!string.IsNullOrEmpty(q))
            {
                scamPosts = scamPosts.Where(p => p.StkScam.Contains(q) || p.SdtScam.Contains(q));
                scamUrls = scamUrls.Where(u => u.Url.Contains(q));
            }

            int pageSize = 6;
            int pageNumberPhone = pagePhone ?? 1;
            int pageNumberUrl = pageUrl ?? 1;

            var paginatedPosts = await scamPosts
                .Include(p => p.Images)
                .Skip((pageNumberPhone - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var paginatedUrls = await scamUrls
                .Skip((pageNumberUrl - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.PageNumberPhone = pageNumberPhone;
            ViewBag.PageNumberUrl = pageNumberUrl;
            ViewBag.TotalPagesPhone = (int)Math.Ceiling((double)await scamPosts.CountAsync() / pageSize);
            ViewBag.TotalPagesUrl = (int)Math.Ceiling((double)await scamUrls.CountAsync() / pageSize);
            ViewBag.ScamPosts = paginatedPosts;
            ViewBag.ScamUrls = paginatedUrls;
            ViewBag.ActiveTab = tab;
            ViewBag.Query = q;
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, false, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Error = "Sai tài khoản hoặc mật khẩu!";
            return View();
        }

        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Gr1", new { ReturnUrl = returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (remoteError != null)
            {
                TempData["Error"] = $"Lỗi từ Google: {remoteError}";
                return RedirectToAction("Login");
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                TempData["Error"] = "Lỗi khi lấy thông tin đăng nhập từ Google.";
                return RedirectToAction("Login");
            }

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                TempData["Success"] = "Đăng nhập bằng Google thành công!";
                return RedirectToAction("Index");
            }

            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            var username = email;

            var user = new IdentityUser { UserName = username, Email = email };
            var createResult = await _userManager.CreateAsync(user);
            if (createResult.Succeeded)
            {
                var addLoginResult = await _userManager.AddLoginAsync(user, info);
                if (addLoginResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    TempData["Success"] = "Đăng nhập bằng Google và tạo tài khoản thành công!";
                    return RedirectToAction("Index");
                }
            }

            TempData["Error"] = "Lỗi khi tạo tài khoản từ Google.";
            return RedirectToAction("Login");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string email, string password1, string password2)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password1) || string.IsNullOrEmpty(password2))
            {
                TempData["Error"] = "❌ Vui lòng điền đầy đủ thông tin!";
                return RedirectToAction("Register");
            }

            if (password1 != password2)
            {
                TempData["Error"] = "❌ Mật khẩu không khớp!";
                return RedirectToAction("Register");
            }

            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                TempData["Error"] = "❌ Email không hợp lệ!";
                return RedirectToAction("Register");
            }

            var existingUser = await _userManager.FindByNameAsync(username);
            if (existingUser != null)
            {
                TempData["Error"] = "❌ Tên tài khoản đã tồn tại!";
                return RedirectToAction("Register");
            }

            existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser != null)
            {
                TempData["Error"] = "❌ Email đã được sử dụng!";
                return RedirectToAction("Register");
            }

            var user = new IdentityUser { UserName = username, Email = email };
            try
            {
                var result = await _userManager.CreateAsync(user, password1);
                if (result.Succeeded)
                {
                    if (username.ToLower() == "admin")
                    {
                        await _userManager.AddToRoleAsync(user, "Superuser");
                    }
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    TempData["Success"] = "✅ Đăng ký thành công!";
                    return RedirectToAction("Index");
                }

                TempData["Error"] = string.Join("; ", result.Errors.Select(e => e.Description));
                return RedirectToAction("Register");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"❌ Lỗi hệ thống: {ex.Message}";
                return RedirectToAction("Register");
            }
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ScamDetail(int id)
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

        [Authorize]
        public IActionResult ReportPhone()
        {
            return View("Report");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ReportPhone(PostScamDto model, List<IFormFile> images)
        {
            Console.WriteLine($"Report SĐT called - model: {model?.StkScam}, {model?.SdtScam}, {model?.NoiDung}, images count: {images?.Count}");

            if (!ModelState.IsValid || string.IsNullOrEmpty(model.NoiDung))
            {
                TempData["Error"] = "❌ Vui lòng điền đầy đủ nội dung tố cáo!";
                return View("Report", model);
            }

            var scamPost = new ScamPost
            {
                StkScam = model.StkScam,
                SdtScam = model.SdtScam,
                NoiDung = model.NoiDung,
                Status = "pending"
            };
            _context.ScamPosts.Add(scamPost);
            try
            {
                await _context.SaveChangesAsync();
                Console.WriteLine($"ScamPost saved with Id: {scamPost.Id}");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"❌ Lỗi lưu database: {ex.Message} - StackTrace: {ex.StackTrace}";
                return View("Report", model);
            }

            if (images != null && images.Any())
            {
                foreach (var image in images)
                {
                    if (image != null && image.Length > 0)
                    {
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "media", "scam_images", fileName);
                        try
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await image.CopyToAsync(stream);
                            }
                            _context.ScamImages.Add(new ScamImage
                            {
                                ScamPostId = scamPost.Id,
                                ImagePath = $"/media/scam_images/{fileName}"
                            });
                            await _context.SaveChangesAsync();
                            Console.WriteLine($"Image saved at: {filePath}");
                        }
                        catch (Exception ex)
                        {
                            TempData["Error"] = $"❌ Lỗi lưu ảnh: {ex.Message} - StackTrace: {ex.StackTrace}";
                            return View("Report", model);
                        }
                    }
                }
            }

            TempData["Success"] = "✅ Tố cáo SĐT đã được gửi và đang chờ duyệt!";
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult ReportUrl()
        {
            return View("ReportUrl");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ReportUrl(PostScamUrlDto model)
        {
            Console.WriteLine($"Report URL called - model: {model?.Url}, {model?.NoiDung}");

            if (!ModelState.IsValid || string.IsNullOrEmpty(model.Url) || string.IsNullOrEmpty(model.NoiDung))
            {
                TempData["Error"] = "❌ Vui lòng điền đầy đủ URL và nội dung tố cáo!";
                return View("ReportUrl", model);
            }

            if (!Uri.IsWellFormedUriString(model.Url, UriKind.Absolute))
            {
                TempData["Error"] = "❌ URL không hợp lệ!";
                return View("ReportUrl", model);
            }

            var scamUrl = new ScamUrl
            {
                Url = model.Url,
                NoiDung = model.NoiDung,
                Status = "pending"
            };
            _context.ScamUrls.Add(scamUrl);
            try
            {
                await _context.SaveChangesAsync();
                Console.WriteLine($"ScamUrl saved with Id: {scamUrl.Id}");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"❌ Lỗi lưu database: {ex.Message} - StackTrace: {ex.StackTrace}";
                return View("ReportUrl", model);
            }

            TempData["Success"] = "✅ Tố cáo URL đã được gửi và đang chờ duyệt!";
            return RedirectToAction("Index");
        }

        [Authorize]
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

        [Authorize(Roles = "Superuser")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var scam = await _context.ScamPosts
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (scam == null)
            {
                TempData["Error"] = $"❌ Không tìm thấy bài tố cáo!";
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

                TempData["Success"] = $"🗑️ Đã xóa bài tố cáo: {scam.StkScam ?? scam.SdtScam ?? "không xác định"}";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"❌ Lỗi khi xóa bài: {ex.Message}";
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Superuser")]
        [HttpPost]
        public async Task<IActionResult> DeleteUrl(int id)
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
                TempData["Success"] = $"🗑️ Đã xóa URL tố cáo: {scamUrl.Url}";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"❌ Lỗi khi xóa URL: {ex.Message}";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("fetch-gemini-scams")]
        public async Task<IActionResult> FetchGeminiScams()
        {
            var prompt = @"{
                ""prompt"": ""Hãy trả về JSON với danh sách 3 vụ lừa đảo phổ biến. Dữ liệu phải là JSON hợp lệ với format: [
                    {""bank_account"": ""Số tài khoản"", ""phone_number"": ""Số điện thoại"", ""description"": ""Chi tiết vụ lừa đảo""},
                    {""bank_account"": ""..."", ""phone_number"": ""..."", ""description"": ""...""},
                    {""bank_account"": ""..."", ""phone_number"": ""..."", ""description"": ""...""}
                ] Chỉ trả về JSON, không kèm theo văn bản giải thích khác. Lưu ý: lấy thông tin thật, không phải ví dụ, hãy lấy các bài có số điện thoại hoặc số tài khoản ngân hàng (1 trong 2 hoặc cả 2), các bài không có 1 trong 2 thứ đó không lấy, lấy từ các trang báo như https://vnexpress.net, https://tuoitre.vn, https://thanhnien.vn, https://dantri.com.vn, https://vietnamnet.vn, https://zingnews.vn, https://nhandan.vn, https://laodong.vn, https://kenh14.vn, https://plo.vn.""
            }";

            try
            {
                var client = _httpClientFactory.CreateClient();
                var content = new StringContent(prompt, System.Text.Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://api.gemini.ai/v1/generate", content);
                var responseString = await response.Content.ReadAsStringAsync();
                var scamData = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(responseString);

                foreach (var item in scamData)
                {
                    var sdtScam = Regex.Replace(item["phone_number"] ?? "", @"\D", "");
                    sdtScam = string.IsNullOrEmpty(sdtScam) || sdtScam.ToLower() == "không có" ? null : sdtScam;
                    var existingPost = await _context.ScamPosts
                        .FirstOrDefaultAsync(p => p.StkScam == item["bank_account"] && p.SdtScam == sdtScam);
                    if (existingPost != null)
                    {
                        existingPost.StkScam = item["bank_account"];
                        existingPost.SdtScam = sdtScam;
                        existingPost.NoiDung = item["description"];
                        existingPost.Status = "approved";
                    }
                    else
                    {
                        _context.ScamPosts.Add(new ScamPost
                        {
                            StkScam = item["bank_account"],
                            SdtScam = sdtScam,
                            NoiDung = item["description"],
                            Status = "approved"
                        });
                    }
                }
                await _context.SaveChangesAsync();
                return Json(new { message = "Dữ liệu lừa đảo Gemini đã được thêm vào!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/scamposts")]
        public async Task<IActionResult> GetAllScamPosts()
        {
            var scamPosts = await _context.ScamPosts
                .Select(p => new GetAllPostDto
                {
                    Id = p.Id,
                    StkScam = p.StkScam,
                    SdtScam = p.SdtScam,
                    NoiDung = p.NoiDung,
                    Status = p.Status
                })
                .ToListAsync();
            return Ok(scamPosts);
        }

        [HttpPost]
        [Route("api/scamposts")]
        public async Task<IActionResult> PostScamPost([FromBody] PostScamDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { error = "Dữ liệu không hợp lệ" });
            }

            var scamPost = new ScamPost
            {
                StkScam = model.StkScam,
                SdtScam = model.SdtScam,
                NoiDung = model.NoiDung,
                Status = "pending"
            };
            _context.ScamPosts.Add(scamPost);
            await _context.SaveChangesAsync();
            return StatusCode(201, new { id = scamPost.Id });
        }

        public IActionResult Instructions()
        {
            return View();
        }

        public IActionResult CheckSdt()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CheckPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return Json(new { success = false, message = "Vui lòng nhập số điện thoại!" });
            }

            string normalizedPhone = phoneNumber.StartsWith("0") ? "+84" + phoneNumber.Substring(1) : phoneNumber;

            try
            {
                var client = _httpClientFactory.CreateClient();
                var apiKey = _configuration["NumverifyApiKey"];
                var response = await client.GetAsync($"http://apilayer.net/api/validate?access_key={apiKey}&number={normalizedPhone}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var phoneData = JsonSerializer.Deserialize<Dictionary<string, object>>(result);

                    if (phoneData.ContainsKey("number"))
                    {
                        phoneData["number"] = "+84" + phoneData["number"].ToString().Replace("+84", "");
                    }

                    return Json(new
                    {
                        success = true,
                        data = phoneData
                    });
                }

                return Json(new { success = false, message = "Không thể kiểm tra số điện thoại!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Lỗi hệ thống: {ex.Message}" });
            }
        }
    }
}