namespace Pansiyon_UI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Odalar")]
    public partial class Odalar
    {
        public int ID { get; set; }

        [Required]
        [StringLength(5)]
        public string OdaNo { get; set; }

        public decimal Fiyat { get; set; }

        public bool MusaitMi { get; set; }
    }
}
