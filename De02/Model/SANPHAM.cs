namespace De02.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SANPHAM")]
    public partial class SANPHAM
    {
        [Key]
        [StringLength(6)]
        public string MASP { get; set; }

        [Required]
        [StringLength(30)]
        public string TENSP { get; set; }

        public DateTime NGAYNHAP { get; set; }

        [Required]
        [StringLength(2)]
        public string MALOAI { get; set; }

        public virtual LOAISP LOAISP { get; set; }

        public virtual LOAISP LOAISP1 { get; set; }
    }
}
