using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace diagnoseApp.DAL
{
    public class Person
    {
        public int id { get; set; }
        [RegularExpression(@"[a-zA-ZæøåÆØÅ. \-]{2,20}")]
        public string fornavn { get; set; }
        [RegularExpression(@"[a-zA-ZæøåÆØÅ. \-]{2,20}")]
        public string etternavn { get; set; }
        [RegularExpression(@"[0-9.]{10}")]
        public string fodselsnr { get; set; }
        [RegularExpression(@"[0-9a-zA-ZæøåÆØÅ. \-]{2,50}")]
        public string adresse { get; set; }
        [RegularExpression(@"[0-9]{8}")]
        public string tlf { get; set; }
        [RegularExpression(@"[0-9a-zA-ZæøåÆØÅ.@_ \-]{2,20}")]
        public string epost { get; set; }
        //public virtual List<Test> tester { get; set; }

        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$")]
        public string passord { get; set; }
    }


}

