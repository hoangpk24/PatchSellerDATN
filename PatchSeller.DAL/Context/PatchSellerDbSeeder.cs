using Microsoft.EntityFrameworkCore;
using PatchSeller.DAL.Models;

namespace PatchSeller.DAL.Context;

public static class PatchSellerDbSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        // NOTE:
        // - All values must be deterministic (no DateTime.Now) for EF Core HasData.
        // - Non-nullable reference properties (Nullable enabled) must be populated.

        modelBuilder.Entity<Publisher>().HasData(
            new Publisher
            {
                PublisherId = 1,
                Name = "Electronic Art",
                Description = "Nhà phát hành lớn (EA). Nổi tiếng với Battlefield, FIFA, Apex Legends.",
                Status = 1,
                Delete = false
            },
            new Publisher
            {
                PublisherId = 2,
                Name = "Riot Games",
                Description = "Nhà phát hành game eSports: League of Legends, VALORANT.",
                Status = 1,
                Delete = false
            },
            new Publisher
            {
                PublisherId = 3,
                Name = "Activision",
                Description = "Nhà phát hành Call of Duty (MW/Warzone) và nhiều series FPS.",
                Status = 1,
                Delete = false
            },
            new Publisher
            {
                PublisherId = 4,
                Name = "Ubisoft",
                Description = "Nhà phát hành nổi tiếng với Rainbow Six, Assassin's Creed, Far Cry.",
                Status = 1,
                Delete = false
            },
            new Publisher
            {
                PublisherId = 5,
                Name = "Valve",
                Description = "Nhà phát hành CS2, Dota 2; tập trung hệ sinh thái Steam.",
                Status = 1,
                Delete = false
            },
            new Publisher
            {
                PublisherId = 6,
                Name = "Epic Games",
                Description = "Nhà phát hành Fortnite; nền tảng Epic Games Store.",
                Status = 1,
                Delete = false
            }
        );

        modelBuilder.Entity<Category>().HasData(
            new Category
            {
                CategoryId = 1,
                CategoryName = "FPS",
                Description = "Bắn súng góc nhìn thứ nhất.",
                Status = 1,
                Delete = false
            },
            new Category
            {
                CategoryId = 2,
                CategoryName = "Battle Royale",
                Description = "Sinh tồn, vòng bo, loot đồ.",
                Status = 1,
                Delete = false
            },
            new Category
            {
                CategoryId = 3,
                CategoryName = "MOBA",
                Description = "Chiến thuật 5v5, phối hợp đội hình.",
                Status = 1,
                Delete = false
            },
            new Category
            {
                CategoryId = 4,
                CategoryName = "Tactical Shooter",
                Description = "Bắn súng chiến thuật, đặt/giải bom, kỹ năng cá nhân + phối hợp.",
                Status = 1,
                Delete = false
            },
            new Category
            {
                CategoryId = 5,
                CategoryName = "Hành động",
                Description = "Nhịp nhanh, thiên về chiến đấu.",
                Status = 1,
                Delete = false
            },
            new Category
            {
                CategoryId = 6,
                CategoryName = "eSports",
                Description = "Tập trung thi đấu xếp hạng/giải đấu.",
                Status = 1,
                Delete = false
            },
            new Category
            {
                CategoryId = 7,
                CategoryName = "Co-op",
                Description = "Chơi hợp tác cùng bạn bè.",
                Status = 1,
                Delete = false
            }
        );

        modelBuilder.Entity<Platform>().HasData(
            new Platform
            {
                PlatformId = 1,
                Name = "PC",
                Description = "Windows (Steam/Battle.net/Epic).",
                Status = 1,
                Delete = false
            },
            new Platform
            {
                PlatformId = 2,
                Name = "PlayStation 5",
                Description = "PS5/PSN.",
                Status = 1,
                Delete = false
            },
            new Platform
            {
                PlatformId = 3,
                Name = "Xbox Series X|S",
                Description = "Xbox Series X/S.",
                Status = 1,
                Delete = false
            },
            new Platform
            {
                PlatformId = 4,
                Name = "Nintendo Switch",
                Description = "Switch/handheld.",
                Status = 1,
                Delete = false
            }
        );

        modelBuilder.Entity<Rank>().HasData(
            new Rank
            {
                RankId = 1,
                RankName = "Đồng",
                MiniumSpend = 0,
                Description = "Mặc định. Tích điểm cơ bản, nhận voucher tân thủ theo sự kiện.",
                Status = 1,
                Delete = false
            },
            new Rank
            {
                RankId = 2,
                RankName = "Bạc",
                MiniumSpend = 500_000,
                Description = "Ưu đãi tốt hơn, hỗ trợ nhanh hơn, voucher theo rank.",
                Status = 1,
                Delete = false
            },
            new Rank
            {
                RankId = 3,
                RankName = "Vàng",
                MiniumSpend = 2_000_000,
                Description = "Ưu đãi cao nhất, ưu tiên hỗ trợ, tham gia test patch sớm.",
                Status = 1,
                Delete = false
            }
        );

        modelBuilder.Entity<Staff>().HasData(
            new Staff
            {
                StaffId = 1,
                UserName = "admin",
                PasswordHash = "dev-hash:admin@123",
                Email = "admin@patchseller.local",
                Role = "Admin",
                PhoneNumber = "0987675845",
                FullName = "Ass Min"
            },
            new Staff
            {
                StaffId = 2,
                UserName = "editor",
                PasswordHash = "dev-hash:editor@123",
                Email = "editor@patchseller.local",
                Role = "Editor",
                PhoneNumber = "0987675866",
                FullName = "E Đít Tơ"
            }
        );

        modelBuilder.Entity<Game>().HasData(
            new Game
            {
                GameId = 1,
                Title = "Battlefield 2042",
                Developer = "DICE",
                Description = "FPS quy mô lớn với chế độ All-Out Warfare. Tập trung chiến trường rộng, phương tiện và teamplay.",
                Thumbnail = "/seed/games/bf2042-thumb.jpg",
                ReleaseDate = new DateTime(2021, 11, 19),
                Status = 1,
                Delete = false,
                PublisherId = 1
            },
            new Game
            {
                GameId = 2,
                Title = "Battlefield V",
                Developer = "DICE",
                Description = "FPS bối cảnh Thế chiến II, nhịp độ nhanh, gunplay tốt và yêu cầu phối hợp tổ đội.",
                Thumbnail = "/seed/games/bfv-thumb.jpg",
                ReleaseDate = new DateTime(2018, 11, 20),
                Status = 1,
                Delete = false,
                PublisherId = 1
            },
            new Game
            {
                GameId = 3,
                Title = "Call of Duty: Modern Warfare II",
                Developer = "Infinity Ward",
                Description = "FPS hiện đại, campaign + multiplayer. TTK nhanh, nhiều chế độ cạnh tranh.",
                Thumbnail = "/seed/games/cod-mw2-thumb.jpg",
                ReleaseDate = new DateTime(2022, 10, 28),
                Status = 1,
                Delete = false,
                PublisherId = 3
            },
            new Game
            {
                GameId = 4,
                Title = "Call of Duty: Warzone",
                Developer = "Raven Software",
                Description = "Battle Royale nhịp nhanh, gunplay COD, meta thay đổi theo mùa.",
                Thumbnail = "/seed/games/warzone-thumb.jpg",
                ReleaseDate = new DateTime(2020, 3, 10),
                Status = 1,
                Delete = false,
                PublisherId = 3
            },
            new Game
            {
                GameId = 5,
                Title = "VALORANT",
                Developer = "Riot Games",
                Description = "Tactical shooter 5v5, agent có kỹ năng, ưu tiên crosshair placement và chiến thuật.",
                Thumbnail = "/seed/games/valorant-thumb.jpg",
                ReleaseDate = new DateTime(2020, 6, 2),
                Status = 1,
                Delete = false,
                PublisherId = 2
            },
            new Game
            {
                GameId = 6,
                Title = "League of Legends",
                Developer = "Riot Games",
                Description = "MOBA 5v5 kinh điển, hệ thống rank đa dạng, meta xoay theo patch.",
                Thumbnail = "/seed/games/lol-thumb.jpg",
                ReleaseDate = new DateTime(2009, 10, 27),
                Status = 1,
                Delete = false,
                PublisherId = 2
            },
            new Game
            {
                GameId = 7,
                Title = "Tom Clancy's Rainbow Six Siege",
                Developer = "Ubisoft Montreal",
                Description = "Bắn súng chiến thuật theo round, phá hủy môi trường, cần phối hợp team và map knowledge.",
                Thumbnail = "/seed/games/r6s-thumb.jpg",
                ReleaseDate = new DateTime(2015, 12, 1),
                Status = 1,
                Delete = false,
                PublisherId = 4
            },
            new Game
            {
                GameId = 8,
                Title = "Counter-Strike 2",
                Developer = "Valve",
                Description = "FPS eSports 5v5, đặt/giải bom. Tối ưu aim, utility và teamwork.",
                Thumbnail = "/seed/games/cs2-thumb.jpg",
                ReleaseDate = new DateTime(2023, 9, 27),
                Status = 1,
                Delete = false,
                PublisherId = 5
            },
            new Game
            {
                GameId = 9,
                Title = "Fortnite",
                Developer = "Epic Games",
                Description = "Battle Royale với cơ chế xây dựng/không xây dựng, sự kiện theo mùa và collab liên tục.",
                Thumbnail = "/seed/games/fortnite-thumb.jpg",
                ReleaseDate = new DateTime(2017, 7, 25),
                Status = 1,
                Delete = false,
                PublisherId = 6
            }
        );

        modelBuilder.Entity<GameCategory>().HasData(
            // Battlefield
            new GameCategory { GameCategoryId = 1, GameId = 1, CategoryId = 1, Delete = false },
            new GameCategory { GameCategoryId = 2, GameId = 1, CategoryId = 5, Delete = false },
            new GameCategory { GameCategoryId = 3, GameId = 2, CategoryId = 1, Delete = false },
            new GameCategory { GameCategoryId = 4, GameId = 2, CategoryId = 5, Delete = false },

            // Call of Duty
            new GameCategory { GameCategoryId = 5, GameId = 3, CategoryId = 1, Delete = false },
            new GameCategory { GameCategoryId = 6, GameId = 3, CategoryId = 6, Delete = false },
            new GameCategory { GameCategoryId = 7, GameId = 4, CategoryId = 2, Delete = false },
            new GameCategory { GameCategoryId = 8, GameId = 4, CategoryId = 1, Delete = false },

            // Riot
            new GameCategory { GameCategoryId = 9, GameId = 5, CategoryId = 4, Delete = false },
            new GameCategory { GameCategoryId = 10, GameId = 5, CategoryId = 6, Delete = false },
            new GameCategory { GameCategoryId = 11, GameId = 6, CategoryId = 3, Delete = false },
            new GameCategory { GameCategoryId = 12, GameId = 6, CategoryId = 6, Delete = false },

            // Ubisoft
            new GameCategory { GameCategoryId = 13, GameId = 7, CategoryId = 4, Delete = false },
            new GameCategory { GameCategoryId = 14, GameId = 7, CategoryId = 6, Delete = false },

            // Valve
            new GameCategory { GameCategoryId = 15, GameId = 8, CategoryId = 1, Delete = false },
            new GameCategory { GameCategoryId = 16, GameId = 8, CategoryId = 6, Delete = false },

            // Epic
            new GameCategory { GameCategoryId = 17, GameId = 9, CategoryId = 2, Delete = false },
            new GameCategory { GameCategoryId = 18, GameId = 9, CategoryId = 6, Delete = false }
        );

        modelBuilder.Entity<GamePlatform>().HasData(
            // Battlefield
            new GamePlatform
            {
                GamePlatformId = 1,
                GameId = 1,
                PlatformId = 1,
                Description = "PC (EA App/Steam) - khuyến nghị SSD, RAM 16GB.",
                Status = 1,
                Delete = false
            },
            new GamePlatform { GamePlatformId = 2, GameId = 1, PlatformId = 2, Description = "PS5 - 120Hz tùy màn hình.", Status = 1, Delete = false },
            new GamePlatform { GamePlatformId = 3, GameId = 1, PlatformId = 3, Description = "Xbox Series X|S - hỗ trợ cross-play.", Status = 1, Delete = false },

            new GamePlatform { GamePlatformId = 4, GameId = 2, PlatformId = 1, Description = "PC (Origin/Steam).", Status = 1, Delete = false },
            new GamePlatform { GamePlatformId = 5, GameId = 2, PlatformId = 2, Description = "PS5/PS4 (tương thích).", Status = 1, Delete = false },
            new GamePlatform { GamePlatformId = 6, GameId = 2, PlatformId = 3, Description = "Xbox Series X|S/Xbox One.", Status = 1, Delete = false },

            // COD
            new GamePlatform { GamePlatformId = 7, GameId = 3, PlatformId = 1, Description = "PC (Battle.net/Steam) - ưu tiên driver GPU mới.", Status = 1, Delete = false },
            new GamePlatform { GamePlatformId = 8, GameId = 3, PlatformId = 2, Description = "PS5 - aim assist theo thiết lập.", Status = 1, Delete = false },
            new GamePlatform { GamePlatformId = 9, GameId = 3, PlatformId = 3, Description = "Xbox Series X|S - cross-play.", Status = 1, Delete = false },

            new GamePlatform { GamePlatformId = 10, GameId = 4, PlatformId = 1, Description = "PC (Battle.net/Steam) - BR free-to-play.", Status = 1, Delete = false },
            new GamePlatform { GamePlatformId = 11, GameId = 4, PlatformId = 2, Description = "PS5 - hỗ trợ tay cầm.", Status = 1, Delete = false },
            new GamePlatform { GamePlatformId = 12, GameId = 4, PlatformId = 3, Description = "Xbox Series X|S - cross-play.", Status = 1, Delete = false },

            // Riot
            new GamePlatform { GamePlatformId = 13, GameId = 5, PlatformId = 1, Description = "PC - anti-cheat Vanguard, nên tắt overlay lạ.", Status = 1, Delete = false },
            new GamePlatform { GamePlatformId = 14, GameId = 6, PlatformId = 1, Description = "PC - cập nhật patch hàng tuần.", Status = 1, Delete = false },

            // Ubisoft
            new GamePlatform { GamePlatformId = 15, GameId = 7, PlatformId = 1, Description = "PC (Ubisoft Connect/Steam).", Status = 1, Delete = false },
            new GamePlatform { GamePlatformId = 16, GameId = 7, PlatformId = 2, Description = "PS5/PS4.", Status = 1, Delete = false },
            new GamePlatform { GamePlatformId = 17, GameId = 7, PlatformId = 3, Description = "Xbox Series X|S/Xbox One.", Status = 1, Delete = false },

            // Valve / Epic
            new GamePlatform { GamePlatformId = 18, GameId = 8, PlatformId = 1, Description = "PC (Steam) - ưu tiên 128-tick server/community.", Status = 1, Delete = false },
            new GamePlatform { GamePlatformId = 19, GameId = 9, PlatformId = 1, Description = "PC (Epic) - nhiều chế độ chơi.", Status = 1, Delete = false },
            new GamePlatform { GamePlatformId = 20, GameId = 9, PlatformId = 4, Description = "Nintendo Switch - ưu tiên chế độ hiệu năng.", Status = 1, Delete = false }
        );

        modelBuilder.Entity<GameImage>().HasData(
            new GameImage
            {
                GameImageId = 1,
                GameId = 1,
                URL = "/seed/games/bf2042-1.jpg",
                Name = "Battlefield 2042 - Screenshot 01",
                Description = "Cảnh chiến trường, phương tiện và hiệu ứng thời tiết.",
                Status = 1,
                Delete = false
            },
            new GameImage
            {
                GameImageId = 2,
                GameId = 1,
                URL = "/seed/games/bf2042-2.jpg",
                Name = "Battlefield 2042 - Screenshot 02",
                Description = "Giao tranh cự ly gần trong khu vực đô thị.",
                Status = 1,
                Delete = false
            },
            new GameImage
            {
                GameImageId = 3,
                GameId = 3,
                URL = "/seed/games/cod-mw2-1.jpg",
                Name = "MWII - Screenshot 01",
                Description = "Multiplayer: giao tranh nhịp nhanh.",
                Status = 1,
                Delete = false
            },
            new GameImage
            {
                GameImageId = 4,
                GameId = 4,
                URL = "/seed/games/warzone-1.jpg",
                Name = "Warzone - Screenshot 01",
                Description = "Battle Royale - vòng bo và loot đồ.",
                Status = 1,
                Delete = false
            },
            new GameImage
            {
                GameImageId = 5,
                GameId = 5,
                URL = "/seed/games/valorant-1.jpg",
                Name = "VALORANT - Screenshot 01",
                Description = "Đặt/giải spike, sử dụng kỹ năng agent.",
                Status = 1,
                Delete = false
            },
            new GameImage
            {
                GameImageId = 6,
                GameId = 6,
                URL = "/seed/games/lol-1.jpg",
                Name = "LoL - Screenshot 01",
                Description = "Giao tranh tổng, kiểm soát mục tiêu lớn.",
                Status = 1,
                Delete = false
            },
            new GameImage
            {
                GameImageId = 7,
                GameId = 7,
                URL = "/seed/games/r6s-1.jpg",
                Name = "R6S - Screenshot 01",
                Description = "Phá hủy tường, drone và setup phòng thủ.",
                Status = 1,
                Delete = false
            },
            new GameImage
            {
                GameImageId = 8,
                GameId = 8,
                URL = "/seed/games/cs2-1.jpg",
                Name = "CS2 - Screenshot 01",
                Description = "Smoke/utility và đấu súng 5v5.",
                Status = 1,
                Delete = false
            },
            new GameImage
            {
                GameImageId = 9,
                GameId = 9,
                URL = "/seed/games/fortnite-1.jpg",
                Name = "Fortnite - Screenshot 01",
                Description = "Battle Royale theo mùa, nhiều sự kiện collab.",
                Status = 1,
                Delete = false
            }
        );

        modelBuilder.Entity<Patch>().HasData(
            new Patch
            {
                PatchId = 1,
                GameId = 1,
                Name = "Gói tối ưu FPS + giảm giật (BF2042)",
                Price = 69_000,
                Description = "Tối ưu cấu hình, giảm stutter, preset đồ họa theo GPU. Có hướng dẫn chi tiết bằng tiếng Việt.",
                UpdateBy = "admin",
                Status = 1,
                Delete = false
            },
            new Patch
            {
                PatchId = 2,
                GameId = 1,
                Name = "Pack Việt hóa UI cơ bản (BF2042)",
                Price = 49_000,
                Description = "Việt hóa menu/thiết lập/thông báo cơ bản. Không can thiệp gameplay.",
                UpdateBy = "editor",
                Status = 1,
                Delete = false
            },
            new Patch
            {
                PatchId = 3,
                GameId = 3,
                Name = "Gói tinh chỉnh âm thanh bước chân (MWII)",
                Price = 79_000,
                Description = "Profile EQ + preset ingame giúp nghe hướng bước chân rõ hơn (tùy tai nghe).",
                UpdateBy = "admin",
                Status = 1,
                Delete = false
            },
            new Patch
            {
                PatchId = 4,
                GameId = 4,
                Name = "Preset đồ họa competitive (Warzone)",
                Price = 59_000,
                Description = "Preset ưu tiên nhìn rõ + FPS ổn định: anti-aliasing, sharpening, render scale.",
                UpdateBy = "editor",
                Status = 1,
                Delete = false
            },
            new Patch
            {
                PatchId = 5,
                GameId = 5,
                Name = "Crosshair training pack (VALORANT)",
                Price = 39_000,
                Description = "Bộ bài tập aim + routine 20 phút/ngày (file + checklist).",
                UpdateBy = "editor",
                Status = 1,
                Delete = false
            },
            new Patch
            {
                PatchId = 6,
                GameId = 6,
                Name = "Gói tối ưu ping/packet loss (LoL)",
                Price = 29_000,
                Description = "Hướng dẫn tối ưu DNS, MTU, cấu hình router cơ bản + checklist kiểm tra mạng.",
                UpdateBy = "admin",
                Status = 1,
                Delete = false
            },
            new Patch
            {
                PatchId = 7,
                GameId = 7,
                Name = "Bộ setup map callout tiếng Việt (R6S)",
                Price = 45_000,
                Description = "Tài liệu callout theo map phổ biến, kèm mini-map đánh dấu vị trí.",
                UpdateBy = "editor",
                Status = 1,
                Delete = false
            },
            new Patch
            {
                PatchId = 8,
                GameId = 8,
                Name = "Config tối ưu FPS + độ trễ (CS2)",
                Price = 89_000,
                Description = "Autoexec, launch options, tối ưu chuột/Hz, giảm input lag (khuyến nghị theo cấu hình).",
                UpdateBy = "admin",
                Status = 1,
                Delete = false
            },
            new Patch
            {
                PatchId = 9,
                GameId = 9,
                Name = "Preset hiệu năng + nhìn rõ (Fortnite)",
                Price = 55_000,
                Description = "Tối ưu hiệu năng, setting competitive, hướng dẫn bật chế độ Performance Mode.",
                UpdateBy = "editor",
                Status = 1,
                Delete = false
            }
        );

        modelBuilder.Entity<PatchVersion>().HasData(
            // Patch 1 - BF2042 FPS
            new PatchVersion
            {
                PatchVersionId = 1,
                PatchId = 1,
                StaffId = 1,
                WorkWithGameVersion = "Season 6",
                VersionName = "v1.0",
                FileUrl = "/seed/files/bf2042-fps-v1.zip",
                FileSize = 18_200_000,
                ExtractionPassword = "patchseller",
                InstallationGuide = "Giải nén -> chạy file 'ApplyPreset.bat' -> vào game kiểm tra setting theo ảnh.",
                Changelog = "- Bổ sung preset cho GPU tầm trung\n- Giảm stutter khi chuyển cảnh",
                Status = 1,
                Note = "Khuyến nghị cập nhật driver GPU trước khi áp dụng.",
                CreateAt = new DateTime(2025, 12, 15),
                Delete = false
            },
            new PatchVersion
            {
                PatchVersionId = 2,
                PatchId = 1,
                StaffId = 2,
                WorkWithGameVersion = "Season 7",
                VersionName = "v1.1",
                FileUrl = "/seed/files/bf2042-fps-v1_1.zip",
                FileSize = 19_800_000,
                ExtractionPassword = "patchseller",
                InstallationGuide = "Giải nén -> đọc 'HuongDan.pdf' -> áp dụng preset theo GPU (NVIDIA/AMD).",
                Changelog = "- Cập nhật preset theo Season 7\n- Tối ưu thêm cho CPU 4/6 core",
                Status = 1,
                Note = "Nếu đang dùng DLSS/FSR, chọn preset tương ứng.",
                CreateAt = new DateTime(2026, 1, 10),
                Delete = false
            },

            // Patch 2 - BF2042 Viet hoa
            new PatchVersion
            {
                PatchVersionId = 3,
                PatchId = 2,
                StaffId = 2,
                WorkWithGameVersion = "Season 7",
                VersionName = "v1.0",
                FileUrl = "/seed/files/bf2042-vi-v1.zip",
                FileSize = 6_500_000,
                ExtractionPassword = "patchseller",
                InstallationGuide = "Copy thư mục 'localization' vào thư mục game (backup trước).",
                Changelog = "- Việt hóa menu/setting\n- Sửa lỗi font ở mục hiển thị",
                Status = 1,
                Note = "Không hỗ trợ Việt hóa toàn bộ nội dung cốt truyện.",
                CreateAt = new DateTime(2026, 1, 5),
                Delete = false
            },
            new PatchVersion
            {
                PatchVersionId = 4,
                PatchId = 2,
                StaffId = 1,
                WorkWithGameVersion = "Season 7.1",
                VersionName = "v1.1",
                FileUrl = "/seed/files/bf2042-vi-v1_1.zip",
                FileSize = 6_950_000,
                ExtractionPassword = "patchseller",
                InstallationGuide = "Copy + ghi đè. Nếu lỗi, xóa cache shader và khởi động lại game.",
                Changelog = "- Cập nhật thuật ngữ vũ khí\n- Sửa lỗi hiển thị một số nút",
                Status = 1,
                Note = "Có kèm file hoàn tác (restore).",
                CreateAt = new DateTime(2026, 1, 20),
                Delete = false
            },

            // Patch 3 - MWII footsteps
            new PatchVersion
            {
                PatchVersionId = 5,
                PatchId = 3,
                StaffId = 1,
                WorkWithGameVersion = "S05 Reloaded",
                VersionName = "v2.0",
                FileUrl = "/seed/files/mw2-audio-v2.zip",
                FileSize = 12_400_000,
                ExtractionPassword = "patchseller",
                InstallationGuide = "Import preset vào Equalizer APO/Peace hoặc dùng preset ingame theo hướng dẫn.",
                Changelog = "- Tối ưu preset cho tai nghe closed-back\n- Giảm chói treble",
                Status = 1,
                Note = "Hiệu quả phụ thuộc tai nghe + sound card.",
                CreateAt = new DateTime(2026, 1, 2),
                Delete = false
            },
            new PatchVersion
            {
                PatchVersionId = 6,
                PatchId = 3,
                StaffId = 2,
                WorkWithGameVersion = "S06",
                VersionName = "v2.1",
                FileUrl = "/seed/files/mw2-audio-v2_1.zip",
                FileSize = 12_900_000,
                ExtractionPassword = "patchseller",
                InstallationGuide = "Chọn preset theo tai nghe (IEM/Over-ear). Đọc quick-guide 1 trang.",
                Changelog = "- Bổ sung preset IEM\n- Cân lại mid để nghe direction tốt hơn",
                Status = 1,
                Note = "Tắt 'Loudness EQ' của Windows để đúng âm.",
                CreateAt = new DateTime(2026, 1, 18),
                Delete = false
            },

            // Patch 4 - Warzone competitive
            new PatchVersion
            {
                PatchVersionId = 7,
                PatchId = 4,
                StaffId = 2,
                WorkWithGameVersion = "Season 1",
                VersionName = "v1.0",
                FileUrl = "/seed/files/warzone-visual-v1.zip",
                FileSize = 8_100_000,
                ExtractionPassword = "patchseller",
                InstallationGuide = "Áp dụng file config + chỉnh lại brightness theo màn hình (hướng dẫn kèm).",
                Changelog = "- Preset competitive\n- Giảm blur và tăng clarity",
                Status = 1,
                Note = "Khuyến nghị tắt motion blur (world/weapon).",
                CreateAt = new DateTime(2026, 1, 6),
                Delete = false
            },
            new PatchVersion
            {
                PatchVersionId = 8,
                PatchId = 4,
                StaffId = 1,
                WorkWithGameVersion = "Season 1.5",
                VersionName = "v1.1",
                FileUrl = "/seed/files/warzone-visual-v1_1.zip",
                FileSize = 8_450_000,
                ExtractionPassword = "patchseller",
                InstallationGuide = "Copy config -> vào game kiểm tra render scale -> chạy benchmark map.",
                Changelog = "- Cập nhật theo bản vá\n- Cải thiện độ nét ở xa",
                Status = 1,
                Note = "Nếu bị crash, xóa config cũ và áp dụng lại.",
                CreateAt = new DateTime(2026, 1, 22),
                Delete = false
            },

            // Patch 5 - Valorant training
            new PatchVersion
            {
                PatchVersionId = 9,
                PatchId = 5,
                StaffId = 2,
                WorkWithGameVersion = "Patch 8.x",
                VersionName = "v1.0",
                FileUrl = "/seed/files/valorant-aimpack-v1.zip",
                FileSize = 3_200_000,
                ExtractionPassword = "patchseller",
                InstallationGuide = "Làm theo lịch 7 ngày. Mỗi ngày 20 phút: range + DM + bài tập tracking.",
                Changelog = "- Lịch tập cơ bản\n- Checklist theo tuần",
                Status = 1,
                Note = "Tùy chỉnh sens ổn định trước khi tập.",
                CreateAt = new DateTime(2026, 1, 1),
                Delete = false
            },
            new PatchVersion
            {
                PatchVersionId = 10,
                PatchId = 5,
                StaffId = 1,
                WorkWithGameVersion = "Patch 9.x",
                VersionName = "v1.1",
                FileUrl = "/seed/files/valorant-aimpack-v1_1.zip",
                FileSize = 3_650_000,
                ExtractionPassword = "patchseller",
                InstallationGuide = "Bổ sung bài tập micro-adjust. In checklist hoặc dùng file Notion (link trong guide).",
                Changelog = "- Thêm micro-adjust\n- Tối ưu routine cho rank Đồng/Bạc",
                Status = 1,
                Note = "Ưu tiên tập đúng form hơn là tốc độ.",
                CreateAt = new DateTime(2026, 1, 16),
                Delete = false
            },

            // Patch 6 - LoL network
            new PatchVersion
            {
                PatchVersionId = 11,
                PatchId = 6,
                StaffId = 1,
                WorkWithGameVersion = "14.x",
                VersionName = "v1.0",
                FileUrl = "/seed/files/lol-network-v1.zip",
                FileSize = 1_150_000,
                ExtractionPassword = "patchseller",
                InstallationGuide = "Chạy script kiểm tra mạng -> áp dụng DNS/MTU theo ISP -> test trong custom.",
                Changelog = "- Checklist tối ưu\n- Cách test packet loss",
                Status = 1,
                Note = "Không can thiệp file game.",
                CreateAt = new DateTime(2026, 1, 8),
                Delete = false
            },
            new PatchVersion
            {
                PatchVersionId = 12,
                PatchId = 6,
                StaffId = 2,
                WorkWithGameVersion = "14.x",
                VersionName = "v1.1",
                FileUrl = "/seed/files/lol-network-v1_1.zip",
                FileSize = 1_300_000,
                ExtractionPassword = "patchseller",
                InstallationGuide = "Bổ sung hướng dẫn tối ưu WiFi (5GHz), ưu tiên dây LAN nếu có.",
                Changelog = "- Thêm phần tối ưu WiFi\n- Mẹo giảm jitter",
                Status = 1,
                Note = "Nếu dùng VPN, thử tắt để đối chiếu.",
                CreateAt = new DateTime(2026, 1, 23),
                Delete = false
            },

            // Patch 7 - R6S callout
            new PatchVersion
            {
                PatchVersionId = 13,
                PatchId = 7,
                StaffId = 2,
                WorkWithGameVersion = "Y9S4",
                VersionName = "v1.0",
                FileUrl = "/seed/files/r6s-callout-vi-v1.zip",
                FileSize = 4_800_000,
                ExtractionPassword = "patchseller",
                InstallationGuide = "Tải về -> mở PDF -> học theo map. Có flashcard để ôn nhanh.",
                Changelog = "- Callout tiếng Việt + tiếng Anh\n- Mini-map đánh dấu",
                Status = 1,
                Note = "Ưu tiên dùng callout ngắn, thống nhất trong team.",
                CreateAt = new DateTime(2026, 1, 9),
                Delete = false
            },
            new PatchVersion
            {
                PatchVersionId = 14,
                PatchId = 7,
                StaffId = 1,
                WorkWithGameVersion = "Y9S4.1",
                VersionName = "v1.1",
                FileUrl = "/seed/files/r6s-callout-vi-v1_1.zip",
                FileSize = 5_050_000,
                ExtractionPassword = "patchseller",
                InstallationGuide = "Bổ sung map mới + cập nhật tên khu vực. In ra hoặc dùng trên điện thoại.",
                Changelog = "- Thêm map mới\n- Sửa lại thuật ngữ phổ biến",
                Status = 1,
                Note = "Có phần luyện callout 10 phút/ngày.",
                CreateAt = new DateTime(2026, 1, 21),
                Delete = false
            },

            // Patch 8 - CS2 config
            new PatchVersion
            {
                PatchVersionId = 15,
                PatchId = 8,
                StaffId = 1,
                WorkWithGameVersion = "Build 2025.12",
                VersionName = "v1.0",
                FileUrl = "/seed/files/cs2-config-v1.zip",
                FileSize = 2_950_000,
                ExtractionPassword = "patchseller",
                InstallationGuide = "Copy autoexec.cfg -> set launch options -> chỉnh Windows mouse + Hz theo guide.",
                Changelog = "- Autoexec cơ bản\n- Launch options an toàn\n- Tối ưu input lag",
                Status = 1,
                Note = "Không dùng lệnh gây VAC risk. Chỉ config hợp lệ.",
                CreateAt = new DateTime(2026, 1, 3),
                Delete = false
            },
            new PatchVersion
            {
                PatchVersionId = 16,
                PatchId = 8,
                StaffId = 2,
                WorkWithGameVersion = "Build 2026.01",
                VersionName = "v1.1",
                FileUrl = "/seed/files/cs2-config-v1_1.zip",
                FileSize = 3_150_000,
                ExtractionPassword = "patchseller",
                InstallationGuide = "Bổ sung preset cho CPU yếu + hướng dẫn giới hạn FPS hợp lý.",
                Changelog = "- Thêm preset low-end\n- Tối ưu net settings",
                Status = 1,
                Note = "Nếu dùng laptop, ưu tiên chế độ hiệu năng cao.",
                CreateAt = new DateTime(2026, 1, 24),
                Delete = false
            },

            // Patch 9 - Fortnite performance
            new PatchVersion
            {
                PatchVersionId = 17,
                PatchId = 9,
                StaffId = 2,
                WorkWithGameVersion = "Chapter 5",
                VersionName = "v1.0",
                FileUrl = "/seed/files/fortnite-perf-v1.zip",
                FileSize = 7_750_000,
                ExtractionPassword = "patchseller",
                InstallationGuide = "Bật Performance Mode -> áp dụng preset -> test trong Creative map benchmark.",
                Changelog = "- Preset hiệu năng\n- Gợi ý setting theo GPU",
                Status = 1,
                Note = "Nếu bị drop FPS, giảm view distance trước.",
                CreateAt = new DateTime(2026, 1, 7),
                Delete = false
            },
            new PatchVersion
            {
                PatchVersionId = 18,
                PatchId = 9,
                StaffId = 1,
                WorkWithGameVersion = "Chapter 5.1",
                VersionName = "v1.1",
                FileUrl = "/seed/files/fortnite-perf-v1_1.zip",
                FileSize = 8_050_000,
                ExtractionPassword = "patchseller",
                InstallationGuide = "Cập nhật preset, bổ sung hướng dẫn tắt background apps và tối ưu shader cache.",
                Changelog = "- Cập nhật theo bản vá\n- Thêm checklist tối ưu Windows",
                Status = 1,
                Note = "Khuyến nghị cập nhật driver trước khi test.",
                CreateAt = new DateTime(2026, 1, 25),
                Delete = false
            }
        );

        modelBuilder.Entity<PatchImage>().HasData(
            // Patch thumbnails
            new PatchImage { PatchImageId = 1, PatchId = 1, PatchVersionId = null, URL = "/seed/patches/bf2042-fps-thumb.jpg", Name = "Thumbnail", Description = "Ảnh đại diện patch", IsThumbnail = true, Delete = false },
            new PatchImage { PatchImageId = 2, PatchId = 2, PatchVersionId = null, URL = "/seed/patches/bf2042-vi-thumb.jpg", Name = "Thumbnail", Description = "Ảnh đại diện patch", IsThumbnail = true, Delete = false },
            new PatchImage { PatchImageId = 3, PatchId = 3, PatchVersionId = null, URL = "/seed/patches/mw2-audio-thumb.jpg", Name = "Thumbnail", Description = "Ảnh đại diện patch", IsThumbnail = true, Delete = false },
            new PatchImage { PatchImageId = 4, PatchId = 4, PatchVersionId = null, URL = "/seed/patches/warzone-visual-thumb.jpg", Name = "Thumbnail", Description = "Ảnh đại diện patch", IsThumbnail = true, Delete = false },
            new PatchImage { PatchImageId = 5, PatchId = 5, PatchVersionId = null, URL = "/seed/patches/valorant-aim-thumb.jpg", Name = "Thumbnail", Description = "Ảnh đại diện patch", IsThumbnail = true, Delete = false },
            new PatchImage { PatchImageId = 6, PatchId = 6, PatchVersionId = null, URL = "/seed/patches/lol-network-thumb.jpg", Name = "Thumbnail", Description = "Ảnh đại diện patch", IsThumbnail = true, Delete = false },
            new PatchImage { PatchImageId = 7, PatchId = 7, PatchVersionId = null, URL = "/seed/patches/r6s-callout-thumb.jpg", Name = "Thumbnail", Description = "Ảnh đại diện patch", IsThumbnail = true, Delete = false },
            new PatchImage { PatchImageId = 8, PatchId = 8, PatchVersionId = null, URL = "/seed/patches/cs2-config-thumb.jpg", Name = "Thumbnail", Description = "Ảnh đại diện patch", IsThumbnail = true, Delete = false },
            new PatchImage { PatchImageId = 9, PatchId = 9, PatchVersionId = null, URL = "/seed/patches/fortnite-perf-thumb.jpg", Name = "Thumbnail", Description = "Ảnh đại diện patch", IsThumbnail = true, Delete = false },

            // Some version images
            new PatchImage { PatchImageId = 10, PatchId = null, PatchVersionId = 2, URL = "/seed/patches/bf2042-fps-v1_1-1.jpg", Name = "Preset NVIDIA", Description = "Ảnh minh họa setting NVIDIA", IsThumbnail = false, Delete = false },
            new PatchImage { PatchImageId = 11, PatchId = null, PatchVersionId = 2, URL = "/seed/patches/bf2042-fps-v1_1-2.jpg", Name = "Preset AMD", Description = "Ảnh minh họa setting AMD", IsThumbnail = false, Delete = false },
            new PatchImage { PatchImageId = 12, PatchId = null, PatchVersionId = 15, URL = "/seed/patches/cs2-config-v1-1.jpg", Name = "Autoexec", Description = "Ví dụ autoexec.cfg", IsThumbnail = false, Delete = false }
        );

        modelBuilder.Entity<User>().HasData(
            new User
            {
                UserId = 1,
                FullName = "Nguyễn Văn A",
                UserName = "nguyenvana",
                Email = "vana@gmail.com",
                PhoneNumber = "0901000111",
                PasswordHash = "B855E41C5C5F5061ECBA4FD8613A7760",
                RewardPoint = 120,
                Status = 1,
                Delete = false,
                RankId = 1
            },
            new User
            {
                UserId = 2,
                UserName = "tranthib",
                FullName = "Chần Thỵ Bê",
                Email = "thib@gmail.com",
                PhoneNumber = "0902000222",
                PasswordHash = "B855E41C5C5F5061ECBA4FD8613A7760",
                RewardPoint = 980,
                Status = 1,
                Delete = false,
                RankId = 2
            },
            new User
            {
                UserId = 3,
                UserName = "leminh",
                FullName = "Lamie Yamal",
                Email = "leminh@gmail.com",
                PhoneNumber = "0903000333",
                PasswordHash = "B855E41C5C5F5061ECBA4FD8613A7760",
                RewardPoint = 2450,
                Status = 1,
                Delete = false,
                RankId = 3
            },
            new User
            {
                UserId = 4,
                UserName = "hoangnam",
                FullName = "Hạo Nam Hạo Nam",
                Email = "hoangnam@gmail.com",
                PhoneNumber = "0904000444",
                PasswordHash = "B855E41C5C5F5061ECBA4FD8613A7760",
                RewardPoint = 420,
                Status = 1,
                Delete = false,
                RankId = 2
            },
            new User
            {
                UserId = 5,
                UserName = "quynhanh",
                FullName = "Cuỳnh Anh",
                Email = "quynhanh@gmail.com",
                PhoneNumber = "0905000555",
                PasswordHash = "B855E41C5C5F5061ECBA4FD8613A7760",
                RewardPoint = 75,
                Status = 1,
                Delete = false,
                RankId = 1
            },
            new User
            {
                UserId = 6,
                UserName = "ducphong",
                FullName = "Nguyễn Đuck Fong",
                Email = "ducphong@gmail.com",
                PhoneNumber = "0906000666",
                PasswordHash = "B855E41C5C5F5061ECBA4FD8613A7760",
                RewardPoint = 1500,
                Status = 1,
                Delete = false,
                RankId = 3
            }
        );

        modelBuilder.Entity<Cart>().HasData(
            new Cart { CartId = 1, UserId = 1, Delete = false },
            new Cart { CartId = 2, UserId = 2, Delete = false },
            new Cart { CartId = 3, UserId = 3, Delete = false },
            new Cart { CartId = 4, UserId = 4, Delete = false },
            new Cart { CartId = 5, UserId = 5, Delete = false },
            new Cart { CartId = 6, UserId = 6, Delete = false }
        );

        modelBuilder.Entity<CartItem>().HasData(
            new CartItem { CartItemId = 1, CartId = 1, PatchId = 8, Delete = false },
            new CartItem { CartItemId = 2, CartId = 1, PatchId = 1, Delete = false },
            new CartItem { CartItemId = 3, CartId = 2, PatchId = 4, Delete = false },
            new CartItem { CartItemId = 4, CartId = 4, PatchId = 5, Delete = false },
            new CartItem { CartItemId = 5, CartId = 5, PatchId = 6, Delete = false }
        );

        modelBuilder.Entity<Discount>().HasData(
            new Discount
            {
                DiscountId = 1,
                Code = "NEWUSER10",
                DiscountType = "Percent",
                Value = 10,
                MaxDiscount = 50_000,
                MinOrderValue = 100_000,
                UsageLimit = 10_000,
                LimitPerUser = 1,
                UsedCount = 0,
                Status = 1,
                Delete = false,
                RankId = 1
            },
            new Discount
            {
                DiscountId = 2,
                Code = "BAC5",
                DiscountType = "Percent",
                Value = 5,
                MaxDiscount = 100_000,
                MinOrderValue = 200_000,
                UsageLimit = 5_000,
                LimitPerUser = 2,
                UsedCount = 0,
                Status = 1,
                Delete = false,
                RankId = 2
            },
            new Discount
            {
                DiscountId = 3,
                Code = "VANG10",
                DiscountType = "Percent",
                Value = 10,
                MaxDiscount = 200_000,
                MinOrderValue = 500_000,
                UsageLimit = 3_000,
                LimitPerUser = 3,
                UsedCount = 0,
                Status = 1,
                Delete = false,
                RankId = 3
            },
            new Discount
            {
                DiscountId = 4,
                Code = "TET2026",
                DiscountType = "Amount",
                Value = 30_000,
                MaxDiscount = 30_000,
                MinOrderValue = 200_000,
                UsageLimit = 20_000,
                LimitPerUser = 1,
                UsedCount = 0,
                Status = 1,
                Delete = false,
                RankId = 1
            },
            new Discount
            {
                DiscountId = 5,
                Code = "FPS15",
                DiscountType = "Percent",
                Value = 15,
                MaxDiscount = 120_000,
                MinOrderValue = 300_000,
                UsageLimit = 2_000,
                LimitPerUser = 1,
                UsedCount = 0,
                Status = 1,
                Delete = false,
                RankId = 2
            }
        );

        modelBuilder.Entity<Order>().HasData(
            new Order
            {
                OrderId = 1,
                UserId = 2,
                DiscountId = 2,
                PaymentStatus = "Paid",
                OrderCode = "PS-202601-0001",
                OrderDate = new DateTime(2026, 1, 11),
                UsedRewardPoint = 50,
                TotalAmount = 148_000,
                DiscountAmount = 7_400,
                FinalAmount = 140_600,
                PaymentLink = "https://pay.local/PS-202601-0001",
                PaymentExpiration = new DateTime(2026, 1, 12),
                Note = "Thanh toán ví điện tử. Giao file ngay sau khi xác nhận.",
                Status = 1
            },
            new Order
            {
                OrderId = 2,
                UserId = 3,
                DiscountId = 3,
                PaymentStatus = "Paid",
                OrderCode = "PS-202601-0002",
                OrderDate = new DateTime(2026, 1, 15),
                UsedRewardPoint = 0,
                TotalAmount = 168_000,
                DiscountAmount = 16_800,
                FinalAmount = 151_200,
                PaymentLink = "https://pay.local/PS-202601-0002",
                PaymentExpiration = new DateTime(2026, 1, 16),
                Note = "Khách rank Vàng, áp dụng ưu đãi theo rank.",
                Status = 1
            }
        );

        modelBuilder.Entity<OrderDetail>().HasData(
            new OrderDetail { OrderDetailID = 1, OrderId = 1, PatchId = 4, Price = 59_000 },
            new OrderDetail { OrderDetailID = 2, OrderId = 1, PatchId = 5, Price = 39_000 },
            new OrderDetail { OrderDetailID = 3, OrderId = 2, PatchId = 8, Price = 89_000 },
            new OrderDetail { OrderDetailID = 4, OrderId = 2, PatchId = 1, Price = 69_000 }
        );

        modelBuilder.Entity<UserPurchase>().HasData(
            new UserPurchase { UserPurchaseId = 1, UserId = 2, PatchId = 4, PurchasedAt = new DateTime(2026, 1, 11) },
            new UserPurchase { UserPurchaseId = 2, UserId = 2, PatchId = 5, PurchasedAt = new DateTime(2026, 1, 11) },
            new UserPurchase { UserPurchaseId = 3, UserId = 3, PatchId = 8, PurchasedAt = new DateTime(2026, 1, 15) },
            new UserPurchase { UserPurchaseId = 4, UserId = 3, PatchId = 1, PurchasedAt = new DateTime(2026, 1, 15) },
            new UserPurchase { UserPurchaseId = 5, UserId = 1, PatchId = 2, PurchasedAt = new DateTime(2026, 1, 20) }
        );

        modelBuilder.Entity<Review>().HasData(
            new Review
            {
                ReviewId = 1,
                UserId = 2,
                PatchId = 4,
                UserName = "tranthib",
                Title = "Preset nhìn rõ, FPS ổn",
                Content = "Áp dụng theo hướng dẫn là ổn, game đỡ mờ và FPS mượt hơn. Nên có thêm preset cho GPU yếu.",
                Overall = 4.5,
                Status = 1
            },
            new Review
            {
                ReviewId = 2,
                UserId = 3,
                PatchId = 8,
                UserName = "leminh",
                Title = "Config CS2 hợp lý",
                Content = "Autoexec gọn, không có lệnh linh tinh. Input lag giảm thấy rõ, recommend.",
                Overall = 5.0,
                Status = 1
            },
            new Review
            {
                ReviewId = 3,
                UserId = 1,
                PatchId = 2,
                UserName = "nguyenvana",
                Title = "Việt hóa dễ cài",
                Content = "Copy là chạy, có file restore nên yên tâm. Thuật ngữ dịch ổn.",
                Overall = 4.2,
                Status = 1
            },
            new Review
            {
                ReviewId = 4,
                UserId = 4,
                PatchId = 5,
                UserName = "hoangnam",
                Title = "Bài tập aim ok",
                Content = "Routine 20 phút hợp lý, kiên trì 1 tuần thấy aim ổn hơn trong DM.",
                Overall = 4.0,
                Status = 1
            }
        );

        modelBuilder.Entity<DownloadLog>().HasData(
            new DownloadLog { DownloadLogId = 1, UserId = 2, PatchVersionId = 7, DownloadedAt = new DateTime(2026, 1, 11, 10, 30, 0) },
            new DownloadLog { DownloadLogId = 2, UserId = 2, PatchVersionId = 9, DownloadedAt = new DateTime(2026, 1, 11, 10, 45, 0) },
            new DownloadLog { DownloadLogId = 3, UserId = 3, PatchVersionId = 15, DownloadedAt = new DateTime(2026, 1, 15, 21, 10, 0) },
            new DownloadLog { DownloadLogId = 4, UserId = 1, PatchVersionId = 3, DownloadedAt = new DateTime(2026, 1, 20, 9, 5, 0) }
        );

        modelBuilder.Entity<ActionLog>().HasData(
            new ActionLog
            {
                ActionLogId = 1,
                StaffId = 1,
                Action = "Create",
                TargetTable = "Patch",
                TargetID = "8",
                OldValue = "",
                NewValue = "Config tối ưu FPS + độ trễ (CS2)",
                CreatedAt = new DateTime(2026, 1, 3, 8, 0, 0)
            },
            new ActionLog
            {
                ActionLogId = 2,
                StaffId = 2,
                Action = "Update",
                TargetTable = "PatchVersion",
                TargetID = "2",
                OldValue = "v1.0",
                NewValue = "v1.1",
                CreatedAt = new DateTime(2026, 1, 10, 14, 20, 0)
            }
        );
    }
}

