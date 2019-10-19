using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;

namespace RestClient
{
    class Code1
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

            var serializer = new DataContractJsonSerializer(typeof(List<repo>));
            var repositories = serializer.ReadObject(await streamTask) as List<repo>;

            foreach (var repo in repositories)
                Console.WriteLine(repo.name);
        }
    }
    public class repo
    {
        public string name;
    }
}
