using LachlanBarclayNet.DAO.Standard;

using System.Configuration;
using System.Linq;

namespace LachlanBarclayNet.DAO
{
    public class ContactDAO
    {
        private lachlanbarclaynet2Context GetContext()
        {
            return new lachlanbarclaynet2Context(
                ConfigurationManager.ConnectionStrings["LbNet"].ConnectionString);
        }

        public void InsertContact(string name, string email, string message)
        {
            using (lachlanbarclaynet2Context context = GetContext())
            {
                context.Add(new Contact
                {
                    Email = email,
                    Message = message,
                    Name = name
                });
                context.SaveChanges();
            }
        }
    }
}