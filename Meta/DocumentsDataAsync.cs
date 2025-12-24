using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ClinicalXPDataConnections.Meta
{
    public interface IDocumentsDataAsync
    {
        public Task<DocumentsContent> GetDocumentDetails(int id);
        public Task<DocumentsContent> GetDocumentDetailsByDocCode(string docCode);
        public Task<List<Document>> GetDocumentsList();
        public Task<Document> GetDocumentData(string docCode);
    }
    public class DocumentsDataAsync : IDocumentsDataAsync
    {       
        private readonly DocumentContext? _docContext;
        
        public DocumentsDataAsync(DocumentContext docContext)
        {            
            _docContext = docContext;
        }        
        
        
        public async Task<DocumentsContent> GetDocumentDetails(int id) //Get content for a type of standard letter by its ID
        {
            DocumentsContent item = await _docContext.DocumentsContent.FirstAsync(d => d.DocContentID == id);
            return item;
        }

        public async Task<DocumentsContent> GetDocumentDetailsByDocCode(string docCode) //Get content for a type of standard letter by its ID
        {
            DocumentsContent item = await _docContext.DocumentsContent.FirstAsync(d => d.DocCode == docCode);
            return item;
        }

        public async Task<List<Document>> GetDocumentsList() 
        {
            IQueryable<Document> docs = from d in _docContext.Documents
                       where d.TemplateInUseNow == true
                       select d;

            return await docs.ToListAsync();
        }

        public async Task<Document> GetDocumentData(string docCode)
        {
            Document doc = await _docContext.Documents.FirstAsync(d => d.DocCode == docCode);

            return doc;
        }
        
    }
}
