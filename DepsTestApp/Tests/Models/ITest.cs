using DepsTestApp.Models;
using System.Net.Http;

namespace DepsTestApp.Tests.Models
{
    internal interface ITest
    {
        int ExpectedStatusCode { get; set; }
        string RequestString { get; set; }
        string HeaderName { get; set; }
        Account Account { get; set; }
        RequestMethod Method { get; set; }
    }
}
