namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_talent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Talents",
                c => new
                    {
                        TalentId = c.Int(nullable: false, identity: true),
                        TalentName = c.String(),
                        Range = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.TalentId);
            
            DropTable("dbo.TalentCardSkills");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TalentCardSkills",
                c => new
                    {
                        SkillId = c.Int(nullable: false, identity: true),
                        Skill = c.String(maxLength: 20),
                        SkillPoint = c.String(maxLength: 3),
                    })
                .PrimaryKey(t => t.SkillId);
            
            DropTable("dbo.Talents");
        }
    }
}
