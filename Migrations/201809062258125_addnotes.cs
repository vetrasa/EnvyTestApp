namespace EnvyTestApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnotes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "Notes", c => c.String(maxLength: 280));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "Notes");
        }
    }
}
