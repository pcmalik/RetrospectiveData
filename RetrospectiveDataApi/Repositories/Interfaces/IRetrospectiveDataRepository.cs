using RetrospectiveDataApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RetrospectiveDataApi.Repositories.Interfaces
{
    public interface IRetrospectiveDataRepository
    {
        Task<IList<RetrospectiveFeedback>> Get(string filePath);
        Task<RetrospectiveFeedback> Add(string filePath, RetrospectiveFeedback retrospectiveData);
        Task<Feedback> Add(string filePath, string retrospectiveName, Feedback feedback);
    }
}
