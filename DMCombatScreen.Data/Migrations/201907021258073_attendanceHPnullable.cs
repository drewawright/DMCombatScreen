namespace DMCombatScreen.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class attendanceHPnullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Attendance", "CurrentHP", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Attendance", "CurrentHP", c => c.Int(nullable: false));
        }
    }
}
