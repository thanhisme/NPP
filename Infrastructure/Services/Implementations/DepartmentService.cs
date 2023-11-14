using AutoMapper;
using Entities;
using Infrastructure.Models.RequestModels.PermissionGroup;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Utils.UnitOfWork.Interfaces;

namespace Infrastructure.Services.Implementations
{
    public class DepartmentService : BaseService, IDepartmentService
    {
        private readonly IGenericRepository<Department> _departmentRepository;

        public DepartmentService(
            IUnitOfWork unitOfWork,
            IMemoryCache memoryCache,
            IMapper mapper
        ) : base(unitOfWork, memoryCache, mapper)
        {
            _departmentRepository = unitOfWork.Repository<Department>();
        }

        public async Task<Guid> Create(CreateDepartmentRequest req)
        {
            var department = _mapper.Map<Department>(req);

            _departmentRepository.Create(department);
            await _unitOfWork.SaveChangesAsync();

            return department.Id;
        }

        public Department? GetById(Guid id)
        {
            return _departmentRepository.GetOne(
                (permission) => permission.Id == id
            );
        }

        public List<Department> GetMany(int page, int pageSize)
        {
            return _departmentRepository.GetMany(page, pageSize);
        }
    }
}
