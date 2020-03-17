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
using ASSIGMENT_MVC_ASP.SanPhamService;

namespace ASSIGMENT_MVC_ASP.Controllers
{
    public class SanPhamsController : Controller
    {
        private ASSIGMENT_MVC_ASPContext db = new ASSIGMENT_MVC_ASPContext();
        private static SanPhamServicecs sanPhamService = new SanPhamServicecs();

        // GET: SanPhams
        public ActionResult Index()
        {
            var DanhMuc = from c in db.LoaiSanPhams select c;
            ViewBag.MaLoaiSanPham = new SelectList(DanhMuc, "MaLoaiSanPham", "TenLoaiSanPham"); // danh sách Category
            var sanPham = db.SanPhams.Include(s => s.LoaiSanPham);
            return View(sanPham.ToList());
        }

        public ActionResult ListProduct()
        {
            var sanPham = db.SanPhams.Include(s => s.LoaiSanPham);
            return PartialView(sanPham.ToList());
        }
        public ActionResult TimKiemTheoTen(string keyword)
        {
            
            var sanPham = db.SanPhams.Include(s => s.LoaiSanPham);
            return PartialView("ListProduct", sanPham.Where(s=>s.TenSP.Contains(keyword)));
        }
        public ActionResult TimKiemTheoDanhMuc(int MaLoaiSanPham = 0)
        {
            var sanPham = db.SanPhams.Include(s => s.LoaiSanPham);

            if (MaLoaiSanPham != 0)
            {
                sanPham = sanPham.Where(p => p.MaLoaiSanPham == MaLoaiSanPham);
            }

            return PartialView("ListProduct", sanPham);
        }
        public ActionResult SapXepSanPham(String sx)
        {
            var sanPham = db.SanPhams.Include(s => s.LoaiSanPham);
            switch (sx)
            {
                // 3.1 Nếu biến sortOrder sắp giảm thì sắp giảm theo LinkName
                case "Giam":
                    sanPham = db.SanPhams.OrderByDescending(s => s.GiaSP);
                    break;

                // 3.2 Mặc định thì sẽ sắp tăng
                default:
                    sanPham = db.SanPhams.OrderBy(s => s.GiaSP);
                    break;
            }

            return PartialView("ListProduct", sanPham);
        }
        public ActionResult TimTheoGioiTinh(int GioiTinh = 0)
        {
            var sanPham = db.SanPhams.Include(s => s.LoaiSanPham);

            if (GioiTinh  == 1)
            {
                sanPham = sanPham.Where(p => p.GioiTinh == SanPham.EnumGioiTinh.Trai);
            }
            if (GioiTinh == 2)
            {
                sanPham = sanPham.Where(p => p.GioiTinh == SanPham.EnumGioiTinh.Gai);
            }

            return PartialView("ListProduct", sanPham);
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

       
        
    }
}
