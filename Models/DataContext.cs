using System.Data.Entity;

namespace QuanLyThongTinBenBaiCho.Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base("name=ConnectionString")
        {
        }
        public DbSet<LichSu> LichSus { get; set; }
        public DbSet<Ben> Bens { get; set; }
    }
}