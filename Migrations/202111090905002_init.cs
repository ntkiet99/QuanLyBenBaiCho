namespace QuanLyThongTinBenBaiCho.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bens",
                c => new
                    {
                        BenId = c.Int(nullable: false, identity: true),
                        Ten = c.String(),
                        Loai = c.String(),
                        DiaChi = c.String(),
                        MoTa = c.String(),
                        GiaTri = c.Single(nullable: false),
                        ChuQuan = c.String(),
                    })
                .PrimaryKey(t => t.BenId);
            
            CreateTable(
                "dbo.LichSus",
                c => new
                    {
                        LichSuId = c.Int(nullable: false, identity: true),
                        NgayDauGia = c.DateTime(nullable: false),
                        GiaTri = c.Single(nullable: false),
                        NgayBatDau = c.DateTime(nullable: false),
                        NgayKetThuc = c.DateTime(nullable: false),
                        TenDonVi = c.String(),
                        BenId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LichSuId)
                .ForeignKey("dbo.Bens", t => t.BenId, cascadeDelete: true)
                .Index(t => t.BenId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LichSus", "BenId", "dbo.Bens");
            DropIndex("dbo.LichSus", new[] { "BenId" });
            DropTable("dbo.LichSus");
            DropTable("dbo.Bens");
        }
    }
}
