using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            var list = this._uor.DocumentInfoRepository.TableNoTracking;
            var rows = list.Select(obj => this._mapper.Map<DocumentInfoModel>(obj)).OrderByDescending(o => o.Id);
            //var rows = list.Select(obj => new DocumentInfoModel()
            //{
            //    Id = obj.Id,
            //    No = obj.No,
            //    Subject = obj.Subject,
            //    Text = obj.Text,
            //    FilePath = obj.FilePath,
            //    DateOfRelease = obj.DateOfRelease,
            //    Creator = obj.Creator,
            //    DateOfCreate = obj.DateOfCreate,
            //    Modifier = obj.Modifier,
            //    DateOfModify = obj.DateOfModify,
            //    DomainId = obj.DomainId,
            //    DomainTitle = obj.Domain.Title,
            //    IssuerId = obj.IssuerId,
            //    IssuerTitle = obj.Issuer.Title,
            //    TypeId = obj.TypeId,
            //    TypeTitle = obj.Type.Title
            //}).OrderByDescending(o => o.Id);
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
            //var model = new DocumentInfoModel() {
            //    Id = obj.Id,
            //    No = obj.No,
            //    Subject = obj.Subject,
            //    Text = obj.Text,
            //    FilePath = obj.FilePath,
            //    DateOfRelease = obj.DateOfRelease,
            //    Creator = obj.Creator,
            //    DateOfCreate = obj.DateOfCreate,
            //    Modifier = obj.Modifier,
            //    DateOfModify = obj.DateOfModify,
            //    DomainId = obj.DomainId,
            //    DomainTitle = obj.Domain.Title,
            //    IssuerId = obj.IssuerId,
            //    IssuerTitle = obj.Issuer.Title,
            //    TypeId = obj.TypeId,
            //    TypeTitle = obj.Type.Title
            //};
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

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<DocumentInfoModel>> Post([FromBody] DocumentInfoModel model, CancellationToken cancellationToken)
        {
            //var entity = this._mapper.Map<Contract>(model);
            DocumentInfo entity = this._mapper.Map<DocumentInfo>(model);// new DocumentInfo() { No = model.No };
            await this._uor.DocumentInfoRepository.AddAsync(entity, cancellationToken);
            var newModel = this._mapper.Map<DocumentInfoModel>(entity);// new DocumentInfoModel() { Id = entity.Id, No = entity.No };
            return Ok(newModel);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<ActionResult<DocumentInfoModel>> Put(long id, [FromBody] DocumentInfoModel model, CancellationToken cancellationToken)
        {
            var entity = await this._uor.DocumentInfoRepository.GetByIdAsync(cancellationToken, id);
            this._mapper.Map(model, entity);
            //entity.No = model.No;

            await this._uor.DocumentInfoRepository.UpdateAsync(entity, cancellationToken);
            var newModel = this._mapper.Map<DocumentInfoModel>(entity);//new DocumentInfoModel() { Id = entity.Id, No = entity.No };
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
