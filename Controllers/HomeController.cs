using QuanLyThongTinBenBaiCho.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyThongTinBenBaiCho.Controllers
{
    public class HomeController : Controller
    {
        public readonly DataContext _context;
        public HomeController()
        {
            _context = new DataContext();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoadTable(string ten, string loai)
        {
            var data = _context.Bens.AsQueryable();
            if (!string.IsNullOrEmpty(ten))
                data = data.Where(x => x.Ten.Contains(ten));
            if(!string.IsNullOrEmpty(loai) && loai != "-1")
                data = data.Where(x => x.Loai == loai);
            return View(data.ToList());
        }
        [HttpGet]
        public ActionResult AddOrUpdate(int id = 0)
        {
            var Loai = new List<string> { "Bến", "Bãi", "Chợ" };
            ViewBag.Loai = new SelectList(Loai) ;
            if (id == 0)
            {
                return View(new Ben());
            }
            else
            {
                var entity = _context.Bens.Where(x => x.BenId == id).FirstOrDefault();
                if (entity == default(Ben))
                    throw new Exception("Không tìm thấy dữ liệu");
                return View(entity);
            }
        }
        [HttpPost]
        public ActionResult AddOrUpdate(Ben model)
        {
            try
            {
                if (model.BenId == 0)
                {
                    _context.Bens.Add(model);
                    _context.SaveChanges();
                    return Json(new GenericMessageVM()
                    {
                        Status = true,
                        Message = $"Thêm thành công!",
                        MessageType = GenericMessage.success,
                        Data = model
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var entity = _context.Bens.Where(x => x.BenId == model.BenId).FirstOrDefault();
                    if (entity == default(Ben))
                        throw new Exception("Không tìm thấy dữ liệu.");
                    entity.Ten = model.Ten;
                    entity.Loai = model.Loai;
                    entity.GiaTri = model.GiaTri;
                    entity.DiaChi = model.DiaChi;
                    entity.MoTa = model.MoTa;
                    entity.ChuQuan = model.ChuQuan;

                    _context.SaveChanges();
                    return Json(new GenericMessageVM()
                    {
                        Status = true,
                        Message = $"Cập nhật thành công!",
                        MessageType = GenericMessage.success,
                        Data = model
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new GenericMessageVM()
                {
                    Status = false,
                    Message = $"{ex.Message}",
                    MessageType = GenericMessage.error
                }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var data = _context.Bens.Where(x => x.BenId == id).FirstOrDefault();
                _context.Bens.Remove(data);
                _context.SaveChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DenHan()
        {
            return View();
        }
        public ActionResult LoadTableDenHan(string ten, string loai)
        {
            var data = _context.Bens.AsQueryable();
            if (!string.IsNullOrEmpty(ten))
                data = data.Where(x => x.Ten.Contains(ten));
            if (!string.IsNullOrEmpty(loai) && loai != "-1")
                data = data.Where(x => x.Loai == loai);

            var result = data.ToList();
            List<BenVM> benVms = new List<BenVM>();
            foreach (var item in result)
            {
                var benVm = new BenVM();
                var ls = _context.LichSus.Where(x => x.BenId == item.BenId).OrderByDescending(x => x.NgayKetThuc).FirstOrDefault();
                if (ls == default(LichSu))
                    continue;
                if (ls.NgayKetThuc > DateTime.Now.AddDays(7))
                    continue;
                benVm.BenId = item.BenId;
                benVm.Ten = item.Ten;
                benVm.Loai = item.Loai;
                benVm.NgayBatDau = ls.NgayBatDau;
                benVm.NgayKetThuc = ls.NgayKetThuc;
                benVm.NgayDauGia = ls.NgayDauGia;
                benVm.SoTienDauGia = ls.GiaTri;
                benVm.TenDonViSuDung = ls.TenDonVi;
                benVm.DiaChi = item.DiaChi;
                benVms.Add(benVm);
            }
            return View(benVms);
        }
    }
}