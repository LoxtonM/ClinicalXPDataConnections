using Microsoft.EntityFrameworkCore;
using ClinicalXPDataConnections.Models;

namespace ClinicalXPDataConnections.Data
{
    public class DocumentContext : DbContext //The DocumentContext class is the data context for everything to do with letters.
                                             //It contains no clinical data.
    { 
        public DocumentContext(DbContextOptions<DocumentContext> options) : base(options) { }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentsContent> DocumentsContent { get; set; }
        public DbSet<Constant> Constants { get; set; }
        
    }
}