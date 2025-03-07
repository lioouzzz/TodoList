using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ToDo.Filters
{
    public class CustomAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                // 取得目前請求的 URL 作為 ReturnUrl
                var returnUrl = context.HttpContext.Request.Path;
                // 重導到自訂的登入頁面 /Account/Login
                context.Result = new RedirectToActionResult("Login", "Account", new { returnUrl });
            }
            base.OnActionExecuting(context);
        }
    }
}

