using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;


namespace ClinicalXPDataConnections.Meta
{
    public interface IPedigreeData
    {
        public Pedigree GetPedigree(string pedno);
        public string GetNextPedigreeNumber();

    }
    public class PedigreeData : IPedigreeData
    {
        private readonly ClinicalContext _clinContext;       

        public PedigreeData(ClinicalContext context)
        {
            _clinContext = context;
        }

        public Pedigree GetPedigree(string pedno)
        {
            Pedigree ped = _clinContext.Pedigrees.FirstOrDefault(p => p.PEDNO == pedno);

            return ped;
        }

        public string GetNextPedigreeNumber()
        {
            IQueryable<Pedigree> pedigrees = _clinContext.Pedigrees;
            List<int> pednums = new List<int>();

            foreach (var p in pedigrees) //believe it or not, this is the ONLY way to order by PEDNO - because any attempts to parse it in the Linq refuses to work.
            {
                int i;
                if (Int32.TryParse(p.PEDNO, out i))
                {
                    pednums.Add(i);
                }
            }

            //string pednumber = _clinContext.Pedigrees.OrderByDescending(p => (p.PEDNO)).FirstOrDefault().PEDNO;
            int pednumber = pednums.OrderByDescending(x => x).First();
            pednumber += 1;
            string newPedNo = pednumber.ToString();

            return newPedNo;
        }

    }
}
