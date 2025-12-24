using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicalXPDataConnections.Meta
{
    public interface IDiseaseDataAsync
    {
        public Task<List<Disease>> GetDiseaseList();
        public Task<List<Diagnosis>> GetDiseaseListByPatient(int mpi);
        public Task<List<DiseaseStatus>> GetStatusList();
        public Task<Diagnosis> GetDiagnosisDetails(int id);
    }
    public class DiseaseDataAsync : IDiseaseDataAsync
    {
        private readonly ClinicalContext _clinContext;        
        public DiseaseDataAsync(ClinicalContext context)
        {
            _clinContext = context;
        }
        
        public async Task<List<Disease>> GetDiseaseList() //Get list of all diseases
        {
            IQueryable<Disease> items = from i in _clinContext.Diseases
                        orderby i.DESCRIPTION
                        select i;           

            return await items.ToListAsync();
        } 

        public async Task<List<Diagnosis>> GetDiseaseListByPatient(int mpi) //Get list of all diseases recorded against a patient
        {            

            IQueryable<Diagnosis> items = from i in _clinContext.Diagnosis
                        where i.MPI == mpi
                        orderby i.DESCRIPTION
                        select i;

            return await items.ToListAsync();
        }

        public async Task<List<DiseaseStatus>> GetStatusList() //Get list of all possible disease statuses
        {
            IQueryable<DiseaseStatus> items = from i in _clinContext.DiseaseStatusList
                        select i;
            
            return await items.ToListAsync();
        }
        
        public async Task<Diagnosis> GetDiagnosisDetails(int id) //Get details of diagnosis by the diagnosis ID
        {
            Diagnosis diagnosis = await _clinContext.Diagnosis.FirstAsync(i => i.ID == id);

            return diagnosis;
        }
        
        public async Task<List<Disease>> GetClinicalIndicationsList()
        {
            IQueryable<Disease> items = from i in _clinContext.Diseases
                                        where i.EXCLUDE_CLINIC == 0
                                        orderby i.DESCRIPTION
                                        select i;

            return await items.ToListAsync();
        }

    }
}
