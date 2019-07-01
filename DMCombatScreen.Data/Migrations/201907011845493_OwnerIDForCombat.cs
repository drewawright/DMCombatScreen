namespace DMCombatScreen.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OwnerIDForCombat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Combat", "OwnerID", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Combat", "OwnerID");
        }
    }
}
