using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MunicipalTrashProgram
{
    public class DoWork
    {
        public DoWork()
        {

        }
        public double ComputeBill(int numberOfBills, double weeklyCost)
        {
            return numberOfBills * weeklyCost;
        }
        public static IHtmlString SerializeObject(object value)
        {
            using (var stringWriter = new StringWriter())
            using (var jsonWriter = new JsonTextWriter(stringWriter))
            {
                var serializer = new Newtonsoft.Json.JsonSerializer
                {
                    // Let's use camelCasing as is common practice in JavaScript
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                // We don't want quotes around object names
                jsonWriter.QuoteName = true;
                serializer.Serialize(jsonWriter, value);

                return new HtmlString(stringWriter.ToString());
            }
        }

    }
}