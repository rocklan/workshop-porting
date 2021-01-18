using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Caching;
using System.Web;

namespace LachlanBarclayNet.DAO
{
    public class PostDAO : IPostDAO
    {
        public List<Post> GetAll()
        {
            using (LbNet context = new LbNet())
            {
                return context
                    .Posts
                    .OrderByDescending(x => x.PostDate)
                    .ToList();
            }
        }

        public List<Post> GetAllLivePosts()
        {
            using (LbNet context = new LbNet())
            {
                return context
                    .Posts
                    .Where(x => x.PostDate < DateTime.Now && x.Published)
                    .OrderByDescending(x => x.PostDate)
                    .ToList();
            }
        }

        public List<Post> PostSearch(DateTime searchFromDate, string category, int limit)
        {
            using (LbNet context = new LbNet())
            {
                return context
                    .Posts
                    .Include("PostComments")
                    .Where(x => x.Published && 
                                x.PostDate < searchFromDate && 
                                (category == null || x.PostType.PostTypeName == category))
                    .OrderByDescending(x => x.PostDate)
                    .Take(limit)
                    .ToList();
            }
        }

        public List<Post> PostSearchString(string SearchString, DateTime? SearchFromDate)
        {
            using (LbNet context = new LbNet())
            {
                return context
                    .Posts
                    .Include("PostType")
                    .Where(x => (SearchString == null || x.PostText.Contains(SearchString)) &&
                                (SearchFromDate == null || x.PostDate <= SearchFromDate))
                    .OrderByDescending(x => x.PostDate)
                    .Take(50)
                    .ToList();
            }
        }

        public Post Get(int PostID)
        {
            using (LbNet context = new LbNet())
            {
                return context
                        .Posts
                        .Include("PostType")
                        .FirstOrDefault(x => x.PostID == PostID);
            }
        }

        public Post Get(int Year, int Month, string Title)
        {
            using (LbNet context = new LbNet())
            {
                return context
                        .Posts
                        .Include("PostComments")
                        .FirstOrDefault(x => x.PostDate.Year == Year && x.PostDate.Month == Month && x.PostUrl == Title);

            }
        }

        public List<Post> GetPostsForSideBar(int PostTypeID)
        {
            using (LbNet context = new LbNet())
            {
                return context
                    .Posts
                    .Where(x => x.PostDate < DateTime.Now && x.Published && x.PostTypeID == PostTypeID)
                    .OrderByDescending(x => x.PostDate)
                    .ToList();

            }
        }

        public List<PostDataDTO> GetForYearCached(int id)
        {
            string key = $"Year.{id}";

            var posts = (List<PostDataDTO>)MemoryCache.Default.Get(key);
            if (posts == null)
            {
                using (LbNet context = new LbNet())
                {
                    posts = context
                       .Posts
                       .Where(x => x.PostDate.Year == id)
                       .Select(x => new
                       {
                           x.PostID,
                           x.PostTitle,
                           x.PostUrl,
                           x.PostDate
                       })
                       .AsEnumerable()
                       .Select(x => new PostDataDTO
                       {
                           PostID = x.PostID,
                           PostTitle = x.PostTitle,
                           PostUrl = x.PostDate.Year + "/" + x.PostDate.Month.ToString("00") + "/" + x.PostUrl
                       })
                       .ToList();

                    MemoryCache.Default.Add(key, posts, new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.UtcNow.AddHours(24) });
                }
            }
            return posts;
        }

        public List<PostType> GetTypes()
        {
            using (LbNet context = new LbNet())
            {
                return context.PostTypes.ToList();
            }
        }

        public int Insert(Post post)
        {
            using (LbNet context = new LbNet())
            {
                post.PostUrl = post.PostUrl.ToLower();
                context.Posts.Add(post);
                context.SaveChanges();
                return post.PostID;
            }
        }

        public void Update(Post post)
        {
            using (LbNet context = new LbNet())
            {
                var postInDb = context.Posts.FirstOrDefault(x => x.PostID == post.PostID);
                if (postInDb != null)
                {
                    postInDb.PostDate = post.PostDate;
                    postInDb.PostTypeID = post.PostTypeID;
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
            using (LbNet context = new LbNet())
            {
                Post post = context.Posts.Single(p => p.PostID == PostID);
                context.Posts.Remove(post);

                context.SaveChanges();
            }
        }
    }

    public class PostDataDTO
    {
        public int PostID { get; set; }
        public string PostTitle { get; set; }
        public string PostUrl { get; set; }
    }

}