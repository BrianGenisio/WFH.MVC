using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;
using System.Web.Security;
using WFH.Models;

namespace WFH.Controllers.Common
{
    public interface ILoginLogic
    {
        bool Login(dynamic modelState, LoginModel model);
    }

    public class LoginCommon : ILoginLogic
    {
        public bool Login(dynamic modelState, LoginModel model)
        {
            if (modelState.IsValid && Membership.ValidateUser(model.UserName, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                return true;
            }

            return false;
        }
    }
}