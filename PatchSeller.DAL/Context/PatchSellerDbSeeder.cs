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
                 PublisherId = -1,
                 Name = "Unknow",
                 Description = "Không rõ",
                 Status = 1,
                 CreatedAt = new DateTime(2020, 1, 1),
                 Delete = false
             },
            new Publisher
            {
                PublisherId = 1,
                Name = "Electronic Art",
                Description = "Nhà phát hành lớn (EA). Nổi tiếng với Battlefield, FIFA, Apex Legends.",
                Status = 1,
                CreatedAt = new DateTime(2020, 1, 1),
                Delete = false
            },
            new Publisher
            {
                PublisherId = 2,
                Name = "Riot Games",
                Description = "Nhà phát hành game eSports: League of Legends, VALORANT.",
                Status = 1,
                CreatedAt = new DateTime(2020, 1, 1),
                Delete = false
            },
            new Publisher
            {
                PublisherId = 3,
                Name = "Activision",
                Description = "Nhà phát hành Call of Duty (MW/Warzone) và nhiều series FPS.",
                Status = 1,
                CreatedAt = new DateTime(2020, 1, 1),
                Delete = false
            },
            new Publisher
            {
                PublisherId = 4,
                Name = "Ubisoft",
                Description = "Nhà phát hành nổi tiếng với Rainbow Six, Assassin's Creed, Far Cry.",
                Status = 1,
                CreatedAt = new DateTime(2020, 1, 1),
                Delete = false
            },
            new Publisher
            {
                PublisherId = 5,
                Name = "Valve",
                Description = "Nhà phát hành CS2, Dota 2; tập trung hệ sinh thái Steam.",
                Status = 1,
                CreatedAt = new DateTime(2020, 1, 1),
                Delete = false
            },
            new Publisher
            {
                PublisherId = 6,
                Name = "Epic Games",
                Description = "Nhà phát hành Fortnite; nền tảng Epic Games Store.",
                Status = 1,
                CreatedAt = new DateTime(2020, 1, 1),
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
                CreatedAt = new DateTime(2020, 1, 1),
                Delete = false
            },
            new Category
            {
                CategoryId = 2,
                CategoryName = "Battle Royale",
                Description = "Sinh tồn, vòng bo, loot đồ.",
                Status = 1,
                CreatedAt = new DateTime(2020, 1, 1),
                Delete = false
            },
            new Category
            {
                CategoryId = 3,
                CategoryName = "MOBA",
                Description = "Chiến thuật 5v5, phối hợp đội hình.",
                Status = 1,
                CreatedAt = new DateTime(2020, 1, 1),
                Delete = false
            },
            new Category
            {
                CategoryId = 4,
                CategoryName = "Tactical Shooter",
                Description = "Bắn súng chiến thuật, đặt/giải bom, kỹ năng cá nhân + phối hợp.",
                Status = 1,
                CreatedAt = new DateTime(2020, 1, 1),
                Delete = false
            },
            new Category
            {
                CategoryId = 5,
                CategoryName = "Hành động",
                Description = "Nhịp nhanh, thiên về chiến đấu.",
                Status = 1,
                CreatedAt = new DateTime(2020, 1, 1),
                Delete = false
            },
            new Category
            {
                CategoryId = 6,
                CategoryName = "eSports",
                Description = "Tập trung thi đấu xếp hạng/giải đấu.",
                Status = 1,
                CreatedAt = new DateTime(2020, 1, 1),
                Delete = false
            },
            new Category
            {
                CategoryId = 7,
                CategoryName = "Co-op",
                Description = "Chơi hợp tác cùng bạn bè.",
                Status = 1,
                CreatedAt = new DateTime(2020, 1, 1),
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
                CreatedAt = new DateTime(2020, 1, 1),
                Delete = false
            },
            new Platform
            {
                PlatformId = 2,
                Name = "PlayStation 5",
                Description = "PS5/PSN.",
                Status = 1,
                CreatedAt = new DateTime(2020, 1, 1),
                Delete = false
            },
            new Platform
            {
                PlatformId = 3,
                Name = "Xbox Series X|S",
                Description = "Xbox Series X/S.",
                Status = 1,
                CreatedAt = new DateTime(2020, 1, 1),
                Delete = false
            },
            new Platform
            {
                PlatformId = 4,
                Name = "Nintendo Switch",
                Description = "Switch/handheld.",
                Status = 1,
                CreatedAt = new DateTime(2020, 1, 1),
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
                CreatedAt = new DateTime(2020, 1, 1),
                Delete = false
            },
            new Rank
            {
                RankId = 2,
                RankName = "Bạc",
                MiniumSpend = 500_000,
                Description = "Ưu đãi tốt hơn, hỗ trợ nhanh hơn, voucher theo rank.",
                Status = 1,
                CreatedAt = new DateTime(2020, 1, 1),
                Delete = false
            },
            new Rank
            {
                RankId = 3,
                RankName = "Vàng",
                MiniumSpend = 2_000_000,
                Description = "Ưu đãi cao nhất, ưu tiên hỗ trợ, tham gia test patch sớm.",
                Status = 1,
                CreatedAt = new DateTime(2020, 1, 1),
                Delete = false
            }
        );

        modelBuilder.Entity<Staff>().HasData(
            new Staff
            {
                StaffId = 1,
                UserName = "admin",
                PasswordHash = "26dc318942685872cf79c5eb96c9bb13",
                Email = "admin@patchseller.local",
                Role = "Admin",
                PhoneNumber = "0987675845",
                CreatedAt = new DateTime(2020, 1, 1),
                FullName = "Ass Min"
            },
            new Staff
            {
                StaffId = 2,
                UserName = "editor",
                PasswordHash = "26dc318942685872cf79c5eb96c9bb13",
                Email = "editor@patchseller.local",
                Role = "Editor",
                PhoneNumber = "0987675866",
                CreatedAt = new DateTime(2020, 1, 1),
                FullName = "E Đít Tơ"
            }
        );

        modelBuilder.Entity<Game>().HasData(
            new Game
            {
                GameId = 1,
                Title = "Mafia: The Old Country Deluxe Edition",
                Developer = "Hangar 13",
                Description = "<h2>COMPARE EDITIONS</h2><p> </p><p><img src=\"https://avigames.net/wp-content/uploads/2025/08/042dc0ab3b41c3563d24e41b8b132c07.\"></p><p> </p><p> </p><h2>Mafia: The Old Country Deluxe Edition</h2><p> </p><p><img src=\"https://avigames.net/wp-content/uploads/2025/08/caf7a2a3d335e66ac20dc4de6e247b7c.\"></p><p> </p><p>Mafia: The Old Country Deluxe Edition lets you descend into Sicily's criminal underworld in true mafioso style with a variety of bonus items for your collection.</p><h2>Deluxe Edition includes:</h2><p><strong>• Full base game</strong></p><p><strong>• Padrino Pack</strong></p><p>     – “Lupara Speciale” Shotgun</p><p>     – “Vendetti Speciale” Pistol</p><p>     – “Immortale” Charm</p><p>     – “Padrino” Outfit</p><p>     – “Stiletto Speciale” Knife</p><p>     – “Eckhart Speciale” Limousine</p><p>     – “Cosimo” Horse and Accessories</p><p><strong>• Gatto Nero Pack</strong></p><p>     – “Bodeo Nero” Pistol</p><p>     – “Velocità” Charm</p><p>     – “Gatto Nero” Outfit</p><p>     – “Carozella Nero” Race Car</p><p><strong>• Bonus materials</strong></p><p>     – Digital Art Book</p><p>     – Original Score</p><p> </p><h2>Ưu đãi mua trước</h2><p> </p><p><img src=\"https://avigames.net/wp-content/uploads/2025/08/5d48af94fb53c36d8da4723721bb8ee3.\"></p><p> </p><p>Pre-Purchase Mafia: The Old Country Standard Edition or Deluxe Edition to receive the Soldato Pack featuring distinctive cosmetics and a helpful Charm.*</p><p>*Pre-Purchase offer available for Mafia: The Old Country Standard Edition and Deluxe Edition until launch. Internet connection required to redeem bonus content. Items will be automatically entitled in-game at launch. Terms apply.</p><p><br></p><h4>Thông tin cơ bản</h4><p>Uncover the origins of organized crime in Mafia: The Old Country, a gritty mob story set in the brutal underworld of 1900s Sicily. Fight to survive as Enzo Favara and prove your worth to the Family in this immersive third-person action-adventure.</p><p><br></p><p><br></p><p><strong>Tối thiểu:</strong></p><ol><li data-list=\"bullet\" class=\"ql-indent-1\"><span class=\"ql-ui\" contenteditable=\"false\"></span><strong>HĐH:</strong> Windows 10 / 11</li><li data-list=\"bullet\" class=\"ql-indent-1\"><span class=\"ql-ui\" contenteditable=\"false\"></span><strong>Bộ xử lý:</strong> AMD Ryzen 7 2700X / Intel Core i7-9700K</li><li data-list=\"bullet\" class=\"ql-indent-1\"><span class=\"ql-ui\" contenteditable=\"false\"></span><strong>Bộ nhớ:</strong> 16 GB RAM</li><li data-list=\"bullet\" class=\"ql-indent-1\"><span class=\"ql-ui\" contenteditable=\"false\"></span><strong>Đồ họa:</strong> AMD Radeon RX 5700 XT / NVIDIA RTX 2070</li><li data-list=\"bullet\" class=\"ql-indent-1\"><span class=\"ql-ui\" contenteditable=\"false\"></span><strong>DirectX:</strong> Phiên bản 12</li><li data-list=\"bullet\" class=\"ql-indent-1\"><span class=\"ql-ui\" contenteditable=\"false\"></span><strong>Lưu trữ:</strong> 55 GB chỗ trống khả dụng</li><li data-list=\"bullet\" class=\"ql-indent-1\"><span class=\"ql-ui\" contenteditable=\"false\"></span><strong>Ghi chú thêm:</strong> Requires a 64-bit processor and operating system. Requires SSD ; Graphic Preset: Medium ; Resolution: 1080p</li></ol><p><strong>Khuyến nghị:</strong></p><ol><li data-list=\"bullet\" class=\"ql-indent-1\"><span class=\"ql-ui\" contenteditable=\"false\"></span><strong>HĐH:</strong> Windows 10 / 11</li><li data-list=\"bullet\" class=\"ql-indent-1\"><span class=\"ql-ui\" contenteditable=\"false\"></span><strong>Bộ xử lý:</strong> AMD Ryzen 7 5800X / Intel Core i7-12700K</li><li data-list=\"bullet\" class=\"ql-indent-1\"><span class=\"ql-ui\" contenteditable=\"false\"></span><strong>Bộ nhớ:</strong> 32 GB RAM</li><li data-list=\"bullet\" class=\"ql-indent-1\"><span class=\"ql-ui\" contenteditable=\"false\"></span><strong>Đồ họa:</strong> AMD Radeon RX 6950 XT / NVIDIA RTX 3080 Ti</li><li data-list=\"bullet\" class=\"ql-indent-1\"><span class=\"ql-ui\" contenteditable=\"false\"></span><strong>DirectX:</strong> Phiên bản 12</li><li data-list=\"bullet\" class=\"ql-indent-1\"><span class=\"ql-ui\" contenteditable=\"false\"></span><strong>Lưu trữ:</strong> 55 GB chỗ trống khả dụng</li><li data-list=\"bullet\" class=\"ql-indent-1\"><span class=\"ql-ui\" contenteditable=\"false\"></span><strong>Ghi chú thêm:</strong> Requires a 64-bit processor and operating system. Requires SSD ; Graphic Preset: High ; Resolution: 1440p</li></ol><p><br></p><p><br></p>",
                Thumbnail = "/uploads/images/games/1c5ae9eb/MAF4-STD-DIGITAL_BANNERS_AND_CHANNEL_SETUP-D2C-STATIC-ENUS-NO_RATING-AGN-600x850-R1.jpg",
                ReleaseDate = new DateTime(2025, 08, 08),
                Status = 1,
                Delete = false,
                CreatedAt = new DateTime(2026, 01, 01),
                PublisherId = 1
            },
            new Game
            {
                GameId = 2,
                Title = "Clair Obscur: Expedition 33 Deluxe Edition",
                Developer = "Sandfall Interactive",
                Description = "<p>■ “eFootball™” – An Evolution from “PES”</p><p>It's an all-new era of digital football: “PES” has now evolved into “eFootball™”! And now you can experience the next generation of football gaming with “eFootball™”!</p><p>■ Welcoming Newcomers</p><p>After downloading, you can learn the basic controls of the game via a step-by-step tutorial that includes practical demonstrations! Complete them all and receive Lionel Messi!</p><p>[Ways of Playing]</p><p>■ Build Your Very Own Dream Team</p><p>You have a plethora of teams that can be chosen as your Base Team, including European powerhouses like FC Barcelona, Manchester United, FC Bayern München, AC Milan and Internazionale Milano. On top of that, you can also choose clubs from South America, J.League and even your favourite national team!</p><p>■ Sign Players</p><p>After creating your team, it's time to get some signings in! From current superstars to footballing legends; sign players and take your team to new heights!</p><p>・ Special Player List</p><p>Here you can sign special players such as standouts from actual fixtures, players from featured leagues and legends of the game!</p><p>・ Standard Player List</p><p>Here you can handpick and sign your favourite players. You can also use the Sort and Filter functions to narrow your search.</p><p>・ Manager List</p><p>Here you can sign managers who are adept at all sorts of tactical approaches with different Coaching Affinities.</p><p>■ Playing Matches</p><p>Once you have built a team with your favourite players, it’s time to take them to the pitch.</p><p>From testing your skills against the AI, to competing for ranking in Online Matches; enjoy eFootball™ the way you like!</p><p>・ Sharpen your skills in VS AI Matches</p><p>There are a variety of Events which coincide with the real-world football calendar, including a “Starter” Event for those just starting out, as well as Events where you can play against teams from high-profile leagues. Build a Dream Team that fits the Events’ themes and take part!</p><p>・ Put your strength to the test in User Matches</p><p>Enjoy real-time competition with the Division-based “eFootball™ League” and a wide variety of weekly Events. Can you take your Dream Team to the pinnacle of Division 1?</p><p>・ Max 3 vs 3 matches with friends</p><p>Use the Friend Match feature to play against your friends. Show them the true colours of your well-developed team!</p><p>Cooperative matches up to 3 vs 3 are also available. Get together with your friends and enjoy some heated footballing actions!</p><p>■ Player Development</p><p>Depending on Player Types, signed players can be further developed.</p><p>Level up your players by having them play in matches and by using “Level Training Programs”, then utilize the acquired Progression Points to develop them to match your playing style! Level Training Programs can be received by participating in Events.</p><p>The players' Progression Points will be automatically allocated into categories such as [Shooting], [Dribbling] and [Defending] in a way that will optimise their Player Stats and maximise their Overall Rating.</p><p>In case you prefer to customise a player to fit your personal likings, you have the option to allocate the Progression Points manually.</p><p>When in doubt about how to develop the player, you can use the [Recommended] function to automatically allocate his Points.</p><p>Develop your players to your exact liking!</p><p>[For More Fun]</p><p>■ Weekly Live Updates</p><p>Data from real matches being played around the world is collated on a weekly basis and implemented in-game through the Live Update feature to create a more authentic experience. These updates affect various aspects of the game, including player Condition Ratings and team rosters.</p><p>*Users that reside in Belgium will not have access to loot boxes that require eFootball™ Coins as payment.</p><p>*eFootball™ Coins can be purchased in-game and can be used in various ways, such as signing randomly selected players or unlocking Match Passes.</p><p>*The description above may be changed in future updates.</p><h2><br></h2>",
                Thumbnail = "/uploads/images/games/5a5fb655/here-are-all-my-custom-covers-i-really-hope-football-life-v0-hr02epdm6ysd1.jpg",
                ReleaseDate = new DateTime(2025, 04, 24),
                Status = 1,
                Delete = false,
                CreatedAt = new DateTime(2025, 12, 31),
                PublisherId = 1
            },
            new Game
            {
                GameId = 3,
                Title = "The Elder Scrolls IV: Oblivion Remastered – Deluxe Edition",
                Developer = "Virtuos",
                Description = "<h2>Digital Deluxe Edition</h2><p> </p><p><img src=\"https://avigames.net/wp-content/uploads/2025/04/Jaws_Deluxe_Steam_SpecialAnnounce_616x943-EN-01.jpg\"></p><p>The Elder Scrolls IV: Oblivion Remastered Deluxe Edition includes:</p><p>– Digital base game</p><p>– New quests for unique digital Akatosh and Mehrunes Dagon Armors, Weapons, and Horse Armor Sets</p><p>– Digital Artbook and Soundtrack App</p><p>– Shivering Isles and Knights of the Nine story expansions</p><p>– Additional downloadable content: Fighter’s Stronghold, Spell Tomes, Vile Lair, Mehrune’s Razor, The Thieves Den, Wizard’s Tower, The Orrery, and Horse Armor Pack</p><p> </p><h2>About This Game</h2><p>The Elder Scrolls IV: Oblivion™ Remastered modernizes the 2006 Game of the Year with all new stunning visuals and refined gameplay. Explore the vast landscape of Cyrodiil like never before and stop the forces of Oblivion from overtaking the land in one of the greatest RPGs ever from the award-winning Bethesda Game Studios.</p><p><img src=\"https://avigames.net/wp-content/uploads/2025/04/JAWS_Rediscover_for_Steam.gif\"></p><p><strong>Rediscover Cyrodiil</strong></p><p>Journey through the rich world of Tamriel and battle across the planes of Oblivion where handcrafted details have been meticulously recreated to ensure each moment of exploration is awe-inspiring.</p><p><img src=\"https://avigames.net/wp-content/uploads/2025/04/JAWS_Your_Story_for_Steam.gif\"></p><p><strong>Navigate Your Own Story</strong></p><p>From the noble warrior to the sinister assassin, wizened sorcerer, or scrappy blacksmith, forge your path and play the way you want.</p><p><img src=\"https://avigames.net/wp-content/uploads/2025/04/JAWS_Adventure_for_Steam.gif\"></p><p><strong>Experience an Epic Adventure</strong></p><p>Step inside a universe bursting with captivating stories and encounter an unforgettable cast of characters. Master swordcraft and wield powerful magic as you fight to save Tamriel from the Daedric invasion.</p><p><img src=\"https://avigames.net/wp-content/uploads/2025/04/JAWS_Complete_Story_for_Steam.gif\"></p><p><strong>The Complete Story</strong></p><p>Experience everything Oblivion has to offer with previously released story expansions Shivering Isles, Knights of the Nine, and additional downloadable content included in The Elder Scrolls IV: Oblivion Remastered.</p><h4>Thông tin cơ bản</h4><p>Explore Cyrodiil like never before with stunning new visuals and refined gameplay in The Elder Scrolls IV: Oblivion™ Remastered.</p><p><br></p><p><br></p><h2>System Requirements</h2><p><strong>Minimum:</strong></p><p><br></p><ol><li data-list=\"bullet\" class=\"ql-indent-1\"><span class=\"ql-ui\" contenteditable=\"false\"></span>Requires a 64-bit processor and operating system</li><li data-list=\"bullet\" class=\"ql-indent-1\"><span class=\"ql-ui\" contenteditable=\"false\"></span><strong>OS:</strong> Windows 10 64-bit</li><li data-list=\"bullet\" class=\"ql-indent-1\"><span class=\"ql-ui\" contenteditable=\"false\"></span><strong>Processor:</strong> AMD Ryzen 5 2600X, Intel Core i7-6800K</li><li data-list=\"bullet\" class=\"ql-indent-1\"><span class=\"ql-ui\" contenteditable=\"false\"></span><strong>Memory:</strong> 16 GB RAM</li><li data-list=\"bullet\" class=\"ql-indent-1\"><span class=\"ql-ui\" contenteditable=\"false\"></span><strong>Graphics:</strong> AMD Radeon RX 5700, NVIDIA GeForce 1070 Ti</li><li data-list=\"bullet\" class=\"ql-indent-1\"><span class=\"ql-ui\" contenteditable=\"false\"></span><strong>DirectX:</strong> Version 12</li><li data-list=\"bullet\" class=\"ql-indent-1\"><span class=\"ql-ui\" contenteditable=\"false\"></span><strong>Storage:</strong> 125 GB available space</li></ol><p><strong>Recommended:</strong></p><p><br></p><ol><li data-list=\"bullet\" class=\"ql-indent-1\"><span class=\"ql-ui\" contenteditable=\"false\"></span>Requires a 64-bit processor and operating system</li><li data-list=\"bullet\" class=\"ql-indent-1\"><span class=\"ql-ui\" contenteditable=\"false\"></span><strong>OS:</strong> Windows 10 64-bit</li><li data-list=\"bullet\" class=\"ql-indent-1\"><span class=\"ql-ui\" contenteditable=\"false\"></span><strong>Processor:</strong> AMD Ryzen 5 3600X, Intel Core i5-10600K</li><li data-list=\"bullet\" class=\"ql-indent-1\"><span class=\"ql-ui\" contenteditable=\"false\"></span><strong>Memory:</strong> 32 GB RAM</li><li data-list=\"bullet\" class=\"ql-indent-1\"><span class=\"ql-ui\" contenteditable=\"false\"></span><strong>Graphics:</strong> AMD Radeon RX 6800XT or NVIDIA RTX 2080</li><li data-list=\"bullet\" class=\"ql-indent-1\"><span class=\"ql-ui\" contenteditable=\"false\"></span><strong>DirectX:</strong> Version 12</li><li data-list=\"bullet\" class=\"ql-indent-1\"><span class=\"ql-ui\" contenteditable=\"false\"></span><strong>Storage:</strong> 125 GB available space</li></ol><p><br></p>",
                Thumbnail = "/uploads/images/games/f0a968e5/avigameskyrim4.jpg",
                ReleaseDate = new DateTime(2024, 04, 01),
                Status = 1,
                Delete = false,
                CreatedAt = new DateTime(2026, 01, 02),
                PublisherId = 3
            },
            new Game
            {
                GameId = 4,
                Title = "SILENT HILL f – Digital Deluxe",
                Developer = "NeoBards Entertainment (nổi tiếng với các dự án như Resident Evil: Resistance, Re:Verse, Devil May Cry HD Collection)",
                Description = "<h2>DELUXE EDITION &amp; PRE-PURCHASE BONUS</h2><p><img src=\"https://avigames.net/wp-content/uploads/2025/09/671332089955de2c84090411ae83ddbf.\" height=\"450\" width=\"800\"></p><h2>Deluxe Edition Contents</h2><p>– Full Game</p><p>– Digital Artbook *1</p><p>– Digital Soundtrack *1</p><p>– Pink Rabbit Costume *2</p><p>*1</p><p>The Digital Artbook and Digital Soundtrack are included in a bonus application.</p><p>The Digital Soundtrack can be downloaded in MP3 or WAV format from the Steam launcher.</p><p>*2</p><p>This costume changes Hinako's appearance.</p><p>To apply this costume, access a Hokora in the game and select Change Costume from the menu.</p><p>Note:</p><p>-The contents of the SILENT HILL f – Deluxe Upgrade are included in this item. Please be careful to avoid redundant purchases.</p><p>-Digital Soundtrack includes music with lyrics in Japanese.</p><p><img src=\"https://avigames.net/wp-content/uploads/2025/09/73f517192bdffbc5bbb6756dc3093f94.\" height=\"450\" width=\"800\"></p><h2>Pre-purchase Bonuses</h2><p>– White Sailor School Uniform *1</p><p>– Omamori: Peony *2</p><p>– Item Pack *3</p><p>*1</p><p>This costume changes Hinako's appearance.</p><p>To apply this costume, access a Hokora in the game and select Change Costume from the menu.</p><p>*2</p><p>An equipable item.</p><p>To receive this item, access a Hokora in the game and select Bonuses from the menu.</p><p>The Bonuses option will unlock based on game progression.</p><p>*3</p><p>An item pack with three consumable items. This pack can only be claimed once.</p><p>To receive this item, access a Hokora in the game and select Bonuses from the menu.</p><p>The Bonuses option will unlock based on game progression.</p><p>Item Pack contains:</p><p>– 1 Shriveled Abura-age</p><p>– 1 Divine Water</p><p>– 1 First Aid Kit</p><p>Note: The bonuses above do not come included in the SILENT HILL f – Deluxe Upgrade.</p><p><img src=\"https://avigames.net/wp-content/uploads/2025/09/4e5350d38c7e52846c6d5f5d6a1aa77f.\" height=\"450\" width=\"800\"></p><h2>Deluxe Edition Pre-purchase Bonuses</h2><p>– 48-hour Early Access</p><p><img src=\"https://avigames.net/wp-content/uploads/2025/09/18f9fc149caf0f87bd40791e61b26b49.\" height=\"450\" width=\"800\"></p><p><br></p><h2>About This Game</h2><p>In 1960s Japan, Shimizu Hinako's secluded town of Ebisugaoka is consumed by a sudden fog, transforming her home into a haunting nightmare.</p><p>As the town falls silent and the fog thickens, Hinako must navigate the twisted paths of Ebisugaoka, solving complex puzzles and confronting grotesque monsters to survive.</p><p>Immerse yourself into Hinako's world imagined by renowned author Ryukishi07, with entrancing music, including pieces by Akira Yamaoka, and beautiful visuals in a gripping tale of doubt, regret, and inescapable choices. Will Hinako embrace the beauty hidden within terror, or succumb to the madness that lies ahead?</p><p>Discover a new chapter in the SILENT HILL series, blending psychological horror with a haunting Japanese setting.</p><ol><li data-list=\"bullet\" class=\"ql-indent-1\"><span class=\"ql-ui\" contenteditable=\"false\"></span><br></li></ol>",
                Thumbnail = "/uploads/images/games/798ffb77/silent-hill-f-1q1eg.jpg",
                ReleaseDate = new DateTime(2025, 01, 01),
                Status = 1,
                Delete = false,
                CreatedAt = new DateTime(2026, 02, 02),
                PublisherId = 3
            },
            new Game
            {
                GameId = 5,
                Title = "Borderlands 4 Super Deluxe Edition",
                Developer = "Gearbox Software",
                Description = "<h2>DELUXE AND SUPER DELUXE EDITIONS</h2><p> </p><p><img src=\"https://avigames.net/wp-content/uploads/2025/09/7ca62306233938f33d1d363cc16cbb8a.\" height=\"3740\" width=\"1080\"></p><p> </p><h2>Roadmap</h2><p> </p><p><img src=\"https://avigames.net/wp-content/uploads/2025/09/1c99aae6151f75bc1d524230bcf9d690.\" height=\"878\" width=\"1560\"></p><h2>About This Game</h2><p><em>Borderlands 4</em> brings intense action, badass Vault Hunters, and billions of wild and deadly weapons to an all-new planet ruled by a ruthless tyrant.</p><p>Crash into Kairos as one of four new Vault Hunters seeking wealth and glory. Wield powerful Action Skills, customize your build with deep skill trees, and dominate enemies with dynamic movement abilities.</p><p>Break free from the oppressive Timekeeper, a ruthless dictator who dominates the masses from on high. Now a world-altering catastrophe threatens his perfect Order, unleashing Mayhem across the planet.</p><p>Become an unstoppable force of battle, blasting through enemies with an all-new arsenal of outrageous weaponry. Move across the Borderlands like never before—double jumping, gliding, dodging, grappling, and more—dealing death from every direction. Explode each encounter with devastating Action Skills that unleash your Vault Hunter's unique abilities. Craft your perfect build with branching skill trees and a deep, rewarding loot chase full of wild weapons and powerful gear.</p><p>Wreaking havoc across Kairos is awesome alone and even better with friends in 4-player online co-op.* <em>Borderlands 4</em> is designed for co-op from the ground up; whether you're hunting for loot, tackling missions, or wandering freely, level scaling and individual difficulty keeps the party together and having fun.</p><p>Freely explore a vast and dangerous world rife with warring factions. Hop on your hover bike and ride through lush fields, towering peaks, and deadly deserts full of fearsome enemies, dynamic events, and engaging quests with unforgettable characters. Unite the people of Kairos and ignite a revolution, tackling this adventure however you see fit in a seamless <em>Borderlands</em> experience.</p><p><em>*Online play requires Internet connection, and cross-play requires SHiFT Account. Terms apply.</em></p><p><br></p>",
                Thumbnail = "/uploads/images/games/da4ca4f4/borderlands-4-fnnpo.jpg",
                ReleaseDate = new DateTime(2025, 09, 01),
                Status = 1,
                Delete = false,
                CreatedAt = new DateTime(2025, 11, 11),
                PublisherId = 2
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
            new GameCategory { GameCategoryId = 11, GameId = 4, CategoryId = 3, Delete = false },
            new GameCategory { GameCategoryId = 12, GameId = 4, CategoryId = 6, Delete = false },

            // Ubisoft
            new GameCategory { GameCategoryId = 13, GameId = 3, CategoryId = 4, Delete = false },
            new GameCategory { GameCategoryId = 14, GameId = 3, CategoryId = 6, Delete = false },

            // Valve
            new GameCategory { GameCategoryId = 15, GameId = 2, CategoryId = 1, Delete = false },
            new GameCategory { GameCategoryId = 16, GameId = 2, CategoryId = 6, Delete = false },

            // Epic
            new GameCategory { GameCategoryId = 17, GameId = 1, CategoryId = 2, Delete = false },
            new GameCategory { GameCategoryId = 18, GameId = 1, CategoryId = 6, Delete = false }
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
            new GamePlatform { GamePlatformId = 14, GameId = 5, PlatformId = 1, Description = "PC - cập nhật patch hàng tuần.", Status = 1, Delete = false },

            // Ubisoft
            new GamePlatform { GamePlatformId = 15, GameId = 4, PlatformId = 1, Description = "PC (Ubisoft Connect/Steam).", Status = 1, Delete = false },
            new GamePlatform { GamePlatformId = 16, GameId = 3, PlatformId = 2, Description = "PS5/PS4.", Status = 1, Delete = false },
            new GamePlatform { GamePlatformId = 17, GameId = 2, PlatformId = 3, Description = "Xbox Series X|S/Xbox One.", Status = 1, Delete = false },

            // Valve / Epic
            new GamePlatform { GamePlatformId = 18, GameId = 1, PlatformId = 1, Description = "PC (Steam) - ưu tiên 128-tick server/community.", Status = 1, Delete = false },
            new GamePlatform { GamePlatformId = 19, GameId = 5, PlatformId = 1, Description = "PC (Epic) - nhiều chế độ chơi.", Status = 1, Delete = false },
            new GamePlatform { GamePlatformId = 20, GameId = 4, PlatformId = 4, Description = "Nintendo Switch - ưu tiên chế độ hiệu năng.", Status = 1, Delete = false }
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
                CreatedAt = new DateTime(2020, 1, 1),
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
                CreatedAt = new DateTime(2020, 1, 1),
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
                CreatedAt = new DateTime(2020, 1, 1),
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
                CreatedAt = new DateTime(2020, 1, 1),
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
                CreatedAt = new DateTime(2020, 1, 1),
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
                Links = "/seed/files/bf2042-fps-v1.zip",
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
                Links = "/seed/files/bf2042-fps-v1_1.zip",
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
                Links = "/seed/files/bf2042-vi-v1.zip",
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
                Links = "/seed/files/bf2042-vi-v1_1.zip",
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
                Links = "/seed/files/mw2-audio-v2.zip",
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
                Links = "/seed/files/mw2-audio-v2_1.zip",
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
                Links = "/seed/files/warzone-visual-v1.zip",
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
                Links = "/seed/files/warzone-visual-v1_1.zip",
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
                Links = "/seed/files/valorant-aimpack-v1.zip",
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
                Links = "/seed/files/valorant-aimpack-v1_1.zip",
                FileSize = 3_650_000,
                ExtractionPassword = "patchseller",
                InstallationGuide = "Bổ sung bài tập micro-adjust. In checklist hoặc dùng file Notion (link trong guide).",
                Changelog = "- Thêm micro-adjust\n- Tối ưu routine cho rank Đồng/Bạc",
                Status = 1,
                Note = "Ưu tiên tập đúng form hơn là tốc độ.",
                CreateAt = new DateTime(2026, 1, 16),
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

            // Some version images
            new PatchImage { PatchImageId = 10, PatchId = null, PatchVersionId = 2, URL = "/seed/patches/bf2042-fps-v1_1-1.jpg", Name = "Preset NVIDIA", Description = "Ảnh minh họa setting NVIDIA", IsThumbnail = false, Delete = false },
            new PatchImage { PatchImageId = 11, PatchId = null, PatchVersionId = 2, URL = "/seed/patches/bf2042-fps-v1_1-2.jpg", Name = "Preset AMD", Description = "Ảnh minh họa setting AMD", IsThumbnail = false, Delete = false },
            new PatchImage { PatchImageId = 12, PatchId = null, PatchVersionId = 10, URL = "/seed/patches/cs2-config-v1-1.jpg", Name = "Autoexec", Description = "Ví dụ autoexec.cfg", IsThumbnail = false, Delete = false }
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
                CreatedAt = new DateTime(2020, 1, 1),
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
                CreatedAt = new DateTime(2020, 1, 1),
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
                CreatedAt = new DateTime(2020, 1, 1),
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
                CreatedAt = new DateTime(2020, 1, 1),
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
                CreatedAt = new DateTime(2020, 1, 1),
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
                CreatedAt = new DateTime(2020, 1, 1),
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
            new CartItem { CartItemId = 1, CartId = 1, PatchId = 1, Delete = false },
            new CartItem { CartItemId = 2, CartId = 1, PatchId = 1, Delete = false },
            new CartItem { CartItemId = 3, CartId = 2, PatchId = 4, Delete = false },
            new CartItem { CartItemId = 4, CartId = 4, PatchId = 5, Delete = false },
            new CartItem { CartItemId = 5, CartId = 5, PatchId = 5, Delete = false }
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
                CreatedAt = new DateTime(2020, 1, 1),
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
                CreatedAt = new DateTime(2020, 1, 1),
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
                CreatedAt = new DateTime(2020, 1, 1),
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
                CreatedAt = new DateTime(2020, 1, 1),
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
                CreatedAt = new DateTime(2020, 1, 1),
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
            new OrderDetail { OrderDetailID = 3, OrderId = 2, PatchId = 5, Price = 89_000 },
            new OrderDetail { OrderDetailID = 4, OrderId = 2, PatchId = 1, Price = 69_000 }
        );

        modelBuilder.Entity<UserPurchase>().HasData(
            new UserPurchase { UserPurchaseId = 1, UserId = 2, PatchId = 4, PurchasedAt = new DateTime(2026, 1, 11) },
            new UserPurchase { UserPurchaseId = 2, UserId = 2, PatchId = 5, PurchasedAt = new DateTime(2026, 1, 11) },
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
                ReviewId = 3,
                UserId = 1,
                PatchId = 2,
                UserName = "nguyenvana",
                Title = "Việt hóa dễ cài",
                Content = "Copy là chạy, có file restore nên yên tâm. Thuật ngữ dịch ổn.",
                Overall = 4.2,
                CreatedAt = new DateTime(2020, 1, 1),
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
                CreatedAt = new DateTime(2020, 1, 1),
                Status = 1
            }
        );

        modelBuilder.Entity<DownloadLog>().HasData(
            new DownloadLog { DownloadLogId = 1, UserId = 2, PatchVersionId = 7, DownloadedAt = new DateTime(2026, 1, 11, 10, 30, 0) },
            new DownloadLog { DownloadLogId = 2, UserId = 2, PatchVersionId = 9, DownloadedAt = new DateTime(2026, 1, 11, 10, 45, 0) },
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

