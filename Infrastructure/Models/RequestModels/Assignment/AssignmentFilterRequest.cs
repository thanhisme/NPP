using Infrastructure.Models.CommonModels;
using Utils.HelperFuncs;

namespace Infrastructure.Models.RequestModels.Assignment
{
    public class AssignmentFilterRequest : PaginationRequest
    {
        private static readonly DateTime currentDate = DateTime.Now;

        public DateTime? StartDate { get; set; } = DateTimeHelper.GetFirstDayOfMonth(currentDate);

        public DateTime? EndDate { get; set; } = DateTimeHelper.GetFirstDayOfMonth(currentDate);

        public Guid? Department { get; set; }

        public Guid? Project { get; set; }
    }
}
