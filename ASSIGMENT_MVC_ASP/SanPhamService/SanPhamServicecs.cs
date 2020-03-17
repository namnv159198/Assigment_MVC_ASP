using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ASSIGMENT_MVC_ASP.Data;
using ASSIGMENT_MVC_ASP.Models;

namespace ASSIGMENT_MVC_ASP.SanPhamService
{
    public class SanPhamServicecs
    {
        private static ASSIGMENT_MVC_ASPContext db = new ASSIGMENT_MVC_ASPContext();

        public List<SanPham> TimKiemTheoTen(String keyword,int MaLoaiSanPham= 0)
        {
            var sanPhams = db.SanPhams.Include(s=>s.LoaiSanPham);

            sanPhams = from sanPham in db.SanPhams
                join c in db.LoaiSanPhams on sanPham.MaLoaiSanPham equals c.MaLoaiSanPham
                select sanPham;

            var DanhSachSanPhamTimKiem = new List<SanPham>();

            if (!String.IsNullOrEmpty(keyword)) // kiểm tra chuỗi tìm kiếm có rỗng/null hay không
            {
                sanPhams = db.SanPhams.Where(s => s.TenSP.Contains(keyword)); //lọc theo chuỗi tìm kiếm
            }
            if (MaLoaiSanPham != 0)
            {
                sanPhams = sanPhams.Where(x => x.MaLoaiSanPham == MaLoaiSanPham);
            }


            foreach (var item in sanPhams)
            {
                SanPham temp = new SanPham();
                temp.LoaiSanPham = item.LoaiSanPham;
                temp.TenSP = item.TenSP;
                temp.MieuTa = item.MieuTa;
                temp.GioiTinh = item.GioiTinh;
                temp.GiaSP = item.GiaSP;
                temp.AnhSP = item.AnhSP;
                temp.XuatSu = item.XuatSu;
                temp.ChatLieu = item.ChatLieu;
                DanhSachSanPhamTimKiem.Add(temp);
            }

            return DanhSachSanPhamTimKiem;
        }

       
    }
}