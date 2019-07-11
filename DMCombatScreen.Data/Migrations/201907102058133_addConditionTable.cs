namespace DMCombatScreen.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addConditionTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Condition",
                c => new
                    {
                        ConditionID = c.Int(nullable: false, identity: true),
                        ConditionName = c.String(),
                    })
                .PrimaryKey(t => t.ConditionID);
            
            CreateTable(
                "dbo.AttendanceCondition",
                c => new
                    {
                        AttendanceID = c.Int(nullable: false),
                        ConditionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AttendanceID, t.ConditionID })
                .ForeignKey("dbo.Attendance", t => t.AttendanceID, cascadeDelete: true)
                .ForeignKey("dbo.Condition", t => t.ConditionID, cascadeDelete: true)
                .Index(t => t.AttendanceID)
                .Index(t => t.ConditionID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AttendanceCondition", "ConditionID", "dbo.Condition");
            DropForeignKey("dbo.AttendanceCondition", "AttendanceID", "dbo.Attendance");
            DropIndex("dbo.AttendanceCondition", new[] { "ConditionID" });
            DropIndex("dbo.AttendanceCondition", new[] { "AttendanceID" });
            DropTable("dbo.AttendanceCondition");
            DropTable("dbo.Condition");
        }
    }
}
