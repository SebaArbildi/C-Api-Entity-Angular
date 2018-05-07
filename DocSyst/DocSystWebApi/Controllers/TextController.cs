﻿using DocSystBusinessLogicInterface.AuthorizationBusinessLogicInterface;
using DocSystBusinessLogicInterface.DocumentStructureLogicInterface;
using DocSystWebApi.Models.DocumentStructureModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace DocSystWebApi.Controllers
{
    public class TextController : ApiController
    {
        private ITextBusinessLogic TextBusinessLogic { get; set; }
        private IAuthorizationBusinessLogic AuthorizationBusinessLogic { get; set; }

        public TextController(ITextBusinessLogic textBusinessLogic, IAuthorizationBusinessLogic authorizationBusinessLogic)
        {
            TextBusinessLogic = textBusinessLogic;
            AuthorizationBusinessLogic = authorizationBusinessLogic;
        }

        // GET: api/Text
        public IHttpActionResult Get()
        {
            try
            {
                Utils.IsAValidToken(Request, AuthorizationBusinessLogic);
                Utils.HasAdminPermissions(Request, AuthorizationBusinessLogic);
                var texts = TextBusinessLogic.GetTexts();
                IList<TextModel> textsModel = TextModel.ToModel(texts).ToList();
                return Ok(textsModel);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: api/Text/5
        public IHttpActionResult Get([FromUri] Guid id)
        {
            try
            {
                Utils.IsAValidToken(Request, AuthorizationBusinessLogic);
                Utils.HasAdminPermissions(Request, AuthorizationBusinessLogic);
                var text = TextBusinessLogic.GetText(id);
                return Ok(TextModel.ToModel(text));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: api/Text
        public IHttpActionResult Post([FromBody]TextModel textModel)
        {
            try
            {
                Utils.IsAValidToken(Request, AuthorizationBusinessLogic);
                Utils.HasAdminPermissions(Request, AuthorizationBusinessLogic);
                TextBusinessLogic.AddText(textModel.ToEntity());
                return CreatedAtRoute("DefaultApi", new { textModel.Id }, textModel);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/Text
        public IHttpActionResult Put([FromBody]TextModel textModel)
        {
            try
            {
                Utils.IsAValidToken(Request, AuthorizationBusinessLogic);
                Utils.HasAdminPermissions(Request, AuthorizationBusinessLogic);
                TextBusinessLogic.ModifyText(textModel.ToEntity());
                return Ok("Text Modified");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/Text/5
        public IHttpActionResult Delete([FromUri] Guid id)
        {
            try
            {
                Utils.IsAValidToken(Request, AuthorizationBusinessLogic);
                Utils.HasAdminPermissions(Request, AuthorizationBusinessLogic);
                TextBusinessLogic.DeleteText(id);
                return Ok("Text deleted");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
