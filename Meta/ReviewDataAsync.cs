using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicalXPDataConnections.Meta
{
    public interface IReviewDataAsync
    {
        public Task<List<Review>> GetReviewsList(string username);
        public Task<List<Review>> GetReviewsListAll();
        public Task<List<Review>> GetReviewsListForPatient(int mpi);
        public Task<Review> GetReviewDetails(int id);
    }
    public class ReviewDataAsync : IReviewDataAsync
    {
        private readonly ClinicalContext _clinContext;
        private readonly StaffUserData _staffUser;

        public ReviewDataAsync(ClinicalContext context)
        {
            _clinContext = context;
            _staffUser = new StaffUserData(_clinContext);
        }
       

        public async Task<List<Review>> GetReviewsList(string username) 
        {
            string staffCode = _staffUser.GetStaffMemberDetails(username).STAFF_CODE;

            IQueryable<Review> reviews = from r in _clinContext.Reviews
                          where r.Review_Recipient == staffCode && r.Review_Status == "Pending"
                          orderby r.Planned_Date
                          select r;

            return await reviews.ToListAsync();
        }

        public async Task<List<Review>> GetReviewsListAll()
        {            

            IQueryable<Review> reviews = from r in _clinContext.Reviews
                                         where r.Review_Status == "Pending"
                                         orderby r.Planned_Date
                                         select r;

            return await reviews.ToListAsync();
        }


        public async Task<List<Review>> GetReviewsListForPatient(int mpi)
       {
            IQueryable<Review> reviews = from r in _clinContext.Reviews
                                         where r.MPI == mpi && r.Review_Status == "Pending"
                                         orderby r.Planned_Date
                                         select r;

            return await reviews.ToListAsync();
        }

        public async Task<Review> GetReviewDetails(int id)
        {
            Review review = await _clinContext.Reviews.FirstAsync(r => r.ReviewID == id);

            return review;
        }        
    }
}
