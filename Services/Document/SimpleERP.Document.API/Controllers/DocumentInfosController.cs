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

        public DocumentInfosController(IUnitOfRepository uor, IMapper mapper)
        {
            this._uor = uor;
            this._mapper = mapper;
        }

        [HttpGet]
        public IQueryable Get()
        {
            var list = this._uor.DocumentInfoRepository.TableNoTracking
                    .Include(o => o.Issuer)
                    .Include(o => o.Domain)
                    .Include(o => o.Type);
            var rows = list.Select(obj => this._mapper.Map<DocumentInfoModel>(obj)).OrderByDescending(o => o.Id);
            return rows;
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

        [Route("upload")]
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(long id, [FromForm] IFormFile file, CancellationToken cancellationToken)
        {
            if (file == null || file.Length < 1)
                return BadRequest("there is no fle to upload");
            try
            {                
                var entity = await this._uor.DocumentInfoRepository.GetByIdAsync(cancellationToken, id);
                using (var fileStream = new MemoryStream())
                {
                    await file.CopyToAsync(fileStream);
                    entity.DocumentFiles.Add(new DocumentFile()
                    {
                        Name = file.FileName,
                        Content = fileStream.ToArray(),
                        ContentType = file.ContentType
                    });
                }                
                await this._uor.DocumentInfoRepository.UpdateAsync(entity, cancellationToken);
            }
            catch (Exception)
            {
                return BadRequest("upload file is faled");
            }            
            return Ok();
        }

        [Route("download")]
        [HttpGet()]
        public async Task<ActionResult> GetImage(long id, int fileId, CancellationToken cancellationToken)
        {
            var entity = await this._uor.DocumentInfoRepository.GetByIdAsync(cancellationToken, id);
            if (entity == null)
                return BadRequest("id is not found");
            var file = entity.DocumentFiles.FirstOrDefault(o => o.Id == fileId);
            if (file == null)
                return BadRequest("file id is not found");
            return base.File(file.Content, file.ContentType);
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<DocumentInfoModel>> Post(DocumentInfoModel model, CancellationToken cancellationToken)
        {            
            DocumentInfo entity = this._mapper.Map<DocumentInfo>(model);
            await this._uor.DocumentInfoRepository.AddAsync(entity, cancellationToken);
            var newModel = this._mapper.Map<DocumentInfoModel>(entity);
            return Ok(newModel);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<ActionResult<DocumentInfoModel>> Put(long id, DocumentInfoModel model, CancellationToken cancellationToken)
        {
            var entity = await this._uor.DocumentInfoRepository.GetByIdAsync(cancellationToken, id);
            this._mapper.Map(model, entity);
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
