using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASSIGMENT_MVC_ASP.Models
{
    public class LoaiSanPham
    {
        [Key]
        public int MaLoaiSanPham { get; set; }

        [DisplayName("Loại sản phẩm")]
        [StringLength(30, ErrorMessage = "Tên loại sản phẩm không được quá 30 ký tự")]
        [Required(ErrorMessage = "Tên loại sản phẩm bắt buộc phải có.")]
        public String TenLoaiSanPham { get; set; }
    }
}