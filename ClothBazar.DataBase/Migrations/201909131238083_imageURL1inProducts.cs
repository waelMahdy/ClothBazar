namespace ClothBazar.DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imageURL1inProducts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ImageURL1", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ImageURL1");
        }
    }
}
