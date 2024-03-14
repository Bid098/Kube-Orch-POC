
using Newtonsoft.Json;
using StackExchange.Redis;
using System;

string host = Environment.GetEnvironmentVariable("Host") != "" ? Environment.GetEnvironmentVariable("Host") : "http://127.0.0.1:5000";
while (true)
{
    string producerUrl = $"{host}/data";
    HttpClient client = new HttpClient();
    string value = await FetchData(client, producerUrl);

    async Task<string> FetchData(HttpClient client, string producerUrl)
    {
        HttpResponseMessage response = await client.GetAsync(producerUrl);
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<dynamic>(data)["value"].ToString();
        }

        throw new Exception("Unable to fetch data");
    }

    ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("172.27.200.198:6379");
    IDatabase db = redis.GetDatabase();

    string key = host;
    db.StringSet(key, value);
    Console.WriteLine($"Consumed: {value}");
    Thread.Sleep(2000);
}