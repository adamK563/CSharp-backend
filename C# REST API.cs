using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NUnit.Framework;
using Amazon.S3;
using Amazon.S3.Model;
using System.IO;
using System.Threading.Tasks;

namespace MyBackend
{
    public class MyController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}

//TESTS

namespace MyBackend.Tests
{
    [TestFixture]
    public class MyControllerTests
    {
        [Test]
        public void Get_ShouldReturnAllValues()
        {
            // Arrange
            var controller = new MyController();

            // Act
            var result = controller.Get();

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("value1", result.ElementAt(0));
            Assert.AreEqual("value2", result.ElementAt(1));
        }

        [Test]
        public void Get_ShouldReturnSingleValue()
        {
            // Arrange
            var controller = new MyController();

            // Act
            var result = controller.Get(5);

            // Assert
            Assert.AreEqual("value", result);
        }

        [Test]
        public void Post_ShouldAddValue()
        {
            // Arrange
            var controller = new MyController();

            // Act
            controller.Post("new value");

            // Assert
            // (Assert that the value was added to the data store)
        }

        [Test]
        public void Put_ShouldUpdateValue()
        {
            // Arrange
            var controller = new MyController();

            // Act
            controller.Put(5, "updated value");

            // Assert
            // (Assert that the value was updated in the data store)
        }

        [Test]
        public void Delete_ShouldRemoveValue()
        {
            // Arrange
            var controller = new MyController();

            // Act
            controller.Delete(5);

            // Assert
            // (Assert that the value was removed from the data store)
        }
    }
}

//ROUTERS

namespace MyBackendRouter
{
    public class MyController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}

// ADDING AWS S3 SERVICE

namespace MyBackendS3
{
    public class MyS3Client
    {
        private readonly IAmazonS3 _s3Client;

        public MyS3Client(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }

        public async Task<PutObjectResponse> AddObject(string bucketName, string keyName, Stream stream)
        {
            var request = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = keyName,
                InputStream = stream
            };

            return await _s3Client.PutObjectAsync(request);
        }

        public async Task<GetObjectResponse> GetObject(string bucketName, string keyName)
        {
            var request = new GetObjectRequest
            {
                BucketName = bucketName,
                Key = keyName
            };

            return await _s3Client.GetObjectAsync(request);
        }
    }
}

