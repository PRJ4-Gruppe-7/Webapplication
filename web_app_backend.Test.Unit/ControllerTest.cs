using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Data.SqlClient;
using Moq;
using NSubstitute;
using NUnit.Framework;
using web_app_backend;
using web_app_backend.Models;

namespace web_app_backend_UnitTest
{
    [TestFixture]
    public class ControllerTest
    {
        private IController _controller;

        [SetUp]
        public void SetUP()
        {
            _controller = Substitute.For<IController>();
        }

        [Test]
        public void Recived_calls_JsonSerializer()
        {
            List<Heatmap> heatmaps = new List<Heatmap>();
            Heatmap h  = new Heatmap();
            heatmaps.Add(h);
           
            var mockedObject = new Mock<IController>();
            var obj = mockedObject.Object;

            obj.OpenConnection();

            Mock.Get(obj).Verify(x =>x.JsonSerialize(heatmaps),Times.AtLeastOnce);
        }


    }

}

