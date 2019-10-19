using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
namespace RestClient
{
    class Code2
    {
        private static readonly HttpClient client = new HttpClient();
        public void Run()
        {
            ProcessRepositories().Wait();
        }
        private async Task ProcessRepositories()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var streamTask = client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");

            var serializer = new DataContractJsonSerializer(typeof(List<Repository>));
            var repositories = serializer.ReadObject(await streamTask) as List<Repository>;

            foreach (var repo in repositories)
                Console.WriteLine(repo.Name);
        }
    }
    // JSONの"repo"ノードをマッピングする
    [DataContract(Name="repo")]
    public class Repository
    {
        [DataMember(Name="name")]
        public string Name;
    }
}
