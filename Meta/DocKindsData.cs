using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;

namespace ClinicalXPDataConnections.Meta
{    
    public interface IDocKindsData
    {
        public List<DocumentKinds> GetDocumentKindsList();
    }
    public class DocKindsData : IDocKindsData
    {
        private readonly DocumentContext _docContext;

        public DocKindsData(DocumentContext docContext)
        {
            _docContext = docContext;
        }

        public List<DocumentKinds> GetDocumentKindsList()
        {
            IQueryable<DocumentKinds> docs = _docContext.DocumentKinds.OrderBy(d => d.DisplayOrder);

            return docs.ToList();
        }
    }
}
