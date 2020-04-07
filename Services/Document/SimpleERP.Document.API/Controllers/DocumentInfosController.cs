using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SimpleERP.Document.API.Infrastructure.Contracts;
using SimpleERP.Document.API.Infrastructure.Data;
using SimpleERP.Document.API.Infrastructure.Models;
using SimpleERP.Libraries.Infrastructure.Excel;
using SimpleERP.Libraries.Infrastructure.QueryHandler;

namespace SimpleERP.Document.API.Controllers
{


    //[Authorize(Policy = "SellerPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentInfosController : ControllerBase, IQuerybleController
    {
        private readonly IUnitOfRepository _uor;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _hostingEnvironment;

        public DocumentInfosController(IUnitOfRepository uor, IMapper mapper, IHostingEnvironment environment)
        {
            this._uor = uor;
            this._mapper = mapper;
            this._hostingEnvironment = environment;
        }

        [HttpGet]
        public IQueryable Get()
        {
            var q = from obj in this._uor.DocumentInfoRepository.Table
                    .Include(o => o.Issuer)
                    .Include(o => o.Domain)
                    .Include(o => o.Type)
                    orderby obj.Id
                    select this._mapper.Map<DocumentInfoModel>(obj);
            return q;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentInfoModel>> Get(long id, CancellationToken cancellationToken)
        {
            var obj = await this._uor.DocumentInfoRepository.GetByIdAsync(cancellationToken, id);
            await this._uor.DocumentInfoRepository.LoadReferenceAsync(obj, o => o.Issuer, cancellationToken);
            await this._uor.DocumentInfoRepository.LoadReferenceAsync(obj, o => o.Domain, cancellationToken);
            await this._uor.DocumentInfoRepository.LoadReferenceAsync(obj, o => o.Type, cancellationToken);
            var model = this._mapper.Map<DocumentInfoModel>(obj);
            return Ok(model);
        }

        [Route("issuers")]
        [HttpGet]
        public IQueryable GetAllIssuers()
        {
            return this._uor.IssuerRepository.TableNoTracking;
        }

        [Route("domains")]
        [HttpGet]
        public IQueryable GetAllDomains()
        {
            return this._uor.DomainRepository.TableNoTracking;
        }

        [Route("types")]
        [HttpGet]
        public IQueryable GetAllTypes()
        {
            return this._uor.TypeRepository.TableNoTracking;
        }

        private async Task<string> UploadFile(IFormFile file)
        {
            string filePath = null;
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            if (file.Length > 0)
            {
                filePath = Path.Combine(uploads, file.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
            return filePath;
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<DocumentInfoModel>> Post([FromForm] string modelJson, [FromForm] IFormFile file, CancellationToken cancellationToken)
        {
            DocumentInfoModel model = JsonConvert.DeserializeObject<DocumentInfoModel>(modelJson);
            DocumentInfo entity = this._mapper.Map<DocumentInfo>(model);
            string filePath = await UploadFile(file); ;
            if (filePath != null)
            {
                entity.FilePath = filePath;
            }
            await this._uor.DocumentInfoRepository.AddAsync(entity, cancellationToken);
            var newModel = this._mapper.Map<DocumentInfoModel>(entity);
            return Ok(newModel);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<ActionResult<DocumentInfoModel>> Put(long id, [FromForm] string modelJson, [FromForm] IFormFile file, CancellationToken cancellationToken)
        {
            DocumentInfoModel model = JsonConvert.DeserializeObject<DocumentInfoModel>(modelJson);
            var entity = await this._uor.DocumentInfoRepository.GetByIdAsync(cancellationToken, id);
            this._mapper.Map(model, entity);
            string filePath = await UploadFile(file); ;
            if (filePath != null)
            {
                entity.FilePath = filePath;
            }
            await this._uor.DocumentInfoRepository.UpdateAsync(entity, cancellationToken);
            var newModel = this._mapper.Map<DocumentInfoModel>(entity);
            return Ok(newModel);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id, CancellationToken cancellationToken)
        {
            var entity = await this._uor.DocumentInfoRepository.GetByIdAsync(cancellationToken, id);
            await this._uor.DocumentInfoRepository.DeleteAsync(entity, cancellationToken);
            return Ok();
        }

        [NonAction]
        public IQueryable GetQuery()
        {
            return this._uor.DocumentInfoRepository.Table;
        }

        [NonAction]
        public async Task<ActionResult> DeleteFromQueryAsync(IQueryable query, CancellationToken cancellationToken = default)
        {
            //this model get from GetQury method 
            if (query is IQueryable<DocumentInfo> q)
            {
                var list = q.ToList();
                await this._uor.DocumentInfoRepository.DeleteRangeAsync(list, cancellationToken);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [NonAction]
        public async Task<ActionResult> DeleteByIdAsync(string[] ids, CancellationToken cancellationToken = default)
        {
            var list = this._uor.DocumentInfoRepository.Table.Where(o => ids.Contains(o.Id.ToString())).ToList();
            await this._uor.DocumentInfoRepository.DeleteRangeAsync(list, cancellationToken);
            return Ok();
        }

        [NonAction]
        public void CreateExcel(IQueryable query, IExcelHelper excelHelper)
        {
            //this model come from ui list requested model
            if (query is IQueryable<DocumentInfoModel> q)
            {
                var list = q.ToList();
                excelHelper.CreateExcel<DocumentInfoModel>("sheet1", list);
            }
        }
    }
}
