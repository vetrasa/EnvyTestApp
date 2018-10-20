namespace EnvyTestApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class previousrank : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "LastRankId", c => c.Byte(nullable: false));
            CreateIndex("dbo.Members", "LastRankId");
            AddForeignKey("dbo.Members", "LastRankId", "dbo.Ranks", "RankId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Members", "LastRankId", "dbo.Ranks");
            DropIndex("dbo.Members", new[] { "LastRankId" });
            DropColumn("dbo.Members", "LastRankId");
        }
    }
}
