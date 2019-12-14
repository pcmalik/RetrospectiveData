using RetrospectiveDataApi.Entities;
using RetrospectiveDataApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RetrospectiveDataApi.Repositories.Interfaces
{
    public interface IRetrospectiveDataRepository
    {
        Task<IList<RetrospectiveData>> Get(string filePath);
        Task<RetrospectiveData> Add(string filePath, RetrospectiveData retrospectiveData);
        Task<Feedback> Add(string filePath, string retrospectiveName, Feedback feedback);
    }
}
