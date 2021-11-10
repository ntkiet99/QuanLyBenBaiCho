using QuanLyThongTinBenBaiCho.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace QuanLyThongTinBenBaiCho.Controllers
{
    public class LichSuController : Controller
    {
        public readonly DataContext _context;
        public LichSuController()
        {
            _context = new DataContext();
        }
        public ActionResult Index(int Id)
        {
            ViewBag.BenId = Id;
            var entity = _context.Bens.Where(x => x.BenId == Id).FirstOrDefault();
            ViewBag.Ten = entity.Ten;
            return View();
        }
        public ActionResult LoadTable(int Id)
        {
            var data = _context.LichSus.Where(x => x.BenId == Id).AsQueryable();
            return View(data.ToList());
        }
        [HttpGet]
        public ActionResult AddOrUpdate(int id = 0, int BenId = 0)
        {
            if (id == 0)
            {

                return View(new LichSu() { BenId = BenId });
            }
            else
            {
                var entity = _context.LichSus.Where(x => x.LichSuId == id).FirstOrDefault();
                if (entity == default(LichSu))
                    throw new Exception("Không tìm thấy dữ liệu");
                return View(entity);
            }
        }
        [HttpPost]
        public ActionResult AddOrUpdate(LichSu model)
        {
            try
            {
                if (model.LichSuId == 0)
                {
                    _context.LichSus.Add(model);
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
                    var entity = _context.LichSus.Where(x => x.LichSuId == model.LichSuId).FirstOrDefault();
                    if (entity == default(LichSu))
                        throw new Exception("Không tìm thấy dữ liệu.");
                    entity.NgayKetThuc = model.NgayKetThuc;
                    entity.NgayBatDau = model.NgayBatDau;
                    entity.NgayDauGia = model.NgayDauGia;
                    entity.GiaTri = model.GiaTri;
                    entity.TenDonVi = model.TenDonVi;

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
                var data = _context.LichSus.Where(x => x.LichSuId == id).FirstOrDefault();
                _context.LichSus.Remove(data);
                _context.SaveChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }
    }
}