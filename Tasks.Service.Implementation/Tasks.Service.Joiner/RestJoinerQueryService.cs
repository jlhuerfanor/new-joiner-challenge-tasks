using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Tasks.Model.Domain;

namespace Tasks.Service.Joiner
{
    public class RestJoinerQueryService : IJoinerQueryService, IDisposable
    {
        private ILogger<RestJoinerQueryService> logger;
        private JsonSerializerOptions jsonOptions;
        private HttpClient client;
        private string serviceUrl;
        private int requestTimeout;

        public RestJoinerQueryService(
            ILogger<RestJoinerQueryService> logger, 
            JsonSerializerOptions jsonOptions, 
            string serviceUrl, 
            int requestTimeout) {

            this.client = new HttpClient();

            this.logger = logger;
            this.jsonOptions = jsonOptions;
            this.serviceUrl = serviceUrl;
            this.requestTimeout = requestTimeout;
        }

        public void Dispose()
        {
            client.Dispose();
        }

        public JoinerProfile GetProfile(long joinerIdNumber)
        {
            var asyncCall = this.GetProfileAsync(joinerIdNumber);
            
            try {
                asyncCall.Wait(requestTimeout);
                return asyncCall.Result;
            } catch(AggregateException ex) {
                if(ex.InnerExceptions.Any((e) => e is JsonException)) {
                    return null;
                } else {
                    var index = 0;
                    foreach (var error in ex.InnerExceptions)
                    {
                        logger.LogError(error, "--------- Exception {0}", index);
                    }
                    throw ex;
                }
            }            
        }

        public async Task<JoinerProfile> GetProfileAsync(long joinerIdNumber) {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            var stream = await client.GetStreamAsync(String.Format("{0}/{1}", serviceUrl, joinerIdNumber));

            //if(stream.Length > 0) {
                return await JsonSerializer.DeserializeAsync<JoinerProfile>(stream, this.jsonOptions);
            //} else {
            //    return null;
            //}
        }
    }

}