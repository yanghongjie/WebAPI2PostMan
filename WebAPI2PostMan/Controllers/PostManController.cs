using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using Newtonsoft.Json;
using WebAPI2PostMan.Areas.HelpPage;
using WebAPI2PostMan.Models;

namespace WebAPI2PostMan.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("PostMan")]
    public class PostManController : ApiController
    {
        private const string Host = "http://localhost:11488/";

        /// <summary>
        ///     获取PostMan集合
        /// </summary>
        /// <returns></returns>
        [Route("")]
        public JsonResult<PostmanCollection> GetPostmanCollection()
        {
            var collectionId = PostMan.GetId();
            var apis = Configuration.Services.GetApiExplorer().ApiDescriptions.Where(x => x.Documentation != null);
            var requests = GetPostmanRequests(apis, collectionId);
            var collection = new PostmanCollection
            {
                id = collectionId,
                name = "WebAPI2PostMan",
                description = "",
                order = requests.Select(x => x.id).ToList(),
                timestamp = 0,
                requests = requests
            };

            return Json(collection);
        }

        private List<PostmanRequest> GetPostmanRequests(IEnumerable<ApiDescription> apis, string collectionId)
        {
            return apis.Select(api => new PostmanRequest
            {
                collection = collectionId,
                id = PostMan.GetId(),
                name = api.Documentation,
                dataMode = "urlencoded",
                data = GetPostmanDatas(api),
                description = "",
                descriptionFormat = "html",
                headers = "",
                method = api.HttpMethod.Method,
                pathVariables = new Dictionary<string, string>(),
                url = Host + api.RelativePath,
                version = 2,
                collectionId = collectionId
            }).ToList();
        }

        private List<PostmanData> GetPostmanDatas(ApiDescription api)
        {
            var postmandatas = new List<PostmanData>();
            var apiModel = Configuration.GetHelpPageApiModel(api.GetFriendlyId());
            var raw = apiModel.SampleRequests.Values.FirstOrDefault();
            if (raw == null) return postmandatas;
            var pdata = JsonConvert.DeserializeObject<Dictionary<string,string>>(raw.ToString());
            postmandatas.AddRange(pdata.Select(model => new PostmanData {key = model.Key, value = model.Value}));
            return postmandatas;
        }
    }

}
