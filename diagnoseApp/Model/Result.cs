using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace diagnoseApp.Model
{
    public class Result
    {
        public int id { get; set; }
        public string fornavn { get; set; }
        public string etternavn { get; set; }
        public string fodselsnr { get; set; }
        public string adresse { get; set; }
        public string tlf { get; set; }
        public string epost { get; set; }
        public int testid { get; set; }
        public string dato { get; set; }
        public string resultat { get; set; }
        public int personid { get; set; }


    }


}

