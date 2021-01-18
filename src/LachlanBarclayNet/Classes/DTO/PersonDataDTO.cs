using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LachlanBarclayNet.Classes.DTO
{

    public class PersonDataDTO
    {
        public int personid { get; set; }
        public string name { get; set; }
        public int? age { get; set; }
        public string gender { get; set; }
        public string occupation { get; set; }
        public string education { get; set; }

        public string firstLanguage { get; set; }
        public string secondLanguage { get; set; }
        public string thirdLanguage { get; set; }
        public string fourthLanguage { get; set; }

        public string livesInMunicipality { get; set; }
        public string livesInDistrict { get; set; }
        public string livesInVillage { get; set; }

        public bool livedWholeLife { get; set; }
        public int? livedInYears { get; set; }

        public string bornMunicipality { get; set; }
        public string bornDistrict { get; set; }
        public string bornVillage { get; set; }

        public List<WordDataDTO> words { get; set; }
    }

}