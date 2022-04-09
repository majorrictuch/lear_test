using Lear.CRS.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lear.CRS.IServices
{
    /// <summary>
    /// ILogServices 
    /// </summary>	
    public interface ILogServices
    {
     
        Task<dynamic> QueryJobLogList(LogInput input);
       

    }
}
