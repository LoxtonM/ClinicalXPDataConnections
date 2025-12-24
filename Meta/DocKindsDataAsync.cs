using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicalXPDataConnections.Meta
{    
    public interface IDocKindsDataAsync
    {
        public Task<List<DocumentKinds>> GetDocumentKindsList();
    }
    public class DocKindsDataAsync : IDocKindsDataAsync
    {
        private readonly DocumentContext _docContext;

        public DocKindsDataAsync(DocumentContext docContext)
        {
            _docContext = docContext;
        }

        public async Task<List<DocumentKinds>> GetDocumentKindsList()
        {
            IQueryable<DocumentKinds> docs = _docContext.DocumentKinds.OrderBy(d => d.DisplayOrder);

            return await docs.ToListAsync();
        }
    }
}
