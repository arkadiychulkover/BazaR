namespace BazaR.Data
{
    public class DbMaker
    {
        private readonly AppDbContext context;

        public DbMaker(AppDbContext context)
        {
            this.context = context;
        }

        public void Make()
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        //public void MakeCategories() 
        //{
        //    var files = Directory.GetFiles("wwwroot/SVGAssets/assets", "*category.svg");
        //    foreach (var file in files) 
        //    {
        //        string fileName = Path.GetFileNameWithoutExtension(file);
        //        List<string> parts = fileName.Split('-').ToList();
        //        if (parts.Count >= 3) 
        //        {
        //            string name = parts[1];
        //            string icon = Path.GetFileName(file);
        //            Category category = new Category { Name = name, Icon = icon };
        //            context.Categories.Add(category);
        //        }
        //    }
        //}
    }
}
