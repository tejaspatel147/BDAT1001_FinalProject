using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApiProject.Model;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace WebApiProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "member,admin")]
    public class MemberDataController : BaseAuthenticatedController
    {
        private static IEnumerable<MockDataModel> Data;
        public MemberDataController()
        {
            if (Data == null)
            {
                var fileName = Environment.CurrentDirectory + "/MOCK_DATA.json";
                var fileContent = System.IO.File.ReadAllText(fileName);
                Data = JsonConvert.DeserializeObject<IEnumerable<MockDataModel>>(fileContent);
            }
        }

        [HttpGet]
        public IEnumerable<MockDataModel> Get()
        {
            return Data;
        }

        [HttpGet]
        [Route("getById")]
        public MockDataModel GetById(int id)
        {
            return Data.FirstOrDefault(x => x.Id == id);
        }

        [HttpGet]
        [Route("getByName")]
        public IEnumerable<MockDataModel> GetByName(string name)
        {
            return Data.Where(x => x.FirstName.Equals(name, StringComparison.OrdinalIgnoreCase) || x.LastName.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        [HttpGet]
        [Route("getByIpRange")]
        public IEnumerable<MockDataModel> GetByIpRange(string prefix)
        {
            return Data.Where(x => x.IpAddress.StartsWith(prefix, StringComparison.OrdinalIgnoreCase));
        }
    }
}
