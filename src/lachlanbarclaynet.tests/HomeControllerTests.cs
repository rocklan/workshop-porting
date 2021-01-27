using System;
using System.Collections.Generic;

using LachlanBarclayNet.DAO;
using LachlanBarclayNet.ViewModel;
using LachlanBarclayNet.DAO.Standard;

using lachlanbarclaynetcore;
using lachlanbarclaynetcore.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Moq;

using NUnit.Framework;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace lachlanbarclaynet.tests
{
    public class Tests
    {
        /*private readonly LachlanBarclayNet.DAO.Standard.Post _testPost = new LachlanBarclayNet.DAO.Standard.Post
        {
            PostTitle = "test-blog",
            PostText = "post text",
            PostDate = new DateTime(2015, 1, 1),
            Published = true
        };

        private HomeController CreateTestableHomeController()
        {
            var mockPostDAO = new Moq.Mock<IPostDAO>();
            mockPostDAO.Setup(x => x.Get(_testPost.PostDate.Year, _testPost.PostDate.Month, _testPost.PostTitle)).Returns(_testPost);

            var mockLogger = new Mock<ILogger<HomeController>>();
            var appSettings = new AppSettings();

            var homeController = new HomeController(
                mockLogger.Object,
                mockPostDAO.Object,
                appSettings
            );

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "example name"),
                new Claim(ClaimTypes.NameIdentifier, "1")
            }, "mock"));

            homeController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            return homeController;
        }

        [Test]
        public void Home_View_Post_Should_Contain_PostText()
        {
            HomeController homeController = CreateTestableHomeController();

            ViewResult result = homeController.ViewPost(2015, 1, "test-blog") as ViewResult;
            Post viewModel = (LachlanBarclayNet.DAO.Standard.Post)result.Model;

            Assert.IsNotNull(viewModel);
            Assert.AreEqual("post text", viewModel.PostText);
        }*/



        private readonly LachlanBarclayNet.DAO.Standard.Post _testPost = new LachlanBarclayNet.DAO.Standard.Post
        {
            PostTitle = "test-blog",
            PostText = "post text",
            PostDate = new DateTime(2015, 1, 1),
            Published = true
        };

        private HomeController GetTestableHomeController()
        {
            var mockDAO = new Moq.Mock<IPostDAO>();
            mockDAO.Setup(x => x.Get(_testPost.PostDate.Year, _testPost.PostDate.Month, _testPost.PostTitle)).Returns(_testPost);

            var mockLogger = new Mock<ILogger<HomeController>>();
            var appSettings = new AppSettings { NumberOfPostsOnHomeScreen = 2 };

            var homeController = new HomeController(mockLogger.Object, mockDAO.Object, appSettings);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "example name"),
                new Claim(ClaimTypes.NameIdentifier, "1")
            }, "mock"));

            homeController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            return homeController;
        }


        [Test]
        public void Core_View_Post_Should_Contain_PostText()
        {
            HomeController homeController = GetTestableHomeController();

            var actionResult = homeController.ViewPost(2015, 1, "test-blog") as ViewResult;

            LachlanBarclayNet.DAO.Standard.Post viewModel = (LachlanBarclayNet.DAO.Standard.Post)actionResult.Model;

            Assert.IsNotNull(viewModel);
            Assert.AreEqual("post text", viewModel.PostText);

        }

    }
}