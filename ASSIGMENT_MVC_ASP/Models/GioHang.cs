using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace ASSIGMENT_MVC_ASP.Models
{
    public class GioHang
    {
        public Dictionary<int,SanPhamGioHang>  SanPhams { get; set; }
        public double TongHoaDon => SanPhams.Values.Sum(m=>m.ThanhTien);
        public int TongSoLuongSanPham => SanPhams.Values.Sum(m => m.SoLuong);
        public GioHang()
        {
            SanPhams = new Dictionary<int, SanPhamGioHang>();
        }
        public class SanPhamGioHang
        {
            public int  MaSP { get; set; }
            public String  TenSP { get; set; }
            public String MieuTa { get; set; }
            public String AnhSP { get; set; }
            public double GiaSP { get; set; }
            public String XuatSu { get; set; }
            public String ChatLieu { get; set; }
            public Models.SanPham.EnumGioiTinh GioiTinh { get; set; }
            
            [ForeignKey("LoaiSanPham")]
            public int MaLoaiSanPham { get; set; }
            [DisplayName("Phong cách")]
            public string LoaiSanPham { get; set; }
            [DisplayName("Số lượng")]
            public int SoLuong { get; set; }
            [DisplayName("Thành tiền")]
            public double ThanhTien => SoLuong * GiaSP;
        }

        public void ThemSanPham(SanPham sp,int sl,bool laCapNhat)
        {
            var SanPhamGioHang = new SanPhamGioHang()
            {
                MaSP = sp.MaSP,
                TenSP = sp.TenSP,
                MieuTa = sp.MieuTa,
                GiaSP = sp.GiaSP,
                AnhSP = sp.AnhSP,
                XuatSu = sp.XuatSu,
                ChatLieu = sp.ChatLieu,
                GioiTinh =  sp.GioiTinh,
                MaLoaiSanPham = sp.MaLoaiSanPham,
                LoaiSanPham = sp.LoaiSanPham.TenLoaiSanPham,
                SoLuong = sl,
            };
            // kiểm tra tồn tại Product có trong giỏ hàng theo id chưa
            var existKey = SanPhams.ContainsKey(sp.MaSP);
            if (!laCapNhat && existKey)
            {
                var existingItem = SanPhams[sp.MaSP];
                SanPhamGioHang.SoLuong += existingItem.SoLuong;
            }

            if (existKey)
            {
                SanPhams[sp.MaSP] = SanPhamGioHang;
            }
            else
            {
                SanPhams.Add(sp.MaSP, SanPhamGioHang);
            }
        }

        public void ThemMotSanPham(SanPham sp)
        {
            ThemSanPham(sp,1,false);
        }
        public void CapNhat(SanPham sp, int sl)
        {
            ThemSanPham(sp, sl, true);
        }

        public void Remove(int MaSP)
        {
            if (SanPhams.ContainsKey(MaSP))
            {
                SanPhams.Remove(MaSP);
            }
        }
    }
}