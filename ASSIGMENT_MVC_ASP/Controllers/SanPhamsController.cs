using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASSIGMENT_MVC_ASP.Data;
using ASSIGMENT_MVC_ASP.Models;

namespace ASSIGMENT_MVC_ASP.Controllers
{
    public class SanPhamsController : Controller
    {
        private ASSIGMENT_MVC_ASPContext db = new ASSIGMENT_MVC_ASPContext();

        // GET: SanPhams
        public ActionResult Index(string TimKiem, int MaLoaiSanPham = 0)
        {
            return View(DanhSachSanPham(TimKiem, MaLoaiSanPham));
        }


        // GET: SanPhams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.MaLoaiSanPham = new SelectList(db.LoaiSanPhams, "MaLoaiSanPham", "TenLoaiSanPham");
            return View();
        }


        // POST: SanPhams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,TenSP,MieuTa,AnhSP,GiaSP,XuatSu,ChatLieu,GioiTinh,MaLoaiSanPham")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLoaiSanPham = new SelectList(db.LoaiSanPhams, "MaLoaiSanPham", "TenLoaiSanPham", sanPham.MaLoaiSanPham);
            return View(sanPham);
        }

        // GET: SanPhams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoaiSanPham = new SelectList(db.LoaiSanPhams, "MaLoaiSanPham", "TenLoaiSanPham", sanPham.MaLoaiSanPham);
            return View(sanPham);
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,TenSP,MieuTa,AnhSP,GiaSP,XuatSu,ChatLieu,GioiTinh,MaLoaiSanPham")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoaiSanPham = new SelectList(db.LoaiSanPhams, "MaLoaiSanPham", "TenLoaiSanPham", sanPham.MaLoaiSanPham);
            return View(sanPham);
        }

        // GET: SanPhams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private List<SanPham> DanhSachSanPham(String keyword, int MaLoaiSanPham = 0)
        {

            //1. Tạo danh sách danh mục để hiển thị ở giao diện View thông qua DropDownList
            var danhmuc = from c in db.LoaiSanPhams select c;
            ViewBag.MaLoaiSanPham = new SelectList(danhmuc, "MaLoaiSanPham", "TenLoaiSanPham"); // danh sách Category
            var sanPhams = db.SanPhams.Include(s => s.LoaiSanPham);
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
                sanPhams = db.SanPhams.Where(x => x.MaLoaiSanPham == MaLoaiSanPham);
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
