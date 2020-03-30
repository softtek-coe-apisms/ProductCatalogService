using System;
using System.Collections.Generic;

namespace ProductCatalogService.Models.EF
{
    public partial class Comments
    {
        public int IdComent { get; set; }
        public int IdProduct { get; set; }
        public int IdRating { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public DateTime DateCommnet { get; set; }

        public Products IdProductNavigation { get; set; }
        public CatRatings IdRatingNavigation { get; set; }
    }
}
