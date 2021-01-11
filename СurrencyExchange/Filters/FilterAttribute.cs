using LoggerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace СurrencyExchange.Filters
{
    public  class FilterAttribute : Attribute, IActionFilter//, IFilterMetadata
    {
        public ILoggerManager _logger { get; }
        public FilterAttribute(ILoggerManager logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var action = context.RouteData.Values["action"];
            var controller = context.RouteData.Values["controller"];

            if (!context.ModelState.IsValid)
            {
                _logger.LogError($"Invalid model state for the object. Controller:{ controller}, action: { action}");
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            }
        }
    }
}
