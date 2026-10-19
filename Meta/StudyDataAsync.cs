using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicalXPDataConnections.Meta
{
    public interface IStudyDataAsync
    {
        public Task<List<Study>> GetStudiesForPatient(int mpi);
        public Task<Study> GetStudyDetails(int studyId);
        public Task<List<Studies>> GetStudyList();
        public Task<List<StudyStatus>> GetStudyStatusList();
    }
    public class StudyDataAsync : IStudyDataAsync
    {
        private readonly ClinicalContext _clinicalContext;

        public StudyDataAsync(ClinicalContext context)
        {
            _clinicalContext = context;
        }
        public async Task<List<Study>> GetStudiesForPatient(int mpi)
        {
            IQueryable<Study> studies = _clinicalContext.Study
                .Where(s => s.MPI == mpi);

            return await studies.ToListAsync();
        }

        public async Task<Study> GetStudyDetails(int studyId)
        {
            Study study = await _clinicalContext.Study.FirstOrDefaultAsync(s => s.StudyRecID == studyId);

            return study;
        }

        public async Task<List<Studies>> GetStudyList()
        {
            IQueryable<Studies> studies = _clinicalContext.Studies;

            return await studies.ToListAsync();
        }

        public async Task<List<StudyStatus>> GetStudyStatusList()
        {
            IQueryable<StudyStatus> studyStatuses = _clinicalContext.StudyStatus;

            return await studyStatuses.ToListAsync();
        }
    }
}
