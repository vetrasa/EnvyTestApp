namespace EnvyTestApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rank : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        DateJoin = c.DateTime(),
                        DateLastRankChange = c.DateTime(),
                        LastChangedBy = c.String(),
                        RankId = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ranks", t => t.RankId, cascadeDelete: true)
                .Index(t => t.RankId);
            
            CreateTable(
                "dbo.Ranks",
                c => new
                    {
                        RankId = c.Byte(nullable: false),
                        RankName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.RankId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Members", "RankId", "dbo.Ranks");
            DropIndex("dbo.Members", new[] { "RankId" });
            DropTable("dbo.Ranks");
            DropTable("dbo.Members");
        }
    }
}
