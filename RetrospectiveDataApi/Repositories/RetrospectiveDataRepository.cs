using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RetrospectiveDataApi.Exceptions;
using RetrospectiveDataApi.Models;
using RetrospectiveDataApi.Repositories.Interfaces;

namespace RetrospectiveDataApi.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="IRetrospectiveDataRepository" />
    public class RetrospectiveDataRepository : IRetrospectiveDataRepository
    {
        private ILogger<RetrospectiveDataRepository> _logger;
        private readonly IsoDateTimeConverter _jsonDateTimeConverter;

        public RetrospectiveDataRepository(ILogger<RetrospectiveDataRepository> logger)
        {
            _logger = logger;
            _jsonDateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" };
        }

        /// <summary>
        /// Gets the retrospective data.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        public async Task<IList<RetrospectiveData>> Get(string filePath)
        {
            var retrospectiveDataList = new List<RetrospectiveData>();
            var file = Path.Combine(Directory.GetCurrentDirectory(), filePath);

            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    string json = await sr.ReadToEndAsync();
                    
                    retrospectiveDataList = JsonConvert.DeserializeObject<List<RetrospectiveData>>(json, _jsonDateTimeConverter);
                }
            }
            catch (IOException ex)
            {
                _logger.LogError(ex, "File related exception");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "General exception");
                throw;
            }

            return retrospectiveDataList;
        }

        public async Task<RetrospectiveData> Add(string filePath, RetrospectiveData retrospectiveData)
        {
            try
            {
                var retrospectiveDataList = Get(filePath).Result;

                var itemExists = retrospectiveDataList.Any(x => x.Name.Equals(retrospectiveData.Name, StringComparison.OrdinalIgnoreCase));

                if (itemExists)
                    throw new RetrospectiveDataException("Insert failed as this retrospective item already exists");

                retrospectiveDataList.Add(retrospectiveData);
                var data = JsonConvert.SerializeObject(retrospectiveDataList, new StringEnumConverter(), _jsonDateTimeConverter);

                using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true))
                using (StreamWriter sw = new StreamWriter(stream))
                {
                    await sw.WriteLineAsync(data);
                }
            }
            catch (RetrospectiveDataException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
            catch (IOException ex)
            {
                _logger.LogError(ex, "File related exception");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }

            return retrospectiveData;
        }

        public async Task<Feedback> Add(string filePath, string retrospectiveName, Feedback feedback)
        {
            RetrospectiveData retrospectiveData;
            try
            {
                var retrospectiveDataList = Get(filePath).Result;

                retrospectiveData = retrospectiveDataList.FirstOrDefault(x => x.Name.Equals(retrospectiveName, StringComparison.OrdinalIgnoreCase));

                if (retrospectiveData == null)
                    throw new RetrospectiveDataException("Cannot insert feedback as the retrospective item don't exist");

                var itemExists = retrospectiveData.Feedback.Any(x => x.Name.Equals(feedback.Name, StringComparison.OrdinalIgnoreCase));

                if (itemExists)
                    throw new RetrospectiveDataException("Insert failed as feedback is already provided by this customer");

                retrospectiveData.Feedback.Add(feedback);

                var data = JsonConvert.SerializeObject(retrospectiveDataList, new StringEnumConverter(), _jsonDateTimeConverter);
                using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true))
                using (StreamWriter sw = new StreamWriter(stream))
                {
                    await sw.WriteLineAsync(data);
                }
            }
            catch (RetrospectiveDataException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
            catch (IOException ex)
            {
                _logger.LogError(ex, "File related exception");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }

            return feedback;
        }
    }
}
