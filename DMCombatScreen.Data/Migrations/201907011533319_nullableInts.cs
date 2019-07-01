namespace DMCombatScreen.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullableInts : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Character", "MaxHP", c => c.Int());
            AlterColumn("dbo.Character", "InitiativeRoll", c => c.Int());
            AlterColumn("dbo.Character", "InitiativeModifier", c => c.Int());
            AlterColumn("dbo.Character", "InitiativeAbilityScore", c => c.Int());
            AlterColumn("dbo.Character", "TotalInitiative", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Character", "TotalInitiative", c => c.Int(nullable: false));
            AlterColumn("dbo.Character", "InitiativeAbilityScore", c => c.Int(nullable: false));
            AlterColumn("dbo.Character", "InitiativeModifier", c => c.Int(nullable: false));
            AlterColumn("dbo.Character", "InitiativeRoll", c => c.Int(nullable: false));
            AlterColumn("dbo.Character", "MaxHP", c => c.Int(nullable: false));
        }
    }
}
