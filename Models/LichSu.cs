using System;

namespace QuanLyThongTinBenBaiCho.Models
{
    public class LichSu
    {
        public int LichSuId { get; set; }
        public DateTime NgayDauGia { get; set; } = DateTime.Now;
        public float GiaTri { get; set; }
        public DateTime NgayBatDau { get; set; } = DateTime.Now;
        public DateTime NgayKetThuc { get; set; } = DateTime.Now;
        public string TenDonVi { get; set; }
        public int BenId { get; set; }
        public Ben Bens { get; set; }
    }
}