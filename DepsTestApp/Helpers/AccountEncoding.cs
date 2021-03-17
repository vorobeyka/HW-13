using DepsTestApp.Models;
using System;
using System.Text;

namespace DepsTestApp.Helpers
{
    internal static class AccountEncoding
    {
        private static readonly string _encodingName = "ISO-8859-1";

        public static string AccountToBase64(Account account)
        {
            if (account == null) return null;
            return "Basic "
                + Convert.ToBase64String(
                    Encoding.GetEncoding(_encodingName)
                    .GetBytes(account.Login + ":" + account.Password));
        }
    }
}
