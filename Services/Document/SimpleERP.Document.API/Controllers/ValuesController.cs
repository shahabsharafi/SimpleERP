using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleERP.Document.API.Infrastructure.Contracts;
using SimpleERP.Document.API.Infrastructure.Data;
using SimpleERP.Document.API.Infrastructure.Models;

namespace SimpleERP.Document.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IUnitOfRepository _uor;
        private readonly IMapper _mapper;

        public ValuesController(IUnitOfRepository uor, IMapper mapper)
        {
            this._uor = uor;
            this._mapper = mapper;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var q = from obj in this._uor.DocumentInfoRepository.Table
                    .Include(o => o.Issuer)
                    .Include(o => o.Domain)
                    .Include(o => o.Type)
                    select this._mapper.Map<DocumentInfoModel>(obj);
            return new string[] { q.FirstOrDefault().IssuerTitle };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
