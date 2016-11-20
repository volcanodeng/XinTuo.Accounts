using System.Web.Http.ModelBinding;

namespace XinTuo.Accounts.Controllers.Api
{
    public static class ModelValidHelper
    {
        public static bool ModelValid(ModelStateDictionary modelState,out string err)
        {
            err = "";
            if (!modelState.IsValid)
            {
                foreach (var s in modelState)
                {
                    foreach (var e in s.Value.Errors)
                    {
                        err += e.ErrorMessage;
                    }
                }
            }

            return modelState.IsValid;
        }
    }
}