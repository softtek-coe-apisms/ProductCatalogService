using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogService.Models
{
    public class Utilities
    {
        /// <summary>
        /// Returns a URL of an Image depending on the given string. If there is no results
        /// then the method returns a random Image
        /// </summary>
        /// <param name="name"></param>
        /// <returns>String URL of a web Image server</returns>
        public static string GetImageByNameOrDefault(string name)
        {
            try
            {
                string URL;
                string ApiUrl = "https://api.unsplash.com/search/photos?query=" + name + "&client_id=RR8zTp6LTR2TmVYodb76GyD0Z5SaXaGUoYxX3lr4TJg";
                var request = (HttpWebRequest)WebRequest.Create(ApiUrl);
                var content = string.Empty;
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        using (var sr = new StreamReader(stream))
                        {
                            content = sr.ReadToEnd();
                        }
                    }
                }
                JObject objs = JObject.Parse(content);
                int c = (Int32)objs["total"];
                if (c >= 1)
                {
                    URL = objs["results"][0]["urls"]["small"].ToString();
                }
                else
                {
                    URL = GetImageByNameOrDefault("random");
                }

                return URL;
            }
            catch (WebException e)
            {
                return "https://images.unsplash.com/photo-1581338819396-3153302d7cd0?ixlib=rb-1.2.1&q=80&fm=jpg&crop=entropy&cs=tinysrgb&w=400&fit=max&ixid=eyJhcHBfaWQiOjExNjU4M30";
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Returns a random string based on the parameters
        /// </summary>
        /// <param name="requiredLength">How long should be the final string</param>
        /// <param name="requireNonAlphanumeric">use "true" to allow NonAlphanumeric</param>
        /// <param name="requireDigit">use "true" to allow Digits</param>
        /// <param name="requireLowercase">use "true" to allow Lowercase</param>
        /// <param name="requireUppercase">use "true" to allow Upercase</param>
        /// <returns></returns>
        public static string NewProductId(int requiredLength, bool requireNonAlphanumeric = true, bool requireDigit = true, bool requireLowercase = true, bool requireUppercase = true)
        {
            int length = requiredLength;

            StringBuilder password = new StringBuilder();
            Random random = new Random();

            while (password.Length < length)
            {
                char c = (char)random.Next(32, 126);

                if (char.IsDigit(c) && requireDigit)
                    password.Append(c);
                else if (char.IsLower(c) && requireLowercase)
                    password.Append(c);
                else if (char.IsUpper(c) && requireUppercase)
                    password.Append(c);
                else if (!char.IsLetterOrDigit(c) && requireNonAlphanumeric)
                    password.Append(c);
            }

            return password.ToString();
        }
    }
}
