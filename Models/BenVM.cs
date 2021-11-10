using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyThongTinBenBaiCho.Models
{
    public class BenVM
    {
        public int BenId { get; set; }
        public string Ten { get; set; }
        public string Loai { get; set; }
        public string DiaChi { get; set; }
        public DateTime NgayDauGia { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public float SoTienDauGia { get; set; }
        public string TenDonViSuDung { get; set; }
    }
}