namespace LachlanBarclayNet.Controllers
{
    public interface IIdentityLogic
    {
        bool IsAuthenticated { get; }
        string Username { get;  }
    }
}