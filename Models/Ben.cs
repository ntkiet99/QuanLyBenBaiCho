using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyThongTinBenBaiCho.Models
{
    public class Ben
    {
        public int BenId { get; set; }
        public string Ten { get; set; }
        public string Loai { get; set; }
        public string DiaChi { get; set; }
        public string MoTa { get; set; }
        public float GiaTri { get; set; }
        public string ChuQuan { get; set; }
        public ICollection<LichSu> LichSus { get; set; }
    }
}