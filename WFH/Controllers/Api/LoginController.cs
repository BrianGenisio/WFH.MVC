using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using WFH.Models;
using System.Net.Http;
using WFH.Controllers.Common;

namespace WFH.Controllers.Api
{
    public class LoginController : ApiController
    {
        private ILoginLogic loginCommon;

        public LoginController(ILoginLogic loginCommon)
        {
            this.loginCommon = loginCommon;
        }

        public HttpResponseMessage PostLogin(LoginModel model)
        {
            if (loginCommon.Login(ModelState, model))
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}