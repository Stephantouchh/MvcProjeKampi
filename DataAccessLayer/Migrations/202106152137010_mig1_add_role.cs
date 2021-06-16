namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig1_add_role : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Writers", "WriterRole", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Writers", "WriterRole", c => c.Boolean(nullable: false));
        }
    }
}
