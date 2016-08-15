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
        /// <summary>
        ///    你可以将该地址用于 PostMan 的 Import from Link
        /// </summary>
        /// <returns></returns>
        [Route("urlencoded")]
        public JsonResult<PostmanCollection> GetPostmanCollection_Urlencoded()
        {
            var collectionId = PostMan.GetId();
            var apis = Configuration.Services.GetApiExplorer().ApiDescriptions.Where(x => x.Documentation != null);
            var requests = GetPostmanRequests_Urlencoded(apis, collectionId);
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

        /// <summary>
        ///     你可以将该地址用于 PostMan 的 Import from Link
        /// </summary>
        /// <returns></returns>
        [Route("raw")]
        public JsonResult<PostmanCollection> GetPostmanCollection_Raw()
        {
            var collectionId = PostMan.GetId();
            var apis = Configuration.Services.GetApiExplorer().ApiDescriptions.Where(x => x.Documentation != null);
            var requests = GetPostmanRequests_Raw(apis, collectionId);
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

        private List<PostmanRequest> GetPostmanRequests_Urlencoded(IEnumerable<ApiDescription> apis, string collectionId)
        {
            return apis.Select(api => new PostmanRequest
            {
                collection = collectionId,
                id = PostMan.GetId(),
                name = api.Documentation,
                dataMode = "urlencoded",
                data = GetPostmanDatas_Urlencoded(api),
                description = "",
                descriptionFormat = "html",
                headers = "",
                method = api.HttpMethod.Method,
                pathVariables = new Dictionary<string, string>(),
                url = Request.RequestUri.Authority + "/" + api.RelativePath,
                version = 2,
                collectionId = collectionId
            }).ToList();
        }

        private List<PostmanData> GetPostmanDatas_Urlencoded(ApiDescription api)
        {
            var postmandatas = new List<PostmanData>();
            var apiModel = Configuration.GetHelpPageApiModel(api.GetFriendlyId());
            var raw = apiModel.SampleRequests.Values.FirstOrDefault();
            if (raw == null) return postmandatas;
            var pdata = JsonConvert.DeserializeObject<Dictionary<string,string>>(raw.ToString());
            postmandatas.AddRange(pdata.Select(model => new PostmanData {key = model.Key, value = model.Value}));
            return postmandatas;
        }

        private List<PostmanRequest> GetPostmanRequests_Raw(IEnumerable<ApiDescription> apis, string collectionId)
        {
            return apis.Select(api => new PostmanRequest
            {
                collection = collectionId,
                id = PostMan.GetId(),
                name = api.Documentation,
                dataMode = "raw",
                data = new List<PostmanData>(),
                rawModeData = GetPostmanDatas_Raw(api),
                description = "",
                descriptionFormat = "html",
                headers = "Content-Type: application/json",
                method = api.HttpMethod.Method,
                pathVariables = new Dictionary<string, string>(),
                url = Request.RequestUri.Authority + "/" + api.RelativePath,
                version = 2,
                collectionId = collectionId
            }).ToList();
        }

        private string GetPostmanDatas_Raw(ApiDescription api)
        {
            var rawContent = string.Empty;
            var apiModel = Configuration.GetHelpPageApiModel(api.GetFriendlyId());
            var raw = apiModel.SampleRequests.Values.FirstOrDefault();
            if (raw == null) return rawContent;
            rawContent = raw.ToString();
            return rawContent;
        }
    }

}
