using System;
using System.Collections.Generic;

namespace LachlanBarclayNet.DAO
{
    public interface IPostDAO
    {
        List<Post> GetAllLivePosts();
        void Delete(int PostID);
        Post Get(int PostID);
        Post Get(int Year, int Month, string Title);
        List<Post> GetAll();
        List<PostType> GetTypes();
        int Insert(Post post);
        List<Post> PostSearch(DateTime searchFromDate, string category, int limit);
        List<Post> PostSearchString(string SearchString, DateTime? SearchFromDate);
        void Update(Post post);
        List<Post> GetPostsForSideBar(int PostTypeID);
    }
}