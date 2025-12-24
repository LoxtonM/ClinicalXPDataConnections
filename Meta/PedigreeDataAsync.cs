using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using Microsoft.EntityFrameworkCore;


namespace ClinicalXPDataConnections.Meta
{
    public interface IPedigreeDataAsync
    {
        public Task<Pedigree> GetPedigree(string pedno);
        public Task<string> GetNextPedigreeNumber();

    }
    public class PedigreeDataAsync : IPedigreeDataAsync
    {
        private readonly ClinicalContext _clinContext;       

        public PedigreeDataAsync(ClinicalContext context)
        {
            _clinContext = context;
        }

        public async Task<Pedigree> GetPedigree(string pedno)
        {
            Pedigree ped = await _clinContext.Pedigrees.FirstAsync(p => p.PEDNO == pedno);

            return ped;
        }

        public async Task<string> GetNextPedigreeNumber()
        {
            var pedNoStrings = await _clinContext.Pedigrees.Select(p => p.PEDNO).ToListAsync();
                        

            int maxPedNo = pedNoStrings.Select(s => int.TryParse(s, out var n) ? n : (int?)null)
                    .Where(n => n.HasValue)
                    .DefaultIfEmpty(0)   // if none are numeric, start from 0
                    .Max()!.Value;

            int next = maxPedNo + 1;
           

            return next.ToString();
        }

    }
}
