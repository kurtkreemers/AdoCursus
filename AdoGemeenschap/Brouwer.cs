using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoGemeenschap
{
    public class Brouwer
    {
        private Int32 brouwersNrValue;
        private String brNaamValue;
        private String adresValue;
        private Int16 postcodeValue;
        private String gemeenteValue;
        private Int32? omzetValue;
        public bool Changed { get; set; }


        public Brouwer()
        {

        }
        public Int32 BrouwerNr
        { get { return brouwersNrValue; } }

        public String BrNaam
        {
            get { return brNaamValue; }
            set 
            {
                brNaamValue = value;
                Changed = true;
            }
        }
        public String Adres
        {
            get { return adresValue; }
            set 
            { 
                adresValue = value;
                Changed = true;
            }
        }
        public Int16 Postcode
        {
            get { return postcodeValue; }
            set 
            { 
                postcodeValue = value;
                Changed = true;
            }
            
        }
        public String Gemeente
        {
            get { return gemeenteValue; }
            set 
            { 
                gemeenteValue = value;
                Changed = true;
            }
        }
        public Int32? Omzet
        {
            get { return omzetValue; }
            set
            {
                if (value.HasValue && Convert.ToInt32(value) < 0)
                { throw new Exception("Omzet moet positief zijn"); }
                else 
                {
                    omzetValue = value;
                    Changed = true;
                }
            }
        }

        public Brouwer(Int32 brNr, String brNaam, String adres, Int16 postcode, String gemeente, Int32? omzet)
        {
            brouwersNrValue = brNr;
            this.BrNaam = brNaam;
            this.Adres = adres;
            this.Postcode = postcode;
            this.Gemeente = gemeente;
            this.Omzet = omzet;
            Changed = false;
        }
    }
}
