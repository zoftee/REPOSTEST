using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBS
{
    public class FindAncestors
    {
        private List<Models.SBSperson> mainlist { get; set; }


        public List<Models.SBSperson> testMeth(int id)
        {
            // ALGORITME 2

            List<Models.SBSperson> mainLst = new List<Models.SBSperson>();
            List<Models.SBSperson> FamListe = new List<Models.SBSperson>();
            List<Models.SBSperson> TempListe = new List<Models.SBSperson>();

            string result = "";
            int MainListCount = 0;
            int InitIdValue = 10403; // Jørgen
            //int InitIdValue = 7539;
            //int InitIdValue = 10345;
            //int InitIdValue = 5850;
            //int InitIdValue = 10970;
            // int InitIdValue = 10960;    // FEJLER

            int Loop = 1;
            int linkcount = 0;

            DataAccess dac = new DataAccess();
            mainLst = dac.GetSBSliste(0);



            
            //int.TryParse()

            FamListe = dac.GetSBSliste(id);



            try   // Alle data er ikke testede for validitet
            {

                while (Loop == 1)
                {

                   // int ERRCHECK = 0;
                    int farID = 0;
                    int morID = 0;

                    try
                        {   farID = FamListe[linkcount].RefTilFar; }
                        catch (Exception ex)
                        {   MessageBox.Show("Fejl i database vedr." + FamListe[linkcount].ID.ToString());
                            farID = 0;
                        }

                    try
                        {    morID = FamListe[linkcount].RefTilMor; }
                        catch (Exception ex)
                        {   MessageBox.Show("Fejl i database vedr." + FamListe[linkcount].ID.ToString());
                            morID = 0;
                        }

                    //int farID = FamListe[linkcount].RefTilFar;
                    //int morID = FamListe[linkcount].RefTilMor;

                    if (farID + morID + linkcount == 0)
                    {
                        // ÆGTEFÆLLE 
                        MessageBox.Show("Ægtefælle uden org tilhørsforhold til familie.");
                        return null;
                    }

                    if (farID != 0)
                    {
                        Models.SBSperson getFar = mainLst.Find(z => z.ID == farID);
                        FamListe.Add(getFar);
                    }

                    if (morID != 0)
                    {
                        Models.SBSperson getMor = mainLst.Find(z => z.ID == morID);
                        FamListe.Add(getMor);
                    }

                    Models.SBSperson last = FamListe.Last();
                    
                    if (last.Familienr == "00" || last.Familienr == "00X" )
                    {
                        return FamListe;
                    }
                    linkcount++;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Fejl i LOOP" + ex.Message);
            }

            return FamListe;
        }
    }
}
