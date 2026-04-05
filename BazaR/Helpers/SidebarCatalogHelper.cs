
using BazaR.Models;
using Microsoft.AspNetCore.Mvc;

namespace BazaR.Helpers
{
    /// <summary>
    /// Каталог у бічному меню: фіксований порядок і макетні назви для кореневих категорій (1–19).
    /// </summary>
    public static class SidebarCatalogHelper
    {
        public static readonly int[] RootCategorySidebarOrder =
        {
            1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19
        };

        public static List<Category> GetSidebarRootCategories(IEnumerable<Category> allCategories)
        {
            var roots = allCategories
                .Where(c => c.ParentCategoryId == null)
                .ToDictionary(c => c.Id);
            var list = new List<Category>(RootCategorySidebarOrder.Length);
            foreach (var id in RootCategorySidebarOrder)
            {
                if (roots.TryGetValue(id, out var cat))
                    list.Add(cat);
            }
            return list;
        }

        /// <summary>Текст пункту як у макеті (узгоджено з сідом Categories).</summary>
        public static string GetSidebarRootDisplayName(Category category)
        {
            return category.Id switch
            {
                17 => "Тури та відпочинок",
                _ => category.Name
            };
        }

        /// <summary>18/19 — вітрина; інші — сторінка гілки категорії.</summary>
        public static string GetSidebarCategoryHref(IUrlHelper url, Category category)
        {
            return category.Id switch
            {
                18 => url.Action("Browse", "Site", new { sort = "price_desc" }) ?? "#",
                19 => url.Action("Browse", "Site") ?? "#",
                _ => url.Action("CategoryPage", "Site", new { category = category.Id }) ?? "#"
            };
        }
    }
}