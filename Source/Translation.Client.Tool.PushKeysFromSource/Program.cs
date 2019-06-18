using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Translation.Client.Tool.PushKeysFromSource.Models;

namespace Translation.Client.Tool.PushKeysFromSource
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceUrl = ConfigurationManager.AppSettings["ServiceUrl"];
            if (string.IsNullOrWhiteSpace(serviceUrl))
            {
                Console.WriteLine("please define service url in app.config");
                return;
            }

            var clientId = ConfigurationManager.AppSettings["ServiceClientId"];
            if (string.IsNullOrWhiteSpace(clientId))
            {
                Console.WriteLine("please define client id in app.config");
                return;
            }

            var clientSecret = ConfigurationManager.AppSettings["ServiceClientSecret"];
            if (string.IsNullOrWhiteSpace(clientSecret))
            {
                Console.WriteLine("please define client secret in app.config");
                return;
            }

            var projectUid = ConfigurationManager.AppSettings["ProjectUid"];
            if (string.IsNullOrWhiteSpace(projectUid))
            {
                Console.WriteLine("please define project uid in app.config");
                return;
            }

            var projectFolder = ConfigurationManager.AppSettings["ProjectFolder"];
            if (string.IsNullOrWhiteSpace(projectFolder))
            {
                Console.WriteLine("please define project folder in app.config");
                return;
            }

            Console.WriteLine("collecting labels from source");

            var items = new List<string>();

            GetLabelKeysFromViews(Directory.GetFiles(projectFolder, "*.cshtml", SearchOption.AllDirectories), items);
            GetLabelKeys(Directory.GetFiles(projectFolder, "*.cs", SearchOption.AllDirectories), items, "(?<=.Localize\\(\")(.*)(?=\")");
            GetLabelKeys(Directory.GetFiles(projectFolder, "*.cs", SearchOption.AllDirectories), items, "(?<=.Messages.Add\\(\")(.*)(?=\")");

            Console.WriteLine("found " + items.Count + " label");

            string token;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(serviceUrl);
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("clientId", clientId),
                    new KeyValuePair<string, string>("clientSecret", clientSecret)
                });
                var result = client.PostAsync("/Token/Create", content).Result;
                var resultContent = result.Content.ReadAsStringAsync().Result;
                var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(resultContent);
                token = tokenResponse.token.ToString();
            }

            Console.WriteLine("checking existing labels");

            var newItems = new List<string>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(serviceUrl);

                var result = client.GetAsync("/Data/GetLabels?token=" + token + "&projectUid=" + projectUid).Result;
                var resultContent = result.Content.ReadAsStringAsync().Result;
                var labels = JsonConvert.DeserializeObject<List<LabelInfo>>(resultContent);

                for (var i = 0; i < items.Count; i++)
                {
                    var item = items[i];
                    if (labels.Any(x => x.key == item))
                    {
                        continue;
                    }

                    newItems.Add(item);
                }
            }

            Console.WriteLine("there is " + newItems.Count + " new label");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(serviceUrl);

                foreach (var newItem in newItems)
                {
                    var content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("token", token),
                        new KeyValuePair<string, string>("projectUid", projectUid),
                        new KeyValuePair<string, string>("labelKey", newItem),
                    });
                    var result = client.PostAsync("/Data/AddLabel", content).Result;
                    var resultContent = result.Content.ReadAsStringAsync().Result;
                    var response = JsonConvert.DeserializeObject<CommonResult>(resultContent);
                    if (!response.isOk)
                    {
                        Console.WriteLine("can not add > " + newItem);
                    }
                }
            }

            Console.WriteLine("completed adding new items");
        }

        private static void GetLabelKeysFromViews(string[] files, List<string> items)
        {
            for (var i = 0; i < files.Length; i++)
            {
                var filePath = files[i];
                var fileContent = File.ReadAllText(filePath);
                if (!fileContent.Contains("data-translation="))
                {
                    continue;
                }

                var regex = new Regex("(?<=data-translation=\")(.*)(?=\")", RegexOptions.Compiled);
                var matches = regex.Matches(fileContent);
                if (!matches.Any())
                {
                    continue;
                }

                var isAlphabetical = new Regex("^[A-Za-z0-9_]+$", RegexOptions.Compiled);

                foreach (var match in matches)
                {
                    var value = match.ToString().Trim();
                    if (string.IsNullOrWhiteSpace(value)
                        || !isAlphabetical.IsMatch(value))
                    {
                        continue;
                    }

                    if (items.Contains(value))
                    {
                        continue;
                    }

                    items.Add(value);
                }
            }
        }

        private static void GetLabelKeys(string[] files, List<string> items, string regexString)
        {
            for (var i = 0; i < files.Length; i++)
            {
                var filePath = files[i];
                var fileContent = File.ReadAllText(filePath);

                var regex = new Regex(regexString, RegexOptions.Compiled);
                var matches = regex.Matches(fileContent);
                if (!matches.Any())
                {
                    continue;
                }

                var isAlphabetical = new Regex("^[A-Za-z0-9_]+$", RegexOptions.Compiled);

                foreach (var match in matches)
                {
                    var value = match.ToString().Trim();
                    if (string.IsNullOrWhiteSpace(value)
                        || !isAlphabetical.IsMatch(value))
                    {
                        continue;
                    }

                    if (items.Contains(value))
                    {
                        continue;
                    }

                    items.Add(value);
                }
            }
        }
    }
}
