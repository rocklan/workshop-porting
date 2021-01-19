using System.Configuration;

namespace LachlanBarclayNet.DAO.Standard
{
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
