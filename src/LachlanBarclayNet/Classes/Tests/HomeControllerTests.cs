using LachlanBarclayNet.Areas.Admin.Controllers;
using LachlanBarclayNet.Controllers;
using LachlanBarclayNet.DAO;
using LachlanBarclayNet.ViewModel;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LachlanBarclayNet.Tests
{
    public class HomeControllerTests
    {

        private readonly Post _testPost = new Post {
            PostTitle = "test-blog",
            PostText = "post text",
            PostDate = new DateTime(2015, 1, 1),
            Published = true
        };

        private HomeController GetTestableHomeController()
        {
            var mockDAO = new Moq.Mock<IPostDAO>();
            mockDAO.Setup(x => x.Get(_testPost.PostDate.Year, _testPost.PostDate.Month, _testPost.PostTitle)).Returns(_testPost);

            var pc = new LachlanBarclayNet.Controllers.HomeController(mockDAO.Object);
            return pc;
        }


        [Test]
        public void View_Post_Should_Contain_PostText()
        {
            HomeController homeController = GetTestableHomeController();

            var actionResult = homeController.ViewPost(2015, 1, "test-blog") as ViewResult;

            Post viewModel = (Post) actionResult.Model;

            Assert.IsNotNull(viewModel);
            Assert.AreEqual("post text", viewModel.PostText);

        }
    }
}
