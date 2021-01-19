using Base32;
using LachlanBarclayNet.Areas.Admin.ViewModels;
using LachlanBarclayNet.DAO;
using LachlanBarclayNet.DAO.Standard;

using OtpSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace LachlanBarclayNet.Areas.Admin.Controllers
{
    [Authorize]
    public class PostController : Controller
    {

        //
        // GET: /Admin/Post/
        public ActionResult Search()
        {
            AdminSearchViewModel ViewModel = new AdminSearchViewModel();
            ViewModel.Posts = new List<LachlanBarclayNet.DAO.Standard.Post>();
            return View(ViewModel);
        }

        [HttpPost]
        public ActionResult Search(AdminSearchViewModel ViewModel)
        {
            PostDAO postDAO = new PostDAO();

            ViewModel.Posts = postDAO.PostSearchString(ViewModel.SearchString, null);

            return View(ViewModel);
        }

        public ActionResult QrCode()
        {
            byte[] secretKey = KeyGeneration.GenerateRandomKey(20);
            string userName = "lachlan";
            string issuerEncoded = HttpUtility.UrlEncode("lachlanbarclay.net");
            string barcodeUrl = KeyUrl.GetTotpUrl(secretKey, userName) + "&issuer=" + issuerEncoded;

            var model = new AdminQrCode
            {
                SecretKey = Base32Encoder.Encode(secretKey),
                BarcodeUrl = barcodeUrl
            };

            return View(model);
        }

        public ActionResult Img()
        {
            PostDAO postDAO = new PostDAO();

            var ViewModel = new AdminImgViewModel();

            var posts = postDAO.GetAllLivePosts();
           
            ViewModel.Posts = new List<AdminImgPost>();

            Regex regex = new Regex("src=['\"](.*?)['\"]");

            foreach (var post in posts)
            {
                MatchCollection mc = regex.Matches(post.PostText);
                if (mc.Count > 0)
                {
                    AdminImgPost imgPost = new AdminImgPost
                    {
                        images = new List<AdminImgPostImg>(),
                        PostID = post.PostId,
                        PostTitle = post.PostTitle
                    };

                    foreach (Match match in mc)
                    {
                        if (match.Success)
                        {
                            string v = match.Groups[1].Value;

                            if (v.EndsWith(".jpg") || v.EndsWith(".png"))
                            {
                                imgPost.images.Add(new AdminImgPostImg
                                {
                                    Img = v
                                });
                            }
                        }
                    }

                    ViewModel.Posts.Add(imgPost);
                }
            }

            return View(ViewModel);
        }


        public ActionResult Index(string from)
        {
            PostDAO postDAO = new PostDAO();
            
            var ViewModel = new AdminIndexViewModel();
            DateTime? searchFromDate = null;
            DateTime parsedDate;
            if (from != null)
            {
                if (DateTime.TryParseExact(from, "yyyyMMdd-HHmmss", CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal, out parsedDate))
                {
                    searchFromDate = parsedDate;
                }
            }

            ViewModel.Posts = postDAO.PostSearchString(null, searchFromDate);

            ViewModel.FromString = (ViewModel.Posts.Count == 0) ? "" : ViewModel.Posts.Last().PostDate.ToString("yyyyMMdd-HHmmss");
            
            return View(ViewModel);
        }

        //
        // GET: /Admin/Post/Details/5

        public ActionResult Details(int? id = 0)
        {
            PostDAO postDAO = new PostDAO();
            Post post = new Post();
            if (id.HasValue)
            {
                post = postDAO.Get(id.Value);
                if (post == null)
                {
                    return HttpNotFound();
                }
            }
            return View(post);
        }


        //
        // GET: /Admin/Post/Edit/5

        public ActionResult Edit(int? id)
        {
            PostDAO postDAO = new PostDAO();
            Post post = null;
            
            if (id.HasValue)
            {
                post = postDAO.Get(id.Value);
                if (post == null)
                {
                    return HttpNotFound();
                }
            } else
            {
                post = new Post();
                post.PostDate = DateTime.Now;
            }
            ViewBag.PostTypeID = CreatePostTypeSelectList(post.PostTypeId);
            ViewBag.action = TempData["action"];
            return View(post);
        }

        private SelectList CreatePostTypeSelectList(int? PostTypeID)
        {
            PostDAO postDAO = new PostDAO();
            return new SelectList(postDAO.GetTypes(), "PostTypeID", "PostTypeName", PostTypeID);
        }

        //
        // POST: /Admin/Post/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                PostDAO postDAO = new PostDAO();
                if (post.PostId > 0)
                {
                    postDAO.Update(post);
                    TempData["action"] = "post updated";

                    string month = post.PostDate.Month.ToString("00");

                    HttpResponse.RemoveOutputCacheItem($"/{post.PostDate.Year}/{month}/{post.PostUrl}");
                    HttpResponse.RemoveOutputCacheItem($"/");
                }
                else
                {
                    postDAO.Insert(post);
                    TempData["action"] = "post created";
                }
                return RedirectToAction("Edit", new { id = post.PostId });
            }
            ViewBag.PostTypeID = CreatePostTypeSelectList(post.PostTypeId);
            return View(post);
        }

        //
        // GET: /Admin/Post/Delete/5

        public ActionResult Delete(int id = 0)
        {
            PostDAO postDAO = new PostDAO();
            Post post = postDAO.Get(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        //
        // POST: /Admin/Post/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PostDAO postDAO = new PostDAO();
            postDAO.Delete(id);
            return RedirectToAction("Index");
        }
        
    }
}