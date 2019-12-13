using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RetrospectiveDataApi.Models;
using RetrospectiveDataApi.Repositories.Interfaces;

namespace RetrospectiveDataApi.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="IFileServiceRepository" />
    public class FileServiceRepository : IFileServiceRepository
    {
        private ILogger<FileServiceRepository> _logger;

        public FileServiceRepository(ILogger<FileServiceRepository> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Gets the retrospective data.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        public async Task<IEnumerable<RetrospectiveData>> GetRetrospectiveData(string filePath)
        {
            var retrospectiveDataList = new List<RetrospectiveData>();
            var file = Path.Combine(Directory.GetCurrentDirectory(), filePath);

            try
            {
                using (StreamReader r = new StreamReader(file))
                {
                    string json = await r.ReadToEndAsync();
                    retrospectiveDataList = JsonConvert.DeserializeObject<List<RetrospectiveData>>(json);
                }
            }
            catch (IOException ioEx)
            {
                _logger.LogError(ioEx, "File related exception");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "General exception");
                throw;
            }

            return retrospectiveDataList;
        }

        public async Task<RetrospectiveData> AddRetrospectiveData(RetrospectiveData retrospectiveData)
        {
            try
            {
                using (StreamReader r = new StreamReader("test"))
                {
                    string json = await r.ReadToEndAsync();
                }
            }
            catch (IOException ioEx)
            {
                _logger.LogError(ioEx, "File related exception");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "General exception");
                throw;
            }

            return retrospectiveData;
        }

    }
}
