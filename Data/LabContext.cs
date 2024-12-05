using Microsoft.EntityFrameworkCore;
using ClinicalXPDataConnections.Models;

namespace ClinicalXPDataConnections.Data
{
    public class LabContext : DbContext
    {
        public LabContext(DbContextOptions<LabContext> options) : base(options) { }
        public DbSet<LabDisease> labDisease { get; set; }
        public DbSet<LabDNALab> labDNALab { get; set; }
        public DbSet<LabDNAIndication> labDNAIndication { get; set; }                
        public DbSet<LabDNAReport> labDNAReport { get; set; }
        public DbSet<LabLab> labLab { get; set; }
        public DbSet<LabPatient> labPatient { get; set; }
        public DbSet<LabRefFac> labRefFac { get; set; }
        public DbSet<LabRefPhys> labRefPhys { get; set; }
        public DbSet<LabStaff> labStaff { get; set; }
        public DbSet<LabSendout> labSendout { get; set; }
        public DbSet<LabDNALabData> labDNALabData { get; set; }
    }
}
