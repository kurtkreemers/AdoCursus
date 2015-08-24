using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections.ObjectModel;

namespace AdoGemeenschap
{
    public class BrouwerManager
    {
        public ObservableCollection<Brouwer> GetBrouwersBeginNaam(String beginNaam)
        {
            ObservableCollection<Brouwer> brouwers = new ObservableCollection<Brouwer>();
            var manager = new BierenDbManager();
            using (var conBieren = manager.GetConnection())
            {
                using (var comBrouwers = conBieren.CreateCommand())
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
                    using (var rdrBrouwers = comBrouwers.ExecuteReader())
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
                                rdrBrouwers.GetString(brNaamPos), rdrBrouwers.GetString(adresPos),
                                rdrBrouwers.GetInt16(postcodePos), rdrBrouwers.GetString(gemeentePos), omzet));
                        }
                    }
                }
            }
            return brouwers;
        }
        public List<String> GetPostCodes()
        {
            List<string> postnummers = new List<string>();
            var manager = new BierenDbManager();
            using (var conBrouwer = manager.GetConnection())
            {
                using (var comPostCodes = conBrouwer.CreateCommand())
                {
                    comPostCodes.CommandType = CommandType.StoredProcedure;
                    comPostCodes.CommandText = "PostCodes";
                    conBrouwer.Open();
                    using (var rdrPostCodes = comPostCodes.ExecuteReader())
                    {
                        Int32 postCodePos = rdrPostCodes.GetOrdinal("PostCode");
                        while (rdrPostCodes.Read())
                        {
                            postnummers.Add(rdrPostCodes.GetInt16(postCodePos).ToString());
                        }
                    }
                }
                return postnummers;
            }
        }
        public void SchrijfVerwijderingen(List<Brouwer> brouwers)
        {
            var manager = new BierenDbManager();
            using (var conBieren = manager.GetConnection())
            {
                using (var comDelete = conBieren.CreateCommand())
                {
                    comDelete.CommandType = CommandType.Text;
                    comDelete.CommandText = "delete from brouwers where BrouwerNr=@brouwernr";
                    var parBrouwerNr = comDelete.CreateParameter();
                    parBrouwerNr.ParameterName = "@brouwernr";
                    comDelete.Parameters.Add(parBrouwerNr);
                    conBieren.Open();
                    foreach (Brouwer eenBrouwer in brouwers)
                    {
                        parBrouwerNr.Value = eenBrouwer.BrouwerNr;
                        comDelete.ExecuteNonQuery();
                    }
                }
            }
        }
        public void SchrijfToevoegingen(List<Brouwer> brouwers)
        {
            var manager = new BierenDbManager();
            using (var conBieren = manager.GetConnection())
            {
                using (var comInsert = conBieren.CreateCommand())
                {
                    comInsert.CommandType = CommandType.Text;
                    comInsert.CommandText = "Insert into brouwers (BrNaam,Adres,Postcode,Gemeente,Omzet) values (@brnaam,@adres,@postcode,@gemeente,@omzet)";
                    var parBrNaam = comInsert.CreateParameter();
                    parBrNaam.ParameterName = "@brnaam";
                    comInsert.Parameters.Add(parBrNaam);
                    var parAdres = comInsert.CreateParameter();
                    parAdres.ParameterName = "@adres";
                    comInsert.Parameters.Add(parAdres);
                    var parPostcode = comInsert.CreateParameter();
                    parPostcode.ParameterName = "@postcode";
                    comInsert.Parameters.Add(parPostcode);
                    var parGemeente = comInsert.CreateParameter();
                    parGemeente.ParameterName = "@gemeente";
                    comInsert.Parameters.Add(parGemeente);
                    var parOmzet = comInsert.CreateParameter();
                    parOmzet.ParameterName = "@omzet";
                    comInsert.Parameters.Add(parOmzet);

                    conBieren.Open();

                    foreach (Brouwer eenBrouwer in brouwers)
                    {
                        parBrNaam.Value = eenBrouwer.BrNaam;
                        parAdres.Value = eenBrouwer.Adres;
                        parPostcode.Value = eenBrouwer.Postcode;
                        parGemeente.Value = eenBrouwer.Gemeente;
                        if (eenBrouwer.Omzet.HasValue)
                        { parOmzet.Value = eenBrouwer.Omzet; }
                        else
                        { parOmzet.Value = DBNull.Value; }
                        comInsert.ExecuteNonQuery();
                    }
                }
            }
        }
        public void SchrijfWijzigingen(List<Brouwer> brouwers)
        {
            var manager = new BierenDbManager();
            using (var conBieren = manager.GetConnection())
            {
                using(var comUpdate = conBieren.CreateCommand())
                {
                    comUpdate.CommandType = CommandType.Text;
                    comUpdate.CommandText = "update brouwers set BrNaam=@brnaam, Adres=@adres, Postcode=@postcode, Gemeente=@gemeente, Omzet=@omzet where BrouwerNr=@brouwernr";

                    var parBrNaam = comUpdate.CreateParameter();
                    parBrNaam.ParameterName = "@brnaam";
                    comUpdate.Parameters.Add(parBrNaam);

                    var parAdres = comUpdate.CreateParameter();
                    parAdres.ParameterName = "@adres";
                    comUpdate.Parameters.Add(parAdres);

                    var parPostcode = comUpdate.CreateParameter();
                    parPostcode.ParameterName = "@postcode";
                    comUpdate.Parameters.Add(parPostcode);

                    var parGemeente = comUpdate.CreateParameter();
                    parGemeente.ParameterName = "@gemeente";
                    comUpdate.Parameters.Add(parGemeente);

                    var parOmzet = comUpdate.CreateParameter();
                    parOmzet.ParameterName = "@omzet";
                    comUpdate.Parameters.Add(parOmzet);

                    var parBrouwerNr = comUpdate.CreateParameter();
                    parBrouwerNr.ParameterName = "@brouwernr";
                    comUpdate.Parameters.Add(parBrouwerNr);

                    conBieren.Open();
                    foreach (var eenBrouwer in brouwers)
                    {
                        parBrNaam.Value = eenBrouwer.BrNaam;
                        parAdres.Value = eenBrouwer.Adres;
                        parPostcode.Value = eenBrouwer.Postcode;
                        parGemeente.Value = eenBrouwer.Gemeente;
                        if (eenBrouwer.Omzet.HasValue) { parOmzet.Value = eenBrouwer.Omzet; }
                        else { parOmzet.Value = DBNull.Value; }
                        parBrouwerNr.Value = eenBrouwer.BrouwerNr;
                        comUpdate.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
