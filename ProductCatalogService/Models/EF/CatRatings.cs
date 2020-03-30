using System;
using System.Collections.Generic;

namespace ProductCatalogService.Models.EF
{
    public partial class CatRatings
    {
        public CatRatings()
        {
            Comments = new HashSet<Comments>();
            Qualification = new HashSet<Qualification>();
        }

        public int IdRating { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public ICollection<Comments> Comments { get; set; }
        public ICollection<Qualification> Qualification { get; set; }
    }
}
