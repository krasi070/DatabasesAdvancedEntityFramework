namespace _02.SalesDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SalesAddDefaultAge : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Customers", "Age", s => s.Int(defaultValue: 20));
        }
        
        public override void Down()
        {
            AlterColumn("Customers", "Age", s => s.Int(defaultValue: null));
        }
    }
}