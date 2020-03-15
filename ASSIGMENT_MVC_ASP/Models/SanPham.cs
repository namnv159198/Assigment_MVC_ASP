using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASSIGMENT_MVC_ASP.Models
{
    public class SanPham
    {
        [Key]
        public int MaSP { get; set; }

        [DisplayName("Tên sản phâm")]
        [StringLength(40,ErrorMessage = "Tên sản phẩm không được quá 40 ký tự")]
        [Required(ErrorMessage = "Tên sản phẩm bắt buộc phải có.")]
        public String TenSP { get; set; }


        [DisplayName("Miêu tả")]
        public String MieuTa { get; set; }


        [DisplayName("Ảnh sản phẩm")]
        [Required(ErrorMessage = "Ảnh sản phẩm bắt buộc phải có.")]
        public String AnhSP { get; set; }


        [DisplayName("Giá sản phẩm")]
        [Required(ErrorMessage = "Giá sản phẩm bắt buộc phải có.")]
        [ Range(50000, 10000000, ErrorMessage = "Giá của một sản phẩm phải phù hợp, giá của bạn ảo quá ! xin hãy nhập lại !")] 
        public double GiaSP { get; set; }


        [DisplayName("Xuất sứ")]
        [Required(ErrorMessage = "Xuất sứ bắt buộc phải có.")]
        public String XuatSu { get; set; }


        [DisplayName("Chất liệu")]
        [Required(ErrorMessage = "Chất liệu bắt buộc phải có.")]
        public String ChatLieu { get; set; }


        [DisplayName("Giới Tính")]
        [Required(ErrorMessage = "Giới tính bắt buộc phải có.")]
        public EnumGioiTinh GioiTinh { get; set; }
        public enum EnumGioiTinh
        {
            Trai = 1,
            Gai = 2
        }
        [ForeignKey("LoaiSanPham")]
        public int MaLoaiSanPham { get; set; }
        public virtual LoaiSanPham LoaiSanPham { get; set; }

       

    }
}