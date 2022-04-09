using Lear.CRS.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lear.CRS.IServices
{
    /// <summary>
    /// IContentServices
    /// </summary>	
    public interface IDictionaryConfigServices
    {
        Task<long> AddDictionaryConfig(AddDictionaryConfigInput input);
        Task<long> UpdateDictionaryConfig(UpdateDictionaryConfigInput input);
        Task<List<DictionaryConfigOutput>> GetDictionaryConfig(DictionaryConfigInput input);
        Task<List<DictionaryConfigKeyValueOutput>> GetDictionaryConfigKeyValue();
        Task<List<DictionaryConfigKeyValueOutput>> GetWeekValue();
        Task<List<DictionaryConfigKeyValueOutput>> GetPeriodValue();

        Task<string> GetDefault(string code);
    }
}
