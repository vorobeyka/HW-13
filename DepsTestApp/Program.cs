using DepsTestApp.Tests;
using DepsTestApp.Tests.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DepsTestApp
{
    class Program
    {
        private static readonly string _testsFilePath = "tests.json";

        static async Task Main(string[] args)
        {
            Console.WriteLine("Test application for DepsWebApp\nby Andrey Basystyi");
            try
            {
                var testValidator = new TestValidator();
                testValidator.SetTestsFromFile(_testsFilePath);
                await testValidator.StartTests();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Oooops... Something was wrong:\n{ex.Message}");
            }
        }
    }
}
