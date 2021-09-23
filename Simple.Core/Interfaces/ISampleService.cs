using Simple.Intrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Core.Interfaces
{
    public interface ISampleService
    {
        Task<List<SampleEntity>> GetAll();
    }
}
