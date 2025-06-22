using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CheckScam.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScamPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StkScam = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SdtScam = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScamPosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScamUrls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScamUrls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScamImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScamPostId = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScamImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScamImages_ScamPosts_ScamPostId",
                        column: x => x.ScamPostId,
                        principalTable: "ScamPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ScamPosts",
                columns: new[] { "Id", "CreatedAt", "NoiDung", "SdtScam", "Status", "StkScam" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 23, 10, 4, 47, 0, DateTimeKind.Utc), "Kẻ lừa đảo giả danh nhân viên ngân hàng gọi điện thoại thông báo tài khoản của nạn nhân gặp sự cố và yêu cầu cung cấp thông tin cá nhân, mật khẩu, mã OTP để 'xác minh' hoặc 'khóa tài khoản'.", "84901234567", "approved", "Không áp dụng" },
                    { 2, new DateTime(2025, 3, 23, 10, 4, 47, 0, DateTimeKind.Utc), "Đối tượng thông báo cho nạn nhân rằng họ đã trúng thưởng một giải thưởng giá trị. Để nhận thưởng, nạn nhân phải chuyển một khoản tiền phí hoặc cung cấp thông tin cá nhân. Sau khi nhận được tiền hoặc thông tin, đối tượng sẽ biến mất.", null, "approved", "Không có thông tin" },
                    { 3, new DateTime(2025, 3, 23, 10, 4, 47, 0, DateTimeKind.Utc), "Đối tượng lừa đảo quảng cáo các dự án đầu tư tiền ảo với lợi nhuận cao, cam kết sinh lời nhanh chóng. Sau khi nhận tiền, chúng biến mất hoặc sàn giao dịch sập.", null, "approved", "Nhiều số tài khoản khác nhau" },
                    { 4, new DateTime(2025, 3, 23, 10, 19, 47, 0, DateTimeKind.Utc), "Kẻ lừa đảo giả danh nhân viên ngân hàng yêu cầu nạn nhân cung cấp thông tin đăng nhập, mã OTP hoặc chuyển tiền để 'xác minh' tài khoản. Chúng dùng thông tin này để chiếm đoạt tiền.", null, "approved", "Không áp dụng" },
                    { 5, new DateTime(2025, 3, 23, 10, 19, 47, 0, DateTimeKind.Utc), "Các đối tượng đăng tin tuyển dụng việc làm online với mức lương hấp dẫn, yêu cầu người lao động đóng phí hoặc đặt cọc trước khi bắt đầu công việc. Sau khi nhận tiền, chúng cắt đứt liên lạc.", null, "approved", null },
                    { 6, new DateTime(2025, 3, 23, 10, 21, 8, 0, DateTimeKind.Utc), "Đối tượng lừa đảo sử dụng các trang web hoặc nhóm chat để quảng cáo các dự án đầu tư tài chính với lợi nhuận cao. Sau khi nhận tiền đầu tư, chúng biến mất hoặc ngừng trả lãi.", null, "approved", "Nhiều số tài khoản khác nhau" },
                    { 7, new DateTime(2025, 3, 24, 2, 0, 55, 0, DateTimeKind.Utc), "Đối tượng giả danh công an gọi điện thông báo nạn nhân liên quan đến một vụ án nghiêm trọng và yêu cầu chuyển tiền vào tài khoản để xác minh.", "84388388099", "approved", null },
                    { 8, new DateTime(2025, 3, 24, 2, 1, 14, 0, DateTimeKind.Utc), "Đối tượng quảng cáo các sàn đầu tư tiền ảo, chứng khoán, ngoại hối với lợi nhuận cao bất thường. Sau khi nạn nhân đầu tư, đối tượng biến mất.", null, "approved", "Nhiều số tài khoản khác nhau" },
                    { 9, new DateTime(2025, 3, 24, 2, 1, 37, 0, DateTimeKind.Utc), "Đối tượng giả danh nhân viên ngân hàng thông báo tài khoản của nạn nhân có vấn đề, yêu cầu cung cấp thông tin cá nhân, mã OTP để chiếm đoạt tiền.", null, "approved", "Không có thông tin" },
                    { 10, new DateTime(2025, 3, 24, 7, 18, 46, 0, DateTimeKind.Utc), "Lừa đảo giả mạo, yêu cầu chuyển tiền vào tài khoản ngân hàng.", "901956846", "approved", "1150080072 vcb" },
                    { 11, new DateTime(2025, 3, 24, 7, 46, 13, 0, DateTimeKind.Utc), "Đối tượng giả danh nhân viên tổng đài xổ số thông báo trúng giải thưởng lớn và yêu cầu chuyển tiền phí, thuế để nhận thưởng.", "84395642", "approved", "Không có thông tin" },
                    { 12, new DateTime(2025, 3, 24, 7, 46, 13, 0, DateTimeKind.Utc), "Đối tượng mạo danh công an, viện kiểm sát thông báo nạn nhân liên quan đến vụ án nghiêm trọng và yêu cầu chuyển tiền để xác minh.", "8424848", "approved", "Không có thông tin" },
                    { 13, new DateTime(2025, 3, 24, 7, 50, 26, 0, DateTimeKind.Utc), "Đối tượng giả mạo nhân viên công ty xổ số thông báo trúng thưởng, yêu cầu nạn nhân chuyển khoản phí nhận thưởng hoặc thuế trước khi nhận giải.", null, "approved", null },
                    { 14, new DateTime(2025, 3, 24, 7, 50, 57, 0, DateTimeKind.Utc), "Đối tượng giả danh công an thông báo nạn nhân liên quan đến một vụ án ma túy, rửa tiền và yêu cầu chuyển tiền để xác minh.", "84374602350", "approved", null },
                    { 15, new DateTime(2025, 3, 24, 7, 50, 57, 0, DateTimeKind.Utc), "Đối tượng lừa đảo qua nhóm 'tuyển cộng tác viên online', yêu cầu chuyển tiền để thực hiện nhiệm vụ và hứa trả hoa hồng, sau đó chiếm đoạt tiền.", "583021", "approved", "99006868xxxx" },
                    { 16, new DateTime(2025, 3, 24, 7, 50, 57, 0, DateTimeKind.Utc), "Đối tượng hack tài khoản mạng xã hội, nhắn tin cho bạn bè để vay tiền với lý do cấp bách, yêu cầu chuyển tiền vào tài khoản ngân hàng.", null, "approved", "19036281983012 (Techcombank)" },
                    { 17, new DateTime(2025, 6, 20, 17, 46, 0, 0, DateTimeKind.Utc), "Thông báo trúng thưởng giả, yêu cầu đóng phí.", "19003439", "approved", null },
                    { 18, new DateTime(2025, 6, 20, 17, 46, 0, 0, DateTimeKind.Utc), "Lừa đảo qua mua bán hàng online, yêu cầu chuyển tiền đặt cọc.", "0342867523", "approved", null },
                    { 19, new DateTime(2025, 6, 20, 17, 46, 0, 0, DateTimeKind.Utc), "Số quốc tế giả mạo, yêu cầu cung cấp thông tin cá nhân.", "+8919008198", "approved", null },
                    { 20, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Số từ Guinea, thường liên quan đến lừa đảo tài chính.", "+224", "approved", null },
                    { 21, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Số từ Liberia, thường giả danh tổ chức quốc tế.", "+231", "approved", null },
                    { 22, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Số từ Somalia, liên quan đến lừa đảo qua tin nhắn.", "+252", "approved", null },
                    { 23, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Số từ Moldova, lừa đảo qua cuộc gọi ngắn (Wangiri).", "+373", "approved", null },
                    { 24, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Số từ Tunisia, giả danh dịch vụ quốc tế.", "+216", "approved", null },
                    { 25, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Số từ Equatorial Guinea, lừa đảo tài chính.", "+240", "approved", null },
                    { 26, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Số từ Burkina Faso, lừa đảo qua cuộc gọi.", "+226", "approved", null },
                    { 27, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Số Hà Nội, giả danh ngân hàng hoặc cơ quan công.", "02439446395", "approved", null },
                    { 28, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Số TP.HCM, lừa đảo giao hàng giả mạo.", "02822211234", "approved", null },
                    { 29, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Số dịch vụ giả danh tổng đài hỗ trợ.", "1900123456", "approved", null },
                    { 30, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Số Hà Nội, mạo danh cơ quan chức năng.", "02499950060", "approved", null },
                    { 31, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Số TP.HCM, lừa đảo qua tin nhắn.", "02899964439", "approved", null },
                    { 32, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Số giả danh Đại sứ quán Việt Nam tại Mỹ.", "2028610737", "approved", null },
                    { 33, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Số từ Campuchia, liên quan đến lừa đảo trực tuyến.", "+855714749087", "approved", null },
                    { 34, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Số có đầu không an toàn từ Ascension.", "+2471234567", "approved", null },
                    { 35, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Số có đầu không an toàn từ Valparaiso.", "+5631234567", "approved", null },
                    { 36, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Số có đầu không an toàn từ mạng quốc tế.", "+88212345678", "approved", null },
                    { 37, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Số TP.HCM giả danh Digitel.", "02888123456", "approved", null }
                });

            migrationBuilder.InsertData(
                table: "ScamUrls",
                columns: new[] { "Id", "CreatedAt", "NoiDung", "Status", "Url" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả mạo Amazon, có thể dụ nhập thông tin hoặc bán hàng giả.", "pending", "amazou2.com" },
                    { 2, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả mạo TikTok, dụ người dùng nhập thông tin hoặc tải ứng dụng giả.", "pending", "vnttiktok.cc" },
                    { 3, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Liên quan đến giao dịch game, yêu cầu thanh toán trước.", "pending", "napgamex5.com" },
                    { 4, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Bán thẻ game/tài khoản, thường không giao hàng sau thanh toán.", "pending", "muacard.online" },
                    { 5, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả mạo trung tâm thương mại, quảng cáo giảm giá nhưng không giao.", "pending", "mall2024tn.com" },
                    { 6, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả danh đối tác chiến lược của công ty lớn, có thể lừa đảo đầu tư.", "pending", "doitacchienluoc.com" },
                    { 7, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả mạo eBay, bán hàng giá rẻ, có thể không giao hoặc giao hàng giả.", "pending", "ebayglobal.store" },
                    { 8, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Tên miền không rõ ràng, có thể liên quan đến lừa đảo tài chính.", "pending", "hhkkd3.com" },
                    { 9, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả danh công ty phát triển TP.HCM, có thể dụ đầu tư hoặc bán hàng giả.", "pending", "hcmphattrien.com" },
                    { 10, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả mạo tuyển dụng Apple, có thể thu thập thông tin cá nhân.", "pending", "tuyendungapple.com" },
                    { 11, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Tên miền không rõ ràng, có thể liên quan đến lừa đảo tài chính/game.", "pending", "sp6708p.com" },
                    { 12, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả mạo Giao Hàng Tiết Kiệm, gửi link thanh toán giả qua SMS/email.", "pending", "ctygiaohangtietkiemm1.com" },
                    { 13, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả mạo TikTok Shop, dụ nhập thông tin hoặc thanh toán.", "pending", "tiktokvn1.shop" },
                    { 14, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Tên miền không rõ ràng, có thể giả danh công ty đầu tư.", "pending", "ssiamvna.asia" },
                    { 15, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Tương tự ssiamvna.asia, có thể liên quan đến lừa đảo đầu tư.", "pending", "ssiamvnx.asia" },
                    { 16, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả mạo Vingroup, bán sản phẩm/dịch vụ giả hoặc thu thập thông tin.", "pending", "vingroup.store" },
                    { 17, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả mạo Vingroup, liên quan đến lừa đảo đầu tư.", "pending", "vingroupventures.vip" },
                    { 18, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả mạo Vingroup, có thể dụ đầu tư hoặc bán hàng giả.", "pending", "vingroupglobal.com" },
                    { 19, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả mạo Vingroup, liên quan đến lừa đảo đầu tư.", "pending", "vingroupventures.top" },
                    { 20, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả mạo Vingroup, có thể dụ đầu tư hoặc thu thập thông tin.", "pending", "vingroupventures.site" },
                    { 21, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Tên miền không rõ ràng, có thể liên quan đến lừa đảo tài chính.", "pending", "sforecast.com" },
                    { 22, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Tương tự ssiamvna.asia, có thể giả danh công ty đầu tư.", "pending", "ssiamvnm.asia" },
                    { 23, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Tên miền không rõ ràng, có thể liên quan đến lừa đảo tài chính/game.", "pending", "pt31.com" },
                    { 24, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Tên miền không rõ ràng, có thể liên quan đến lừa đảo quảng cáo.", "pending", "aguih.com" },
                    { 25, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả danh tổ chức ASEAN, có thể dụ đầu tư hoặc thu thập thông tin.", "pending", "asean-sc.com" },
                    { 26, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả mạo cửa hàng trực tuyến, có thể không giao hàng hoặc bán hàng giả.", "pending", "lynstore.net" },
                    { 27, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả danh sàn giao dịch chứng khoán, có thể lừa đảo đầu tư.", "pending", "stratestokex.vn" },
                    { 28, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Tên miền không rõ ràng, có thể liên quan đến lừa đảo tài chính.", "pending", "lotusfn.com" },
                    { 29, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Liên kết phụ, có thể dẫn đến trang lừa đảo trên tên miền inkbio.me.", "pending", "inkbio.me/hoanglongfn" },
                    { 30, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả mạo MetLife, có thể dụ mua bảo hiểm hoặc thu thập thông tin.", "pending", "metlife222.com" },
                    { 31, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả mạo ứng dụng hoặc cửa hàng, có thể dụ tải phần mềm độc hại.", "pending", "donkihote.xyz/app" },
                    { 32, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Liên quan đến nạp game, yêu cầu thanh toán trước nhưng không giao.", "pending", "playkuvip.club" },
                    { 33, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Liên kết phụ, có thể dẫn đến trang lừa đảo trên tên miền inkbio.me.", "pending", "inkbio.me/hoanglong.com" },
                    { 34, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả mạo xổ số Việt Nam, dụ nhập thông tin hoặc thanh toán.", "pending", "lotteryvietnamese.com/h5" },
                    { 35, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Liên quan đến nạp game, yêu cầu thanh toán trước nhưng không giao.", "pending", "bumvip68.club" },
                    { 36, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Tên miền không rõ ràng, có thể giả danh công ty đầu tư.", "pending", "ssiamvn.com" },
                    { 37, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả mạo dịch vụ hoặc ứng dụng, có thể dụ nhập thông tin.", "pending", "tikiz.vip" },
                    { 38, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả mạo sàn Nasdaq, dụ đầu tư giả.", "pending", "nasdaqvnn.com" },
                    { 39, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Hứa lợi nhuận cao qua đầu tư PAMM, có thể là lừa đảo tài chính.", "pending", "pamm.fvptrade.com" },
                    { 40, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả danh quỹ đầu tư, dụ đầu tư giả.", "pending", "fund.almohannadigroup.com" },
                    { 41, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả mạo ví MoMo, đánh cắp thông tin tài khoản qua link giả.", "pending", "chanlemomo.win" },
                    { 42, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả mạo ví MoMo, dụ nhập thông tin tài khoản.", "pending", "chanlemomo.cc" },
                    { 43, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Quảng cáo trên TikTok, dụ đăng ký nhận phần thưởng, thu thập thông tin.", "pending", "rewardsgiantusa.com" },
                    { 44, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Bán thực phẩm bổ sung Emma, Trustpilot chấm 2.0/5, 76% đánh giá tiêu cực.", "pending", "emmarelief.com" },
                    { 45, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Bán sản phẩm da giá rẻ, giao hàng giả hoặc không giao.", "pending", "trendcraftleather.com" },
                    { 46, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả mạo Tiffany & Co., bán trang sức giá thấp, không liên kết chính thức.", "pending", "tiffanycoshop.com" },
                    { 47, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Bán tài khoản/vật phẩm Lords Mobile, lừa tiền qua PayPal, không giao.", "pending", "lords-mob.com" },
                    { 48, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả mạo PayPal, dụ nhập thông tin đăng nhập qua email giả mạo.", "pending", "paypa1.com" },
                    { 49, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả mạo PayPal, đánh cắp thông tin đăng nhập qua email lừa đảo.", "pending", "paypaysecurity.com" },
                    { 50, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả mạo bán giày Hoka, tên miền mới, thiết kế cẩu thả.", "pending", "onlinehokasale.com" },
                    { 51, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả mạo DMV Oklahoma, gửi link thanh toán phạt giao thông giả qua SMS.", "pending", "ok.gov-yiw.vip" },
                    { 52, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả mạo App Store Việt Nam, lừa người dùng cài ứng dụng giả mạo.", "pending", "appstorevn.cc" },
                    { 53, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả danh công ty bảo hiểm, dụ người dùng đầu tư giả.", "pending", "baohiemxlife.com" },
                    { 54, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả mạo Grab Việt Nam, lừa nhập thông tin thanh toán.", "pending", "grab-vn.app" },
                    { 55, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả mạo Meta (Facebook), quảng cáo tuyển dụng giả mạo.", "pending", "meta-vn.store" },
                    { 56, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Trang bán thẻ cào siêu rẻ, yêu cầu chuyển khoản trước rồi không giao.", "pending", "thecaosieure.net" },
                    { 57, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả mạo liên kết MoMo - VNPAY, dụ người dùng nhập OTP.", "pending", "momo-vnpay.com" },
                    { 58, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả mạo trang xổ số, thu thập dữ liệu cá nhân.", "pending", "xoso68vn.me" },
                    { 59, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả danh dịch vụ cho vay online, lừa cung cấp thông tin cá nhân.", "pending", "vaytienonline365.store" },
                    { 60, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả mạo EVN (Điện lực), gửi hoá đơn giả qua email/SMS.", "pending", "evnthanhtoan.cc" },
                    { 61, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả danh Viettel, yêu cầu xác minh tài khoản qua link giả.", "pending", "viettelxacminh.com" },
                    { 62, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả mạo Shopee tuyển dụng, thu thập hồ sơ cá nhân.", "pending", "tuyendung-shopee.net" },
                    { 63, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả mạo trang thương mại Shopee, yêu cầu thanh toán qua ví giả.", "pending", "vnshopeex.com" },
                    { 64, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả mạo MyViettel, yêu cầu xác thực qua OTP.", "pending", "myviettelverify.cc" },
                    { 65, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả mạo EVN Hà Nội, gửi link thanh toán điện giả mạo.", "pending", "evnhanoi-pay.net" },
                    { 66, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Trang bán thẻ Viettel giả, yêu cầu chuyển khoản không hoàn lại.", "pending", "gachtheviettel.net" },
                    { 67, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả mạo thanh toán Mobifone, đánh cắp thông tin thanh toán.", "pending", "mobifonepay.site" },
                    { 68, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả mạo Facebook Việt Nam, yêu cầu xác minh tài khoản.", "pending", "vn-facebook.app" },
                    { 69, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả danh sự kiện ZaloPay, lừa người dùng nhập OTP.", "pending", "zalopay-event.com" },
                    { 70, new DateTime(2025, 6, 22, 22, 12, 0, 0, DateTimeKind.Utc), "Giả mạo chương trình lì xì, yêu cầu nhập tài khoản ngân hàng.", "pending", "lixi2025-event.net" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ScamImages_ScamPostId",
                table: "ScamImages",
                column: "ScamPostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ScamImages");

            migrationBuilder.DropTable(
                name: "ScamUrls");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ScamPosts");
        }
    }
}
