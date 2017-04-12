using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureClouderMVC.Configuration
{
    public class AzureStorageSettings
    {
        /// <summary>
        /// Connection string (Account name and Account key)
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Container for files
        /// </summary>
        public string ImagesContainer { get; set; }
    }
}
