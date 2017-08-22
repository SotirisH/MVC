using Aurora.Core.Data;
using Aurora.SMS.Service;
using Aurora.SMS.Web.Areas.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Aurora.SMS.Web.Areas.Api.Controllers
{
    public class TemplatesController : ApiController
    {
        private readonly ITemplateServices _templateServices;
        private readonly IUnitOfWork _UoW;

        /// <summary>
        /// Sample Web API
        /// </summary>
        /// <param name="templateServices"></param>
        /// <param name="UoW"></param>
        /// <remarks>Noramlly the _UoW.Commit should be invoked at the end of the bussiness transaction</remarks>
        public TemplatesController(ITemplateServices templateServices,
                                    IUnitOfWork UoW)
        {
            _templateServices = templateServices;
            _UoW = UoW;
        }
        // Templates
        public IHttpActionResult GetAll()
        {
            try
            {
                var result = _templateServices.GetAll();
                if (result == null || !result.Any())
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }
                var model = AutoMapper.Mapper.Map<IEnumerable<SmsTemplateModel>>(result);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // Templates/1
        public IHttpActionResult GetTemplate(int id)
        {
            try
            {
                var result = _templateServices.GetById(id);
                if (result == null)
                {
                    return NotFound();
                }
                var model = AutoMapper.Mapper.Map<SmsTemplateModel>(result);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetTemplateByName(string name)
        {
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        /// <summary>
        /// Updates an existing template
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IHttpActionResult Put([FromBody] SmsTemplateModel model)
        {
            Validate(model);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(model.RowVersion))
            {
                return BadRequest("This method is not allowed to Create an entity. Please use the POST verb");
            }

            try
            {
                _templateServices.Update(AutoMapper.Mapper.Map<EFModel.Template>(model));
                _UoW.Commit();
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        /// <summary>
        /// Creates a new template
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IHttpActionResult Post([FromBody] SmsTemplateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!string.IsNullOrWhiteSpace(model.RowVersion))
            {
                return BadRequest("This method is not allowed to update an entity. Please use the PUT verb");
            }

            try
            {
                _templateServices.CreateTemplate(AutoMapper.Mapper.Map<EFModel.Template>(model));
                _UoW.Commit();
                return StatusCode(HttpStatusCode.Created);
                //TODO:Return the new value
                //return Created<SmsTemplateModel>(Request.RequestUri + newCust.ID.ToString(), newCust);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        public IHttpActionResult Delete(int id)
        {
            try
            {
                _templateServices.DeleteTemplate(id);
                _UoW.Commit();
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
