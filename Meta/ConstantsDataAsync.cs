using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using Microsoft.EntityFrameworkCore;


namespace ClinicalXPDataConnections.Meta
{
    public interface IConstantsDataAsync
    {
        public Task<string> GetConstant(string constantCode, int constantValue);
    }
    public class ConstantsDataAsync : IConstantsDataAsync
    {        
        private readonly DocumentContext? _docContext;
        
        public ConstantsDataAsync(DocumentContext docContext)
        {
            _docContext = docContext;
        }        
       

        public async Task<string> GetConstant(string constantCode, int constantValue)
        {
            Constant item = await _docContext.Constants.FirstOrDefaultAsync(c => c.ConstantCode == constantCode);
            string returnValue = "";

            if (item != null)
            {
                if (constantValue == 1)
                {
                    returnValue = item.ConstantValue;
                }
                else
                {
                    returnValue = item.ConstantValue2;
                }

                returnValue = returnValue.Trim();
                
            }
            return returnValue;
        }
    }
}
