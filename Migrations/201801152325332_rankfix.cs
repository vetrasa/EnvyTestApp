namespace EnvyTestApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rankfix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Members", "LastRankId", "dbo.Ranks");
            DropIndex("dbo.Members", new[] { "LastRankId" });
            AlterColumn("dbo.Members", "LastRankId", c => c.Byte());
            CreateIndex("dbo.Members", "LastRankId");
            AddForeignKey("dbo.Members", "LastRankId", "dbo.Ranks", "RankId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Members", "LastRankId", "dbo.Ranks");
            DropIndex("dbo.Members", new[] { "LastRankId" });
            AlterColumn("dbo.Members", "LastRankId", c => c.Byte(nullable: false));
            CreateIndex("dbo.Members", "LastRankId");
            AddForeignKey("dbo.Members", "LastRankId", "dbo.Ranks", "RankId", cascadeDelete: true);
        }
    }
}
