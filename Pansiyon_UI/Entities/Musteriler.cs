namespace Pansiyon_UI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Musteriler")]
    public partial class Musteriler
    {
        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        public string Isim { get; set; }

        [Required]
        [StringLength(20)]
        public string Soyisim { get; set; }

        [Required]
        [StringLength(11)]
        public string TCNo { get; set; }

        [Required]
        [StringLength(15)]
        public string Telefon { get; set; }

        public string Email { get; set; }

        [StringLength(5)]
        public string Cinsiyet { get; set; }
    }
}
