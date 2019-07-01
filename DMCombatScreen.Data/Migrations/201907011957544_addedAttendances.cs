namespace DMCombatScreen.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedAttendances : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendance",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OwnerID = c.Guid(nullable: false),
                        CharacterID = c.Int(nullable: false),
                        CombatID = c.Int(nullable: false),
                        CurrentHP = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Character", t => t.CharacterID, cascadeDelete: true)
                .ForeignKey("dbo.Combat", t => t.CombatID, cascadeDelete: true)
                .Index(t => t.CharacterID)
                .Index(t => t.CombatID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attendance", "CombatID", "dbo.Combat");
            DropForeignKey("dbo.Attendance", "CharacterID", "dbo.Character");
            DropIndex("dbo.Attendance", new[] { "CombatID" });
            DropIndex("dbo.Attendance", new[] { "CharacterID" });
            DropTable("dbo.Attendance");
        }
    }
}
