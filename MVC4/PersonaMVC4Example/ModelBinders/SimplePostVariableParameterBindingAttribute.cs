using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using Westwind.Web.WebApi;

namespace PersonaMVC4Example
{
    public class SimplePostVariableParameterBindingAttribute : Attribute, IControllerConfiguration
    {
        public void Initialize(HttpControllerSettings controllerSettings,
                                HttpControllerDescriptor controllerDescriptor)
        {
            controllerSettings.ParameterBindingRules.Insert(0,
                new Func<HttpParameterDescriptor, HttpParameterBinding>(
                    d => new SimplePostVariableParameterBinding(d)));
        }
    }
}