using System.ComponentModel.DataAnnotations;
using System;

namespace diagnoseApp.Model
{
    public class Bruker
    {

        [RegularExpression(@"^[a-zA-ZæøåÆØÅ. \-]{2,20}$")]
        public string Brukernavn { get; set; }
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{4,}$")]
        public string Passord { get; set; }

    }
}
