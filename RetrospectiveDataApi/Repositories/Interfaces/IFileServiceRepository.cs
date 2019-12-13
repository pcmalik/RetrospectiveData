using RetrospectiveDataApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RetrospectiveDataApi.Repositories.Interfaces
{
    public interface IFileServiceRepository
    {
        Task<IList<RetrospectiveData>> GetRetrospectiveData(string filePath);
        Task<RetrospectiveData> AddRetrospectiveData(string filePath, RetrospectiveData retrospectiveData);
    }
}
