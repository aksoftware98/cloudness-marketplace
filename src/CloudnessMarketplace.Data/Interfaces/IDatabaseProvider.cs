using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudnessMarketplace.Data.Interfaces
{
    public interface IDatabaseProvider
    {
        /// <summary>
        /// Creates the requried containers
        /// </summary>
        /// <returns></returns>
        Task InitializeDbAsycn();
    }
}
