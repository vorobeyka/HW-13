using DepsTestApp.Tests.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DepsTestApp.Helpers;
using DepsTestApp.Models;
using System.Text;

namespace DepsTestApp.Tests
{
    internal class TestValidator
    {
        private readonly Queue<ITest> _tests = new Queue<ITest>();
        private readonly HttpClient _client = new HttpClient();
        private readonly string _settingsFilePath = "settings.json";

        public TestValidator()
        {
            var address = CustomSerializer.GetObject<Settings>(_settingsFilePath).Address;
            _client.BaseAddress = new Uri(address);
            _client.Timeout = TimeSpan.FromSeconds(10);
        }

        public void SetTestsFromFile(string filePath)
        {
            var tests = CustomSerializer.GetObject<IList<Test>>(filePath);
            foreach (var test in tests)
            {
                _tests.Enqueue(test);
            }
        }

        public async Task StartTests()
        {
            while (_tests.Count != 0)
            {
                await StartValidate(_tests.Dequeue());
            }
        }

        private async Task StartValidate(ITest test)
        {
            if (test == null) throw new ArgumentNullException(nameof(test));

            #region request process
            var httpMethod = HttpMethodMapping.MapMethod(test.Method);
            var authBase64string = AccountEncoding.AccountToBase64(test.Account);
            var body = CustomSerializer.ToJson(test.Account);
            using var message = new HttpRequestMessage(httpMethod, test.RequestString);
            message.Headers.Add(test.HeaderName, authBase64string);
            message.Content = new StringContent(body, Encoding.UTF8, "application/json");
            #endregion

            using var response = await _client.SendAsync(message);

            await WriteResult(response, test);
        }

        private static async Task WriteResult(HttpResponseMessage response, ITest test)
        {
            Console.WriteLine($"Request: {test.RequestString}");
            Console.WriteLine($"Expected status code: {test.ExpectedStatusCode}");
            Console.WriteLine($"Response status code: {(int)response.StatusCode}");
            if ((int)response.StatusCode == test.ExpectedStatusCode)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Test SUCCESS!");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Test FAILED!");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            Console.WriteLine($"Response content:\n{await response.Content.ReadAsStringAsync()}");
            Console.WriteLine(new string('_', 25));
        }
    }
}
