using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Caching;
using System.Web;

using LachlanBarclayNet.DAO.Standard;

namespace LachlanBarclayNet.DAO
{
    public class PostDAO : IPostDAO
    {
        private lachlanbarclaynet2Context GetContext()
        {
            return new lachlanbarclaynet2Context(
                ConfigurationManager.ConnectionStrings["LbNet"].ConnectionString);
        }

        public List<LachlanBarclayNet.DAO.Standard.Post> GetAll()
        {
            using (lachlanbarclaynet2Context context = GetContext())
            {
                return context
                    .Post
                    .OrderByDescending(x => x.PostDate)
                    .ToList();
            }
        }

        public List<LachlanBarclayNet.DAO.Standard.Post> GetAllLivePosts()
        {
            using (lachlanbarclaynet2Context context = GetContext())
            {
                return context
                    .Post
                    .Where(x => x.PostDate < DateTime.Now && (x.Published.HasValue && x.Published.Value))
                    .OrderByDescending(x => x.PostDate)
                    .ToList();
            }
        }

        public List<LachlanBarclayNet.DAO.Standard.Post> PostSearch(DateTime searchFromDate, string category, int limit)
        {
            using (lachlanbarclaynet2Context context = GetContext())
            {
                return context
                    .Post
                    .Include(x => x.PostComment)
                    .Where(x => x.Published.HasValue && x.Published.Value &&
                                x.PostDate < searchFromDate &&
                                (category == null || x.PostType.PostTypeName == category))
                    .OrderByDescending(x => x.PostDate)
                    .Take(limit)
                    .ToList();
            }
        }

     

        public List<LachlanBarclayNet.DAO.Standard.Post> PostSearchString(string SearchString, DateTime? SearchFromDate)
        {
            using (lachlanbarclaynet2Context context = GetContext())
            {
                return context
                    .Post
                    .Include("PostType")
                    .Where(x => (SearchString == null || x.PostText.Contains(SearchString)) &&
                                (SearchFromDate == null || x.PostDate <= SearchFromDate))
                    .OrderByDescending(x => x.PostDate)
                    .Take(50)
                    .ToList();
            }
        }

        public LachlanBarclayNet.DAO.Standard.Post Get(int PostID)
        {
            using (lachlanbarclaynet2Context context = GetContext())
            {
                return context
                        .Post
                        .Include("PostType")
                        .FirstOrDefault(x => x.PostId == PostID);
            }
        }

        public LachlanBarclayNet.DAO.Standard.Post Get(int Year, int Month, string Title)
        {
            using (lachlanbarclaynet2Context context = GetContext())
            {
                return context
                        .Post
                        .Include("PostComments")
                        .FirstOrDefault(x => x.PostDate.Year == Year && x.PostDate.Month == Month && x.PostUrl == Title);

            }
        }

        public List<LachlanBarclayNet.DAO.Standard.Post> GetPostsForSideBar(int PostTypeID)
        {
            using (lachlanbarclaynet2Context context = GetContext())
            {
                return context
                    .Post
                    .Where(x => x.PostDate < DateTime.Now && (x.Published.HasValue && x.Published.Value) && x.PostTypeId == PostTypeID)
                    .OrderByDescending(x => x.PostDate)
                    .ToList();

            }
        }


        public List<LachlanBarclayNet.DAO.Standard.PostType> GetTypes()
        {
            using (lachlanbarclaynet2Context context = GetContext())
            {
                return context.PostType.ToList();
            }
        }

        public int Insert(LachlanBarclayNet.DAO.Standard.Post post)
        {
            using (lachlanbarclaynet2Context context = GetContext())
            {
                post.PostUrl = post.PostUrl.ToLower();
                context.Post.Add(post);
                context.SaveChanges();
                return post.PostId;
            }
        }

        public void Update(LachlanBarclayNet.DAO.Standard.Post post)
        {
            using (lachlanbarclaynet2Context context = GetContext())
            {
                var postInDb = context.Post.FirstOrDefault(x => x.PostId == post.PostId);
                if (postInDb != null)
                {
                    postInDb.PostDate = post.PostDate;
                    postInDb.PostTypeId = post.PostTypeId;
                    postInDb.PostTitle = post.PostTitle;
                    postInDb.PostDescription = post.PostDescription;
                    postInDb.PostUrl = post.PostUrl.ToLower();
                    postInDb.PostText = post.PostText;
                    postInDb.Published = post.Published;

                    context.SaveChanges();
                }
            }
        }
        public void Delete(int PostID)
        {
            using (lachlanbarclaynet2Context context = GetContext())
            {
                var post = context.Post.Single(p => p.PostId == PostID);
                context.Post.Remove(post);

                context.SaveChanges();
            }
        }
    }

}