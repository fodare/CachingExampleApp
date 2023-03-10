using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class SampleDataAccess
    {
        private readonly IMemoryCache _memoryCache;
        public SampleDataAccess(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public List<EmployeeModel> GetEmployees()
        {
            List<EmployeeModel> output = new();

            output.Add(new() { FirstName = "Damilare", LastName = "Test" });
            output.Add(new() { FirstName = "Bosun", LastName = "Muller" });
            output.Add(new() { FirstName = "Bobo", LastName = "James" });
            output.Add(new() { FirstName = "Dare", LastName = "Jane" });

            Thread.Sleep(5000);
            return output;
        }

        public async Task<List<EmployeeModel>> GetEmployeesAsync()
        {
            List<EmployeeModel> output = new();

            output.Add(new() { FirstName = "Damilare", LastName = "Test" });
            output.Add(new() { FirstName = "Bosun", LastName = "Muller" });
            output.Add(new() { FirstName = "Bobo", LastName = "James" });
            output.Add(new() { FirstName = "Dare", LastName = "Jane" });

            await Task.Delay(5000);
            return output;
        }

        public async Task<List<EmployeeModel>> GetEmployeesCached()
        {
            List<EmployeeModel> output;
            output = _memoryCache.Get<List<EmployeeModel>>("employees");
            if (output is null)
            {
                output = new();

                output.Add(new() { FirstName = "Damilare", LastName = "Test" });
                output.Add(new() { FirstName = "Bosun", LastName = "Muller" });
                output.Add(new() { FirstName = "Bobo", LastName = "James" });
                output.Add(new() { FirstName = "Dare", LastName = "Jane" });

                await Task.Delay(5000);

                _memoryCache.Set("employees", output, TimeSpan.FromMinutes(1));
            }
            return output;
        }
    }
}
