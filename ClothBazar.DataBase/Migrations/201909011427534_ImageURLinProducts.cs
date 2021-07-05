namespace ClothBazar.DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageURLinProducts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ImageURL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ImageURL");
        }
    }
}
