using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASSIGMENT_MVC_ASP.Data;
using ASSIGMENT_MVC_ASP.Models;

namespace ASSIGMENT_MVC_ASP.Controllers
{
    public class GioHangController : Controller
    {
        private static ASSIGMENT_MVC_ASPContext db = new ASSIGMENT_MVC_ASPContext();

        private static string SessionGioHang = "GIO_HANG";
        // GET: GioHang
        public ActionResult Index()
        {
            return View("GioHang",GetGioHang());
        }

        public ActionResult ThemSanPham(int msp,int sl)
        {
            // check san pham trong db
            var checksp = db.SanPhams.FirstOrDefault(m => m.MaSP == msp);

            if (checksp == null)
            {
                return new HttpNotFoundResult();
            }

            var gioHang = GetGioHang();

            gioHang.ThemSanPham(checksp,sl,false);
            SetGioHang(gioHang);
            return RedirectToAction("Index","GioHang");
        }

        public ActionResult XoaSanPham(int msp)
        {
            
            var gioHang = GetGioHang();
            gioHang.Remove(msp);
            SetGioHang(gioHang);
            return RedirectToAction("Index", "GioHang");
        }

        public ActionResult CapNhatGioHang(int msp,int sl)
        {
            // check san pham trong db
            var checksp = db.SanPhams.FirstOrDefault(m => m.MaSP == msp);

            if (checksp == null)
            {
                return new HttpNotFoundResult();
            }
            var gioHang = GetGioHang();
            gioHang.CapNhat(checksp, sl);
            SetGioHang(gioHang);
            return RedirectToAction("Index", "GioHang");
        }
        public ActionResult XaGioHang()
        {
            XoaTatCaSanPham();
            return Redirect("Index");
        }

        private GioHang GetGioHang()
        {
            GioHang gioHang = null;
            // Kiểm tra sự tòn tại của gio hang da co trong session
            if (Session[SessionGioHang] != null)
            {
                try
                {
                    //Ép kiểu đối tượng lấy được  về kiểu GioHang
                    gioHang = Session[SessionGioHang] as GioHang;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            if (gioHang == null)
            {
                gioHang = new GioHang();
            }
            return gioHang;
        }

        private void SetGioHang(GioHang gioHang)
        {
            Session[SessionGioHang] = gioHang;
        }
        private void XoaTatCaSanPham()
        {
            Session[SessionGioHang] = null;
        }
    }
}