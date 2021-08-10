//using Microsoft.AspNetCore.Http;
//using Newtonsoft.Json;
//using Services.Shared.Models;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Services.Auth.Helpers
//{
//    public class PasswordHashingMiddleware : IMiddleware
//    {
//        private readonly IPasswordHasher PasswordHasher;

//        public PasswordHashingMiddleware(IPasswordHasher passwordHasher)
//        {
//            PasswordHasher = passwordHasher;
//        }

//        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
//        {
//            var request = context.Request;

//            if (!request.Path.Value.Contains("login"))
//            {
//                await next(context);
//                return;
//            }

//            //get the request body and put it back for the downstream items to read
//            var stream = request.Body;// currently holds the original stream                    
//            var originalContent = new StreamReader(stream).ReadToEnd();
//            var notModified = true;
//            try
//            {
//                var dataSource = JsonConvert.DeserializeObject<LoginInfo>(originalContent);
//                if (dataSource != null)
//                {
//                    //modified stream
//                    dataSource.Password = PasswordHasher.HashPassword(dataSource.Password);
//                    var json = JsonConvert.SerializeObject(dataSource);
//                    var requestData = Encoding.UTF8.GetBytes(json);
//                    stream = new MemoryStream(requestData);
//                    notModified = false;
//                }
//            }
//            catch (Exception ex)
//            {
//                //log error
//            }
//            if (notModified)
//            {
//                //put original data back for the downstream to read
//                var requestData = Encoding.UTF8.GetBytes(originalContent);
//                stream = new MemoryStream(requestData);
//            }

//            request.Body = stream;
//            await next.Invoke(context);
//        }
//    }
//}
