namespace LachlanBarclayNet.DAO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Configuration;
    using System.Data.Entity.Spatial;

    
    public partial class Post
    {
        public string FriendlyUrl
        {
            get
            {
                return this.PostDate.Year + "/" + this.PostDate.Month.ToString("00") + "/" + this.PostUrl;
            }
        }

        public string FullUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["domain"] + "/" + this.FriendlyUrl;
            }
        }
    }
}
