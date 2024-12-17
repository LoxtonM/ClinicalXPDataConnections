using Microsoft.VisualBasic;

namespace ClinicalXPDataConnections.Meta
{
    public interface IAgeCalculator
    {
        public int DateDifferenceYear(DateTime startDate, DateTime endDate);
        public int DateDifferenceWeek(DateTime startDate, DateTime endDate);
        public int DateDifferenceDay(DateTime startDate, DateTime endDate);
    }

    public class AgeCalculator : IAgeCalculator
    {        

        public AgeCalculator(){}

        public int DateDifferenceYear(DateTime startDate, DateTime endDate)
        {            
            int dateDiff = endDate.Year - startDate.Year;
            if (endDate.Month > startDate.Month && endDate.Day > startDate.Day)
            {
                dateDiff = dateDiff -= 1;
            }

            return dateDiff;
        }

        

        public int DateDifferenceDay(DateTime startDate, DateTime endDate)
        {
            int dateDiff = (endDate - startDate).Days;

            return dateDiff;
        }


        public int DateDifferenceWeek(DateTime startDate, DateTime endDate)
        {
            int dateDiff = DateDifferenceDay(startDate, endDate) / 7;

            return dateDiff;
        }

    }
}
