using System;

namespace LachlanBarclayNet.Controllers
{
    [Serializable]
    public class SessionData
    {
        public string LastTitle { get; set; }
        public DateTime LastRead { get; set; }
    }

}
