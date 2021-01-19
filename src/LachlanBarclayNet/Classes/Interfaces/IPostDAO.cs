using System;
using System.Collections.Generic;

namespace LachlanBarclayNet.DAO
{
    public interface IPostDAO
    {
        List<LachlanBarclayNet.DAO.Standard.Post> GetAllLivePosts();
        void Delete(int PostID);
        LachlanBarclayNet.DAO.Standard.Post Get(int PostID);
        LachlanBarclayNet.DAO.Standard.Post Get(int Year, int Month, string Title);
        List<LachlanBarclayNet.DAO.Standard.Post> GetAll();
        List<LachlanBarclayNet.DAO.Standard.PostType> GetTypes();
        int Insert(LachlanBarclayNet.DAO.Standard.Post post);
        List<LachlanBarclayNet.DAO.Standard.Post> PostSearch(DateTime searchFromDate, string category, int limit);
        List<LachlanBarclayNet.DAO.Standard.Post> PostSearchString(string SearchString, DateTime? SearchFromDate);
        void Update(LachlanBarclayNet.DAO.Standard.Post post);
        List<LachlanBarclayNet.DAO.Standard.Post> GetPostsForSideBar(int PostTypeID);
    }
}