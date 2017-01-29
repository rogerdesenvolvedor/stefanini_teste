namespace ApplicationTest.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CustomerFilter
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int IdRegion { get; set; }
        public int IdCity { get; set; }
        public int IdClassification { get; set; }
        public int IdGender { get; set; }
        public int IdSeller { get; set; }
        public string Name { get; set; }

    }
}
