using System;
using System.Collections.Generic;

namespace ProductCatalogService.Models.EF
{
    public partial class Qualification
    {
        public int Id { get; set; }
        public int IdProduct { get; set; }
        public int IdRating { get; set; }
        public int Count { get; set; }
        public DateTime DateUpdate { get; set; }

        public Products IdProductNavigation { get; set; }
        public CatRatings IdRatingNavigation { get; set; }
    }
}
