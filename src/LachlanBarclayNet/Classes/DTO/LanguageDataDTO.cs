using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LachlanBarclayNet.Classes.DTO
{
    public class LanguageDataDTO
    {
        public string InstallID { get; set; }
        public List<PersonDataDTO> Data { get; set; }
    }

}