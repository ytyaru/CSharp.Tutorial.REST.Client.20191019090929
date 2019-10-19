using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
namespace RestClient
{
    class Code4
    {
        private static readonly HttpClient client = new HttpClient();
        public void Run()
        {
            var repositories = ProcessRepositories().Result;
            foreach (var repo in repositories) {
                Console.WriteLine(repo.Name);
                Console.WriteLine(repo.Description);
                Console.WriteLine(repo.GitHubHomeUrl);
                Console.WriteLine(repo.Homepage);
                Console.WriteLine(repo.Watchers);
                Console.WriteLine();
            }
        }
        private async Task<List<Repository4>> ProcessRepositories()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository4 Reporter");

            var streamTask = client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");

            var serializer = new DataContractJsonSerializer(typeof(List<Repository4>));
            return serializer.ReadObject(await streamTask) as List<Repository4>;
        }
    }
    // JSONの"repo"ノードをマッピングする
    [DataContract(Name="repo")]
    public class Repository4
    {
        [DataMember(Name="name")]
        public string Name { get; set; }
        [DataMember(Name="description")]
        public string Description { get; set; }
        [DataMember(Name="html_url")]
        public Uri GitHubHomeUrl { get; set; }
        [DataMember(Name="homepage")]
        public Uri Homepage { get; set; }
        [DataMember(Name="watchers")]
        public int Watchers { get; set; }
    }
}
