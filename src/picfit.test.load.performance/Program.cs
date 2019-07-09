using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace picfit.test.load.performance
{
    class Program
    {
        static int treadsCount = 2048;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.ReadLine();
            for (int i = 0; i < treadsCount; i++)
            {

                var path = @"C:\Users\andrey.zaitsev\Pictures\500_F_135722246_wbxhr1Q4yxVW5foaDZHkwSLjYl8wbJAO.jpg";
                ThreadPool.SetMinThreads(treadsCount, treadsCount);


                var url = "http://localhost:5000/api/Images";
                var filePath = @"C:\Users\andrey.zaitsev\Pictures\500_F_135722246_wbxhr1Q4yxVW5foaDZHkwSLjYl8wbJAO.jpg";

                HttpClient httpClient = new HttpClient();
                MultipartFormDataContent form = new MultipartFormDataContent();

                FileStream fs = File.OpenRead(filePath);
                var streamContent = new StreamContent(fs);

                var imageContent = new ByteArrayContent(streamContent.ReadAsByteArrayAsync().Result);
                imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart /form-data");

                form.Add(imageContent, "image", i + ".jpg");
                var response = httpClient.PostAsync(url, form).Result;

            }

        }
    }
}
