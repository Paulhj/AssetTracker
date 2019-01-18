using System.Collections.Generic;
using System.Threading.Tasks;
using AssetTracker.Core.Entities;

namespace AssetTracker.Core.Services
{
    public interface IUserService
    {
        Task<bool> Create(User item);
        Task<bool> Delete(int id);
        User GetById(int id);
        Task<User> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetByOrganizationId(int organizationId);
        Task<bool> Update(User item);
        bool UserBelongToOrganization(int userId, int organizationId);
    }
}