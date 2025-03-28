﻿using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using System.Data;

namespace ClinicalXPDataConnections.Meta
{
    public interface IRiskData
    {
        public List<Risk> GetRiskList(int icpID);
        public List<Risk> GetRiskListForPatient(int mpi);
        public Risk GetRiskDetails(int riskID);
        public List<Risk> GetRiskListByRefID(int refID);        
    }
    public class RiskData : IRiskData
    {
        private readonly ClinicalContext _clinContext;
        
        public RiskData(ClinicalContext context)
        {
            _clinContext = context;            
        }

        public List<Risk> GetRiskList(int icpID) //Get list of all risk items for an ICP (by IcpID)
        {
            ICPCancer icp = _clinContext.ICPCancer.FirstOrDefault(c => c.ICP_Cancer_ID == icpID);

            IQueryable<Risk> risks = from r in _clinContext.Risk
                       where r.MPI == icp.MPI
                       select r;
           
            return risks.ToList();
        }

        public List<Risk> GetRiskListForPatient(int mpi) //Get list of all risk items for a patient (by MPI)
        {            

            IQueryable<Risk> risks = from r in _clinContext.Risk
                                     where r.MPI == mpi
                                     select r;

            return risks.ToList();
        }

        public Risk GetRiskDetails(int riskID) //Get details of risk item by RiskID
        {
            Risk risk = _clinContext.Risk.FirstOrDefault(c => c.RiskID == riskID);
            return risk;
        }

        public List<Risk> GetRiskListByRefID(int refID) //Get details of risk item by RiskID
        {
            IQueryable<Risk> risk = _clinContext.Risk.Where(c => c.RefID == refID);
            return risk.ToList();
        }       

    }
}
