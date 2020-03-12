﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleERP.Document.API.Infrastructure.Contracts;
using SimpleERP.Document.API.Infrastructure.Data;
using SimpleERP.Document.API.Infrastructure.Model;

namespace SimpleERP.Document.API.Controllers
{


    //[Authorize(Policy = "SellerPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentInfosController : ControllerBase
    {
        IUnitOfRepository _uor;

        public DocumentInfosController(IUnitOfRepository uor)
        {
            this._uor = uor;
        }

        [HttpGet]
        public IQueryable Get()
        {
            var list = this._uor.DocumentInfoRepository.TableNoTracking;
            var rows = list.Select(obj => new DocumentInfoModel()
            {
                Id = obj.Id,
                No = obj.No,
                Subject = obj.Subject,
                Text = obj.Text,
                FilePath = obj.FilePath,
                DateOfRelease = obj.DateOfRelease,
                Creator = obj.Creator,
                DateOfCreate = obj.DateOfCreate,
                Modifier = obj.Modifier,
                DateOfModify = obj.DateOfModify,
                DomainId = obj.DomainId,
                DomainTitle = obj.Domain.Title,
                IssuerId = obj.IssuerId,
                IssuerTitle = obj.Issuer.Title,
                TypeId = obj.TypeId,
                TypeTitle = obj.Type.Title
            }).OrderByDescending(o => o.Id);
            return rows;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentInfoModel>> Get(long id, CancellationToken cancellationToken)
        {
            var obj = await this._uor.DocumentInfoRepository.GetByIdAsync(cancellationToken, id);
            //var model = this._mapper.Map<ContractModel>(obj);
            var model = new DocumentInfoModel() {
                Id = obj.Id,
                No = obj.No,
                Subject = obj.Subject,
                Text = obj.Text,
                FilePath = obj.FilePath,
                DateOfRelease = obj.DateOfRelease,
                Creator = obj.Creator,
                DateOfCreate = obj.DateOfCreate,
                Modifier = obj.Modifier,
                DateOfModify = obj.DateOfModify,
                DomainId = obj.DomainId,
                DomainTitle = obj.Domain.Title,
                IssuerId = obj.IssuerId,
                IssuerTitle = obj.Issuer.Title,
                TypeId = obj.TypeId,
                TypeTitle = obj.Type.Title
            };
            return Ok(model);
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<DocumentInfoModel>> Post([FromBody] DocumentInfoModel model, CancellationToken cancellationToken)
        {
            //var entity = this._mapper.Map<Contract>(model);
            DocumentInfo entity = new DocumentInfo() { No = model.No };
            await this._uor.DocumentInfoRepository.AddAsync(entity, cancellationToken);
            var newModel = new DocumentInfoModel() { Id = entity.Id, No = entity.No };
            return Ok(newModel);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<ActionResult<DocumentInfoModel>> Put(long id, [FromBody] DocumentInfoModel model, CancellationToken cancellationToken)
        {
            var entity = await this._uor.DocumentInfoRepository.GetByIdAsync(cancellationToken, id);
            //this._mapper.Map(model, entity);
            entity.No = model.No;

            await this._uor.DocumentInfoRepository.UpdateAsync(entity, cancellationToken);
            var newModel = new DocumentInfoModel() { Id = entity.Id, No = entity.No };
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
    }
}
