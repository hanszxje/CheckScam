using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace CheckScam.Models
{
    public class CheckScamDbContext : IdentityDbContext<IdentityUser>
    {
        public CheckScamDbContext(DbContextOptions<CheckScamDbContext> options)
            : base(options)
        {
        }

        public DbSet<ScamPost> ScamPosts { get; set; }
        public DbSet<ScamImage> ScamImages { get; set; }
        public DbSet<ScamUrl> ScamUrls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seeding for ScamUrl (70 records, unchanged)
            modelBuilder.Entity<ScamUrl>().HasData(
                new ScamUrl { Id = 1, Url = "amazou2.com", NoiDung = "Giả mạo Amazon, có thể dụ nhập thông tin hoặc bán hàng giả.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 2, Url = "vnttiktok.cc", NoiDung = "Giả mạo TikTok, dụ người dùng nhập thông tin hoặc tải ứng dụng giả.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 3, Url = "napgamex5.com", NoiDung = "Liên quan đến giao dịch game, yêu cầu thanh toán trước.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 4, Url = "muacard.online", NoiDung = "Bán thẻ game/tài khoản, thường không giao hàng sau thanh toán.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 5, Url = "mall2024tn.com", NoiDung = "Giả mạo trung tâm thương mại, quảng cáo giảm giá nhưng không giao.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 6, Url = "doitacchienluoc.com", NoiDung = "Giả danh đối tác chiến lược của công ty lớn, có thể lừa đảo đầu tư.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 7, Url = "ebayglobal.store", NoiDung = "Giả mạo eBay, bán hàng giá rẻ, có thể không giao hoặc giao hàng giả.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 8, Url = "hhkkd3.com", NoiDung = "Tên miền không rõ ràng, có thể liên quan đến lừa đảo tài chính.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 9, Url = "hcmphattrien.com", NoiDung = "Giả danh công ty phát triển TP.HCM, có thể dụ đầu tư hoặc bán hàng giả.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 10, Url = "tuyendungapple.com", NoiDung = "Giả mạo tuyển dụng Apple, có thể thu thập thông tin cá nhân.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 11, Url = "sp6708p.com", NoiDung = "Tên miền không rõ ràng, có thể liên quan đến lừa đảo tài chính/game.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 12, Url = "ctygiaohangtietkiemm1.com", NoiDung = "Giả mạo Giao Hàng Tiết Kiệm, gửi link thanh toán giả qua SMS/email.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 13, Url = "tiktokvn1.shop", NoiDung = "Giả mạo TikTok Shop, dụ nhập thông tin hoặc thanh toán.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 14, Url = "ssiamvna.asia", NoiDung = "Tên miền không rõ ràng, có thể giả danh công ty đầu tư.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 15, Url = "ssiamvnx.asia", NoiDung = "Tương tự ssiamvna.asia, có thể liên quan đến lừa đảo đầu tư.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 16, Url = "vingroup.store", NoiDung = "Giả mạo Vingroup, bán sản phẩm/dịch vụ giả hoặc thu thập thông tin.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 17, Url = "vingroupventures.vip", NoiDung = "Giả mạo Vingroup, liên quan đến lừa đảo đầu tư.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 18, Url = "vingroupglobal.com", NoiDung = "Giả mạo Vingroup, có thể dụ đầu tư hoặc bán hàng giả.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 19, Url = "vingroupventures.top", NoiDung = "Giả mạo Vingroup, liên quan đến lừa đảo đầu tư.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 20, Url = "vingroupventures.site", NoiDung = "Giả mạo Vingroup, có thể dụ đầu tư hoặc thu thập thông tin.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 21, Url = "sforecast.com", NoiDung = "Tên miền không rõ ràng, có thể liên quan đến lừa đảo tài chính.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 22, Url = "ssiamvnm.asia", NoiDung = "Tương tự ssiamvna.asia, có thể giả danh công ty đầu tư.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 23, Url = "pt31.com", NoiDung = "Tên miền không rõ ràng, có thể liên quan đến lừa đảo tài chính/game.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 24, Url = "aguih.com", NoiDung = "Tên miền không rõ ràng, có thể liên quan đến lừa đảo quảng cáo.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 25, Url = "asean-sc.com", NoiDung = "Giả danh tổ chức ASEAN, có thể dụ đầu tư hoặc thu thập thông tin.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 26, Url = "lynstore.net", NoiDung = "Giả mạo cửa hàng trực tuyến, có thể không giao hàng hoặc bán hàng giả.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 27, Url = "stratestokex.vn", NoiDung = "Giả danh sàn giao dịch chứng khoán, có thể lừa đảo đầu tư.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 28, Url = "lotusfn.com", NoiDung = "Tên miền không rõ ràng, có thể liên quan đến lừa đảo tài chính.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 29, Url = "inkbio.me/hoanglongfn", NoiDung = "Liên kết phụ, có thể dẫn đến trang lừa đảo trên tên miền inkbio.me.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 30, Url = "metlife222.com", NoiDung = "Giả mạo MetLife, có thể dụ mua bảo hiểm hoặc thu thập thông tin.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 31, Url = "donkihote.xyz/app", NoiDung = "Giả mạo ứng dụng hoặc cửa hàng, có thể dụ tải phần mềm độc hại.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 32, Url = "playkuvip.club", NoiDung = "Liên quan đến nạp game, yêu cầu thanh toán trước nhưng không giao.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 33, Url = "inkbio.me/hoanglong.com", NoiDung = "Liên kết phụ, có thể dẫn đến trang lừa đảo trên tên miền inkbio.me.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 34, Url = "lotteryvietnamese.com/h5", NoiDung = "Giả mạo xổ số Việt Nam, dụ nhập thông tin hoặc thanh toán.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 35, Url = "bumvip68.club", NoiDung = "Liên quan đến nạp game, yêu cầu thanh toán trước nhưng không giao.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 36, Url = "ssiamvn.com", NoiDung = "Tên miền không rõ ràng, có thể giả danh công ty đầu tư.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 37, Url = "tikiz.vip", NoiDung = "Giả mạo dịch vụ hoặc ứng dụng, có thể dụ nhập thông tin.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 38, Url = "nasdaqvnn.com", NoiDung = "Giả mạo sàn Nasdaq, dụ đầu tư giả.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 39, Url = "pamm.fvptrade.com", NoiDung = "Hứa lợi nhuận cao qua đầu tư PAMM, có thể là lừa đảo tài chính.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 40, Url = "fund.almohannadigroup.com", NoiDung = "Giả danh quỹ đầu tư, dụ đầu tư giả.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 41, Url = "chanlemomo.win", NoiDung = "Giả mạo ví MoMo, đánh cắp thông tin tài khoản qua link giả.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 42, Url = "chanlemomo.cc", NoiDung = "Giả mạo ví MoMo, dụ nhập thông tin tài khoản.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 43, Url = "rewardsgiantusa.com", NoiDung = "Quảng cáo trên TikTok, dụ đăng ký nhận phần thưởng, thu thập thông tin.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 44, Url = "emmarelief.com", NoiDung = "Bán thực phẩm bổ sung Emma, Trustpilot chấm 2.0/5, 76% đánh giá tiêu cực.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 45, Url = "trendcraftleather.com", NoiDung = "Bán sản phẩm da giá rẻ, giao hàng giả hoặc không giao.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 46, Url = "tiffanycoshop.com", NoiDung = "Giả mạo Tiffany & Co., bán trang sức giá thấp, không liên kết chính thức.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 47, Url = "lords-mob.com", NoiDung = "Bán tài khoản/vật phẩm Lords Mobile, lừa tiền qua PayPal, không giao.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 48, Url = "paypa1.com", NoiDung = "Giả mạo PayPal, dụ nhập thông tin đăng nhập qua email giả mạo.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 49, Url = "paypaysecurity.com", NoiDung = "Giả mạo PayPal, đánh cắp thông tin đăng nhập qua email lừa đảo.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 50, Url = "onlinehokasale.com", NoiDung = "Giả mạo bán giày Hoka, tên miền mới, thiết kế cẩu thả.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 51, Url = "ok.gov-yiw.vip", NoiDung = "Giả mạo DMV Oklahoma, gửi link thanh toán phạt giao thông giả qua SMS.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 52, Url = "appstorevn.cc", NoiDung = "Giả mạo App Store Việt Nam, lừa người dùng cài ứng dụng giả mạo.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 53, Url = "baohiemxlife.com", NoiDung = "Giả danh công ty bảo hiểm, dụ người dùng đầu tư giả.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 54, Url = "grab-vn.app", NoiDung = "Giả mạo Grab Việt Nam, lừa nhập thông tin thanh toán.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 55, Url = "meta-vn.store", NoiDung = "Giả mạo Meta (Facebook), quảng cáo tuyển dụng giả mạo.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 56, Url = "thecaosieure.net", NoiDung = "Trang bán thẻ cào siêu rẻ, yêu cầu chuyển khoản trước rồi không giao.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 57, Url = "momo-vnpay.com", NoiDung = "Giả mạo liên kết MoMo - VNPAY, dụ người dùng nhập OTP.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 58, Url = "xoso68vn.me", NoiDung = "Giả mạo trang xổ số, thu thập dữ liệu cá nhân.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 59, Url = "vaytienonline365.store", NoiDung = "Giả danh dịch vụ cho vay online, lừa cung cấp thông tin cá nhân.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 60, Url = "evnthanhtoan.cc", NoiDung = "Giả mạo EVN (Điện lực), gửi hoá đơn giả qua email/SMS.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 61, Url = "viettelxacminh.com", NoiDung = "Giả danh Viettel, yêu cầu xác minh tài khoản qua link giả.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 62, Url = "tuyendung-shopee.net", NoiDung = "Giả mạo Shopee tuyển dụng, thu thập hồ sơ cá nhân.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 63, Url = "vnshopeex.com", NoiDung = "Giả mạo trang thương mại Shopee, yêu cầu thanh toán qua ví giả.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 64, Url = "myviettelverify.cc", NoiDung = "Giả mạo MyViettel, yêu cầu xác thực qua OTP.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 65, Url = "evnhanoi-pay.net", NoiDung = "Giả mạo EVN Hà Nội, gửi link thanh toán điện giả mạo.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 66, Url = "gachtheviettel.net", NoiDung = "Trang bán thẻ Viettel giả, yêu cầu chuyển khoản không hoàn lại.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 67, Url = "mobifonepay.site", NoiDung = "Giả mạo thanh toán Mobifone, đánh cắp thông tin thanh toán.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 68, Url = "vn-facebook.app", NoiDung = "Giả mạo Facebook Việt Nam, yêu cầu xác minh tài khoản.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 69, Url = "zalopay-event.com", NoiDung = "Giả danh sự kiện ZaloPay, lừa người dùng nhập OTP.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamUrl { Id = 70, Url = "lixi2025-event.net", NoiDung = "Giả mạo chương trình lì xì, yêu cầu nhập tài khoản ngân hàng.", Status = "pending", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) }
            );

            // Seeding for ScamPost (consolidated and cleaned)
            modelBuilder.Entity<ScamPost>().HasData(
                // Existing approved records (cleaned and assigned new IDs)
                new ScamPost { Id = 1, StkScam = "Không áp dụng", SdtScam = "84901234567", NoiDung = "Kẻ lừa đảo giả danh nhân viên ngân hàng gọi điện thoại thông báo tài khoản của nạn nhân gặp sự cố và yêu cầu cung cấp thông tin cá nhân, mật khẩu, mã OTP để 'xác minh' hoặc 'khóa tài khoản'.", Status = "approved", CreatedAt = new DateTime(2025, 3, 23, 10, 4, 47, DateTimeKind.Utc) },
                new ScamPost { Id = 2, StkScam = "Không có thông tin", SdtScam = null, NoiDung = "Đối tượng thông báo cho nạn nhân rằng họ đã trúng thưởng một giải thưởng giá trị. Để nhận thưởng, nạn nhân phải chuyển một khoản tiền phí hoặc cung cấp thông tin cá nhân. Sau khi nhận được tiền hoặc thông tin, đối tượng sẽ biến mất.", Status = "approved", CreatedAt = new DateTime(2025, 3, 23, 10, 4, 47, DateTimeKind.Utc) },
                new ScamPost { Id = 3, StkScam = "Nhiều số tài khoản khác nhau", SdtScam = null, NoiDung = "Đối tượng lừa đảo quảng cáo các dự án đầu tư tiền ảo với lợi nhuận cao, cam kết sinh lời nhanh chóng. Sau khi nhận tiền, chúng biến mất hoặc sàn giao dịch sập.", Status = "approved", CreatedAt = new DateTime(2025, 3, 23, 10, 4, 47, DateTimeKind.Utc) },
                new ScamPost { Id = 4, StkScam = "Không áp dụng", SdtScam = null, NoiDung = "Kẻ lừa đảo giả danh nhân viên ngân hàng yêu cầu nạn nhân cung cấp thông tin đăng nhập, mã OTP hoặc chuyển tiền để 'xác minh' tài khoản. Chúng dùng thông tin này để chiếm đoạt tiền.", Status = "approved", CreatedAt = new DateTime(2025, 3, 23, 10, 19, 47, DateTimeKind.Utc) },
                new ScamPost { Id = 5, StkScam = null, SdtScam = null, NoiDung = "Các đối tượng đăng tin tuyển dụng việc làm online với mức lương hấp dẫn, yêu cầu người lao động đóng phí hoặc đặt cọc trước khi bắt đầu công việc. Sau khi nhận tiền, chúng cắt đứt liên lạc.", Status = "approved", CreatedAt = new DateTime(2025, 3, 23, 10, 19, 47, DateTimeKind.Utc) },
                new ScamPost { Id = 6, StkScam = "Nhiều số tài khoản khác nhau", SdtScam = null, NoiDung = "Đối tượng lừa đảo sử dụng các trang web hoặc nhóm chat để quảng cáo các dự án đầu tư tài chính với lợi nhuận cao. Sau khi nhận tiền đầu tư, chúng biến mất hoặc ngừng trả lãi.", Status = "approved", CreatedAt = new DateTime(2025, 3, 23, 10, 21, 8, DateTimeKind.Utc) },
                new ScamPost { Id = 7, StkScam = null, SdtScam = "84388388099", NoiDung = "Đối tượng giả danh công an gọi điện thông báo nạn nhân liên quan đến một vụ án nghiêm trọng và yêu cầu chuyển tiền vào tài khoản để xác minh.", Status = "approved", CreatedAt = new DateTime(2025, 3, 24, 2, 0, 55, DateTimeKind.Utc) },
                new ScamPost { Id = 8, StkScam = "Nhiều số tài khoản khác nhau", SdtScam = null, NoiDung = "Đối tượng quảng cáo các sàn đầu tư tiền ảo, chứng khoán, ngoại hối với lợi nhuận cao bất thường. Sau khi nạn nhân đầu tư, đối tượng biến mất.", Status = "approved", CreatedAt = new DateTime(2025, 3, 24, 2, 1, 14, DateTimeKind.Utc) },
                new ScamPost { Id = 9, StkScam = "Không có thông tin", SdtScam = null, NoiDung = "Đối tượng giả danh nhân viên ngân hàng thông báo tài khoản của nạn nhân có vấn đề, yêu cầu cung cấp thông tin cá nhân, mã OTP để chiếm đoạt tiền.", Status = "approved", CreatedAt = new DateTime(2025, 3, 24, 2, 1, 37, DateTimeKind.Utc) },
                new ScamPost { Id = 10, StkScam = "1150080072 vcb", SdtScam = "901956846", NoiDung = "Lừa đảo giả mạo, yêu cầu chuyển tiền vào tài khoản ngân hàng.", Status = "approved", CreatedAt = new DateTime(2025, 3, 24, 7, 18, 46, DateTimeKind.Utc) },
                new ScamPost { Id = 11, StkScam = "Không có thông tin", SdtScam = "84395642", NoiDung = "Đối tượng giả danh nhân viên tổng đài xổ số thông báo trúng giải thưởng lớn và yêu cầu chuyển tiền phí, thuế để nhận thưởng.", Status = "approved", CreatedAt = new DateTime(2025, 3, 24, 7, 46, 13, DateTimeKind.Utc) },
                new ScamPost { Id = 12, StkScam = "Không có thông tin", SdtScam = "8424848", NoiDung = "Đối tượng mạo danh công an, viện kiểm sát thông báo nạn nhân liên quan đến vụ án nghiêm trọng và yêu cầu chuyển tiền để xác minh.", Status = "approved", CreatedAt = new DateTime(2025, 3, 24, 7, 46, 13, DateTimeKind.Utc) },
                new ScamPost { Id = 13, StkScam = null, SdtScam = null, NoiDung = "Đối tượng giả mạo nhân viên công ty xổ số thông báo trúng thưởng, yêu cầu nạn nhân chuyển khoản phí nhận thưởng hoặc thuế trước khi nhận giải.", Status = "approved", CreatedAt = new DateTime(2025, 3, 24, 7, 50, 26, DateTimeKind.Utc) },
                new ScamPost { Id = 14, StkScam = null, SdtScam = "84374602350", NoiDung = "Đối tượng giả danh công an thông báo nạn nhân liên quan đến một vụ án ma túy, rửa tiền và yêu cầu chuyển tiền để xác minh.", Status = "approved", CreatedAt = new DateTime(2025, 3, 24, 7, 50, 57, DateTimeKind.Utc) },
                new ScamPost { Id = 15, StkScam = "99006868xxxx", SdtScam = "583021", NoiDung = "Đối tượng lừa đảo qua nhóm 'tuyển cộng tác viên online', yêu cầu chuyển tiền để thực hiện nhiệm vụ và hứa trả hoa hồng, sau đó chiếm đoạt tiền.", Status = "approved", CreatedAt = new DateTime(2025, 3, 24, 7, 50, 57, DateTimeKind.Utc) },
                new ScamPost { Id = 16, StkScam = "19036281983012 (Techcombank)", SdtScam = null, NoiDung = "Đối tượng hack tài khoản mạng xã hội, nhắn tin cho bạn bè để vay tiền với lý do cấp bách, yêu cầu chuyển tiền vào tài khoản ngân hàng.", Status = "approved", CreatedAt = new DateTime(2025, 3, 24, 7, 50, 57, DateTimeKind.Utc) },

                // New ScamPost records (merged and cleaned)
                new ScamPost { Id = 17, SdtScam = "19003439", StkScam = null, NoiDung = "Thông báo trúng thưởng giả, yêu cầu đóng phí.", Status = "approved", CreatedAt = new DateTime(2025, 6, 20, 17, 46, 0, DateTimeKind.Utc) },
                new ScamPost { Id = 18, SdtScam = "0342867523", StkScam = null, NoiDung = "Lừa đảo qua mua bán hàng online, yêu cầu chuyển tiền đặt cọc.", Status = "approved", CreatedAt = new DateTime(2025, 6, 20, 17, 46, 0, DateTimeKind.Utc) },
                new ScamPost { Id = 19, SdtScam = "+8919008198", StkScam = null, NoiDung = "Số quốc tế giả mạo, yêu cầu cung cấp thông tin cá nhân.", Status = "approved", CreatedAt = new DateTime(2025, 6, 20, 17, 46, 0, DateTimeKind.Utc) },
                new ScamPost { Id = 20, SdtScam = "+224", StkScam = null, NoiDung = "Số từ Guinea, thường liên quan đến lừa đảo tài chính.", Status = "approved", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamPost { Id = 21, SdtScam = "+231", StkScam = null, NoiDung = "Số từ Liberia, thường giả danh tổ chức quốc tế.", Status = "approved", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamPost { Id = 22, SdtScam = "+252", StkScam = null, NoiDung = "Số từ Somalia, liên quan đến lừa đảo qua tin nhắn.", Status = "approved", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamPost { Id = 23, SdtScam = "+373", StkScam = null, NoiDung = "Số từ Moldova, lừa đảo qua cuộc gọi ngắn (Wangiri).", Status = "approved", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamPost { Id = 24, SdtScam = "+216", StkScam = null, NoiDung = "Số từ Tunisia, giả danh dịch vụ quốc tế.", Status = "approved", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamPost { Id = 25, SdtScam = "+240", StkScam = null, NoiDung = "Số từ Equatorial Guinea, lừa đảo tài chính.", Status = "approved", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamPost { Id = 26, SdtScam = "+226", StkScam = null, NoiDung = "Số từ Burkina Faso, lừa đảo qua cuộc gọi.", Status = "approved", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamPost { Id = 27, SdtScam = "02439446395", StkScam = null, NoiDung = "Số Hà Nội, giả danh ngân hàng hoặc cơ quan công.", Status = "approved", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamPost { Id = 28, SdtScam = "02822211234", StkScam = null, NoiDung = "Số TP.HCM, lừa đảo giao hàng giả mạo.", Status = "approved", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamPost { Id = 29, SdtScam = "1900123456", StkScam = null, NoiDung = "Số dịch vụ giả danh tổng đài hỗ trợ.", Status = "approved", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamPost { Id = 30, SdtScam = "02499950060", StkScam = null, NoiDung = "Số Hà Nội, mạo danh cơ quan chức năng.", Status = "approved", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamPost { Id = 31, SdtScam = "02899964439", StkScam = null, NoiDung = "Số TP.HCM, lừa đảo qua tin nhắn.", Status = "approved", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamPost { Id = 32, SdtScam = "2028610737", StkScam = null, NoiDung = "Số giả danh Đại sứ quán Việt Nam tại Mỹ.", Status = "approved", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamPost { Id = 33, SdtScam = "+855714749087", StkScam = null, NoiDung = "Số từ Campuchia, liên quan đến lừa đảo trực tuyến.", Status = "approved", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamPost { Id = 34, SdtScam = "+2471234567", StkScam = null, NoiDung = "Số có đầu không an toàn từ Ascension.", Status = "approved", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamPost { Id = 35, SdtScam = "+5631234567", StkScam = null, NoiDung = "Số có đầu không an toàn từ Valparaiso.", Status = "approved", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamPost { Id = 36, SdtScam = "+88212345678", StkScam = null, NoiDung = "Số có đầu không an toàn từ mạng quốc tế.", Status = "approved", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) },
                new ScamPost { Id = 37, SdtScam = "02888123456", StkScam = null, NoiDung = "Số TP.HCM giả danh Digitel.", Status = "approved", CreatedAt = new DateTime(2025, 6, 22, 22, 12, 0, DateTimeKind.Utc) }
            );
        }
    }
}