namespace DMCombatScreen.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class movedTotalInitiativeFromCharacterToAttendance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Attendance", "CurrentInitiative", c => c.Int());
            DropColumn("dbo.Character", "TotalInitiative");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Character", "TotalInitiative", c => c.Int());
            DropColumn("dbo.Attendance", "CurrentInitiative");
        }
    }
}
