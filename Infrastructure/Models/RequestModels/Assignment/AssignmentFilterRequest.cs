using Infrastructure.Models.CommonModels;
using Utils.HelperFuncs;

namespace Infrastructure.Models.RequestModels.Assignment
{
    public class AssignmentFilterRequest : PaginationRequest
    {
        private static readonly DateTime _currentDate = DateTime.UtcNow;
        public DateTime StartDate { get; set; } = DateHelper.GetFirstDayOfMonth(_currentDate);

        public DateTime EndDate { get; set; } = DateHelper.GetLastDayOfMonth(_currentDate);

        public Guid? Department { get; set; }

        public Guid? Project { get; set; }
    }
}
