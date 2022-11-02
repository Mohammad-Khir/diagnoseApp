using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace diagnoseApp.DAL
{
    public class Person
    {
        public int id { get; set; }
        public string fornavn { get; set; }
        public string etternavn { get; set; }
        public string fodselsnr { get; set; }
        public string adresse { get; set; }
        public string tlf { get; set; }
        public string epost { get; set; }
        //public virtual List<Test> tester { get; set; }



    }


}

