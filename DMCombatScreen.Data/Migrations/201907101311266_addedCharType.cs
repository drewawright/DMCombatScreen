namespace DMCombatScreen.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedCharType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Character", "TypeOfCharacter", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Character", "TypeOfCharacter");
        }
    }
}
