using System.Collections.Generic;
using System.Threading.Tasks;
using AssetTracker.Core.Entities;

namespace AssetTracker.Core.Services
{
    public interface IOrganizationService
    {
        Task<bool> Create(Organization item);
        Task<bool> Delete(int id);
        Task<Organization> GetById(int id);
        Task<IEnumerable<Organization>> GetByUserId(int userId);
        Task<bool> Update(Organization item);
    }
}