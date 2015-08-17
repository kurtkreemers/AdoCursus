using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace AdoGemeenschap
{
    public class BrouwerManager
    {
        public List<Brouwer> GetBrouwersBeginNaam(String beginNaam)
        {
            List<Brouwer> brouwers = new List<Brouwer>();
            var manager = new BierenDbManager();
            using(var conBieren = manager.GetConnection())
            {
                using(var comBrouwers = conBieren.CreateCommand())
                {
                    comBrouwers.CommandType = CommandType.Text;
                    if (beginNaam != string.Empty)
                    {
                        comBrouwers.CommandText = "select * from Brouwers where BrNaam like @zoals order by BrNaam";
                        var parZoals = comBrouwers.CreateParameter();
                        parZoals.ParameterName = "@Zoals";
                        parZoals.Value = beginNaam + "%";
                        comBrouwers.Parameters.Add(parZoals);
                    }
                    else
                        comBrouwers.CommandText = "select * from Brouwers";
                    conBieren.Open();
                    using(var rdrBrouwers = comBrouwers.ExecuteReader())
                    {
                        Int32 brouwerNrPos = rdrBrouwers.GetOrdinal("BrouwerNr");
                        Int32 brNaamPos = rdrBrouwers.GetOrdinal("BrNaam");
                        Int32 adresPos = rdrBrouwers.GetOrdinal("Adres");
                        Int32 postcodePos = rdrBrouwers.GetOrdinal("Postcode");
                        Int32 gemeentePos = rdrBrouwers.GetOrdinal("Gemeente");
                        Int32 omzetPos = rdrBrouwers.GetOrdinal("Omzet");
                        Int32? omzet;
                        while (rdrBrouwers.Read())
                        {
                            if (rdrBrouwers.IsDBNull(omzetPos))
                                omzet = null;
                            else
                                omzet = rdrBrouwers.GetInt32(omzetPos);

                            brouwers.Add(new Brouwer(rdrBrouwers.GetInt32(brouwerNrPos),
                                rdrBrouwers.GetString(brNaamPos),rdrBrouwers.GetString(adresPos),
                                rdrBrouwers.GetInt16(postcodePos),rdrBrouwers.GetString(gemeentePos),omzet));                          
                        }
                    }
                }
            }
            return brouwers;
        }
    }
}
