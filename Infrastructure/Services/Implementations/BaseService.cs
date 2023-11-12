using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Utils.UnitOfWork.Interfaces;

namespace Infrastructure.Services.Implementations
{
    public class BaseService
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMemoryCache _memoryCache;
        protected readonly IMapper _mapper;

        protected BaseService(IUnitOfWork unitOfWork, IMemoryCache memoryCache, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _memoryCache = memoryCache;
            _mapper = mapper;
        }
    }
}
