using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
namespace RestClient
{
    class Code3
    {
        private static readonly HttpClient client = new HttpClient();
        public void Run()
        {
            var repositories = ProcessRepositories().Result;
            foreach (var repo in repositories)
                Console.WriteLine(repo.Name);
        }
        private async Task<List<Repository3>> ProcessRepositories()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository3 Reporter");

            var streamTask = client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");

            var serializer = new DataContractJsonSerializer(typeof(List<Repository3>));
            return serializer.ReadObject(await streamTask) as List<Repository3>;
        }
    }
    // JSONの"repo"ノードをマッピングする
    [DataContract(Name="repo")]
    public class Repository3
    {
        [DataMember(Name="name")]
        public string Name { get; set; }
    }
}
