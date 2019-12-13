using RetrospectiveDataApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RetrospectiveDataApi.Repositories.Interfaces
{
    public interface IFileServiceRepository
    {
        Task<IEnumerable<RetrospectiveData>> GetRetrospectiveData(string filePath);
        Task<RetrospectiveData> AddRetrospectiveData(RetrospectiveData retrospectiveData);
    }
}
