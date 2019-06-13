using System.Collections.Generic;
using System.Linq;
using System;
//using Microsoft.Office.Interop.Excel;
//using Excel = Microsoft.Office.Interop.Excel;
//using System.Runtime.InteropServices;
using OfficeOpenXml;
using System.IO;

namespace SoftAnalyzerClient
{
    public class Podmiot
    {
        public string NazwaPodmiotu { get; set; }
        public int LiczbaAtrybutowProperty { get; set; }
        public int LiczbaMetodProperty { get; set; }
        public LinkedList<ServiceReference1.atrybutyPlikow> LiczbaAtrybutowWKlasach { get; set; }
        public List<string> ZbiorBibliotekProperty { get; set; }
        public List<string> ZbiorWykorzystywanychPlikowProperty { get; set; }
        public List<string> ZbiorWykorzystywanychAdresowProperty { get; set; }
        public List<string> ZbiorWykorzystywanychPortowProperty { get; set; }
        public int LiczbaPlikowProperty { get; set; }
        public LinkedList<ServiceReference1.liczbaLiniiKodu> LiczbaLiniiKoduProperty { get; set; }
        public LinkedList<ServiceReference1.rozmiaryPlikow> RozmiaryPlikowKodowZrodlowychProperty { get; set; }
        public string JezykProgramowaniaProperty { get; set; }
        public int LiczbaZnakowProperty { get; set; }
        public List<string> ListaNazwPlikow { get; set; }
        public List<string> ListaNazwKatalogow { get; set; }
        public LinkedList<ServiceReference1.rozszerzeniaPlikow> LiczbaPlikowODanymRozszerzeniuProperty { get; set; }
        public LinkedList<ServiceReference1.typyPlikow> LiczbaPlikowDanegoTypuProperty { get; set; }
        public bool MozliwoscWczytywaniaPlikowProperty { get; set; }
        public int LiczbaDanychWejsciowychProperty { get; set; }
        public string JezykInterfejsuProperty { get; set; }
        public LinkedList<ServiceReference1.typyZmiennych> LiczbaZmiennychDanegoTypuProperty { get; set; }
        public LinkedList<ServiceReference1.hashePlikow> SkrotyPlikowProperty { get; set; }
        public string ParadygmatProperty { get; set; }
        public bool MechanizmWielowatkowosciProperty { get; set; }

        //public static _Application excel = new Excel.Application();

        public Podmiot(ServiceReference1.ServiceSAClient soap, string link)
        {
            this.NazwaPodmiotu = link.Split('/').Last();
            soap.przeslijPlik(link);
            this.SkrotyPlikowProperty = new LinkedList<ServiceReference1.hashePlikow>(soap.getSkrotyPlikow(NazwaPodmiotu));
            this.LiczbaPlikowProperty = soap.getLiczbaPlikow(NazwaPodmiotu);
            this.LiczbaMetodProperty = soap.getLiczbaMetod(NazwaPodmiotu);
            this.LiczbaAtrybutowProperty = soap.getLiczbaAtrybutow(NazwaPodmiotu);
            this.LiczbaAtrybutowWKlasach = new LinkedList<ServiceReference1.atrybutyPlikow>(soap.getLiczbaAtrybutowWKlasach(NazwaPodmiotu));
            this.ZbiorBibliotekProperty = new List<string>(soap.getZbiorBibliotek(NazwaPodmiotu));
            this.LiczbaLiniiKoduProperty = new LinkedList<ServiceReference1.liczbaLiniiKodu>(soap.getLiczbaLiniiKodu(NazwaPodmiotu));
            this.RozmiaryPlikowKodowZrodlowychProperty = new LinkedList<ServiceReference1.rozmiaryPlikow>(soap.getRozmiaryPlikowKodow(NazwaPodmiotu));
            this.JezykProgramowaniaProperty = soap.getJezykProgramowania(NazwaPodmiotu);
            this.LiczbaZnakowProperty = soap.getLiczbaZnakow(NazwaPodmiotu);
            this.ListaNazwPlikow = new List<string>(soap.getListaNazwPlikow(NazwaPodmiotu));
            this.ListaNazwKatalogow = new List<string>(soap.getListaNazwKatalogow(NazwaPodmiotu));
            this.LiczbaPlikowODanymRozszerzeniuProperty = new LinkedList<ServiceReference1.rozszerzeniaPlikow>(soap.getLiczbaPlikowDanegoRozszerzenia(NazwaPodmiotu));
            this.LiczbaPlikowDanegoTypuProperty = new LinkedList<ServiceReference1.typyPlikow>(soap.getLiczbaPlikowDanegoTypu(NazwaPodmiotu));
            this.MozliwoscWczytywaniaPlikowProperty = soap.getMozliwosciWczytywaniaPlikow(NazwaPodmiotu);
            this.LiczbaDanychWejsciowychProperty = soap.getLiczbaDanychWejsciowych(NazwaPodmiotu);
            this.JezykInterfejsuProperty = soap.getJezykInterfejsu(NazwaPodmiotu);
            this.LiczbaZmiennychDanegoTypuProperty = new LinkedList<ServiceReference1.typyZmiennych>(soap.getLiczbaZmiennychDanegoTypu(NazwaPodmiotu));
            this.ParadygmatProperty = soap.getParadygmat(NazwaPodmiotu);
            this.MechanizmWielowatkowosciProperty = soap.getWykorzystanieWielowatkowosci(NazwaPodmiotu);

            string[] temp1 = soap.getZbiorWykorzystywanychAdresow(NazwaPodmiotu);
            if (temp1 != null)
            {
                this.ZbiorWykorzystywanychAdresowProperty = new List<string>(soap.getZbiorWykorzystywanychAdresow(NazwaPodmiotu));
            }
            else
            {
                this.ZbiorWykorzystywanychAdresowProperty = new List<string>();
            }
            string[] temp2 = soap.getZbiorWykorzystywanychPortow(NazwaPodmiotu);
            if (temp2 != null)
            {
                this.ZbiorWykorzystywanychPortowProperty = new List<string>(soap.getZbiorWykorzystywanychPortow(NazwaPodmiotu));
            }
            else
            {
                this.ZbiorWykorzystywanychPortowProperty = new List<string>();
            }
            string[] temp3 = soap.getZbiorWykorzystywanychPlikow(NazwaPodmiotu);
            if (temp3 != null)
            {
                this.ZbiorWykorzystywanychPlikowProperty = new List<string>(soap.getZbiorWykorzystywanychPlikow(NazwaPodmiotu));
            }
            else
            {
                this.ZbiorWykorzystywanychPlikowProperty = new List<string>();
            }


            //plikExcel();
          
        }

        //public void PlikExcel(string nazwaSkoroszytu)
        //{
        //    string fileName = string.Format(@"{0}\Metryki.xlsx", Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));

        //    Excel._Worksheet workSheet;

        //    if (File.Exists(fileName))
        //    {
        //        excel.Workbooks.Open(string.Format(fileName));

        //        workSheet = (Excel.Worksheet)excel.Worksheets.Add();
        //        // Create Worksheet from active sheet

        //    }
        //    else
        //    {
        //        excel.Workbooks.Add();
        //        workSheet = excel.ActiveSheet;
        //    }

        //    bool flaga = false;
        //    foreach (Excel._Worksheet item in excel.Worksheets)
        //    {
        //        if (item.Name.Equals(nazwaSkoroszytu))
        //        {
        //            workSheet.Name = nazwaSkoroszytu + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond;
        //            flaga = true;
        //            break;
        //        }
        //    }

        //    if (!flaga)
        //    {
        //        workSheet.Name = nazwaSkoroszytu;
        //    }

        //    // I created Application and Worksheet objects before try/catch,
        //    // so that i can close them in finnaly block.
        //    // It's IMPORTANT to release these COM objects!!
        //    try
        //    {
        //        // ------------------------------------------------
        //        // Creation of header cells
        //        // ------------------------------------------------
        //        workSheet.Cells[1, "A"] = "Liczba zmiennych";
        //        workSheet.Cells[2, "A"] = LiczbaAtrybutowProperty;
        //        workSheet.Cells[1, "B"] = "Liczba metod";
        //        workSheet.Cells[2, "B"] = LiczbaMetodProperty;

        //        workSheet.Cells[1, "C"] = "Liczba zmiennych w plikach";
        //        workSheet.Cells[2, "C"] = "Nazwa pliku";
        //        workSheet.Cells[2, "D"] = "Liczba zmiennych";
        //        workSheet.Range[workSheet.Cells[1, "C"], workSheet.Cells[1, "D"]].Merge();
        //        // ------------------------------------------------
        //        // Populate sheet with some real data from "cars" list
        //        // ------------------------------------------------
        //        int row = 3; // start row (in row 1 are header cells)
        //        foreach (var atrybutyPlikow in LiczbaAtrybutowWKlasach)
        //        {
        //            workSheet.Cells[row, "C"] = atrybutyPlikow.nazwa;
        //            workSheet.Cells[row, "D"] = atrybutyPlikow.liczba;
        //            row++;
        //        }

        //        workSheet.Cells[1, "E"] = "Zbior bibliotek";

        //        row = 2; // start row (in row 1 are header cells)
        //        foreach (var biblioteka in ZbiorBibliotekProperty)
        //        {
        //            workSheet.Cells[row, "E"] = biblioteka;
        //            row++;
        //        }

        //        row = 2;
        //        workSheet.Cells[1, "F"] = "Zbior wykorzystywanych plikow";
        //        foreach (var biblioteka in ZbiorWykorzystywanychPlikowProperty)
        //        {
        //            workSheet.Cells[row, "F"] = biblioteka;
        //            row++;
        //        }

        //        row = 2;
        //        workSheet.Cells[1, "G"] = "Zbior wykorzystywanych adresow";
        //        foreach (var biblioteka in ZbiorWykorzystywanychAdresowProperty)
        //        {
        //            workSheet.Cells[row, "G"] = biblioteka;
        //            row++;
        //        }

        //        row = 2;
        //        workSheet.Cells[1, "H"] = "Zbior wykorzystywanych portow";
        //        foreach (var biblioteka in ZbiorWykorzystywanychPortowProperty)
        //        {
        //            workSheet.Cells[row, "H"] = biblioteka;
        //            row++;
        //        }

        //        workSheet.Cells[1, "I"] = "Liczba plikow";
        //        workSheet.Cells[2, "I"] = LiczbaPlikowProperty;

        //        workSheet.Cells[1, "J"] = "Liczba linii kodu";
        //        workSheet.Cells[2, "J"] = "Nazwa pliku";
        //        workSheet.Cells[2, "K"] = "Liczba linii kodu";
        //        workSheet.Range[workSheet.Cells[1, "J"], workSheet.Cells[1, "K"]].Merge();

        //        row = 3;
        //        foreach (var biblioteka in LiczbaLiniiKoduProperty)
        //        {
        //            workSheet.Cells[row, "J"] = biblioteka.nazwa;
        //            workSheet.Cells[row, "K"] = biblioteka.liczbaLinii;
        //            row++;
        //        }

        //        workSheet.Cells[1, "L"] = "Rozmiary plikow kodow zrodlowych";
        //        workSheet.Cells[2, "L"] = "Nazwa pliku";
        //        workSheet.Cells[2, "M"] = "Rozmiar";
        //        workSheet.Range[workSheet.Cells[1, "L"], workSheet.Cells[1, "M"]].Merge();
        //        row = 3;
        //        foreach (var biblioteka in RozmiaryPlikowKodowZrodlowychProperty)
        //        {
        //            workSheet.Cells[row, "L"] = biblioteka.nazwa;
        //            workSheet.Cells[row, "M"] = biblioteka.rozmiar;
        //            row++;
        //        }

        //        workSheet.Cells[1, "N"] = "Jezyk programowania";
        //        workSheet.Cells[2, "N"] = JezykProgramowaniaProperty;

        //        workSheet.Cells[1, "O"] = "Liczba znakow";
        //        workSheet.Cells[2, "O"] = LiczbaZnakowProperty;

        //        workSheet.Cells[1, "P"] = "ListaNazwPlikow";
        //        row = 2;
        //        foreach (var biblioteka in ListaNazwPlikow)
        //        {
        //            workSheet.Cells[row, "P"] = biblioteka;
        //            row++;
        //        }

        //        workSheet.Cells[1, "Q"] = "Lista nazw katalogow";
        //        row = 2;
        //        foreach (var biblioteka in ListaNazwKatalogow)
        //        {
        //            workSheet.Cells[row, "Q"] = biblioteka;
        //            row++;
        //        }

        //        workSheet.Cells[1, "R"] = "Liczba plikow o danym rozszerzeniu";
        //        workSheet.Cells[2, "R"] = "Rozszerzenie";
        //        workSheet.Cells[2, "S"] = "Liczba plików";
        //        workSheet.Range[workSheet.Cells[1, "R"], workSheet.Cells[1, "S"]].Merge();

        //        row = 3;
        //        foreach (var biblioteka in LiczbaPlikowODanymRozszerzeniuProperty)
        //        {
        //            workSheet.Cells[row, "R"] = biblioteka.rozszerzenie;
        //            workSheet.Cells[row, "S"] = biblioteka.liczba;
        //            row++;
        //        }

        //        workSheet.Cells[1, "T"] = "Liczba plikow danego typu";
        //        workSheet.Cells[2, "T"] = "Typ plików";
        //        workSheet.Cells[2, "U"] = "Liczba plików";
        //        workSheet.Range[workSheet.Cells[1, "T"], workSheet.Cells[1, "U"]].Merge();
        //        row = 3;
        //        foreach (var biblioteka in LiczbaPlikowDanegoTypuProperty)
        //        {
        //            workSheet.Cells[row, "T"] = biblioteka.typ;
        //            workSheet.Cells[row, "U"] = biblioteka.liczba;
        //            row++;
        //        }

        //        workSheet.Cells[1, "V"] = "Mozliwosc wczytywania plikow";
        //        workSheet.Cells[2, "V"] = MozliwoscWczytywaniaPlikowProperty;

        //        workSheet.Cells[1, "W"] = "Liczba danych wejsciowych";
        //        workSheet.Cells[2, "W"] = LiczbaDanychWejsciowychProperty;

        //        workSheet.Cells[1, "X"] = "Lokalizacja interfejsu";
        //        workSheet.Cells[2, "X"] = JezykInterfejsuProperty;

        //        workSheet.Cells[1, "Y"] = "Liczba zmiennych danego typu";
        //        workSheet.Cells[2, "Y"] = "Typ zmiennej";
        //        workSheet.Cells[2, "Z"] = "Liczba zmiennych";
        //        workSheet.Range[workSheet.Cells[1, "Y"], workSheet.Cells[1, "Z"]].Merge();
        //        row = 3;
        //        foreach (var biblioteka in LiczbaZmiennychDanegoTypuProperty)
        //        {
        //            workSheet.Cells[row, "Y"] = biblioteka.typ;
        //            workSheet.Cells[row, "Z"] = biblioteka.liczba;
        //            row++;
        //        }

        //        workSheet.Cells[1, "AA"] = "Skroty plikow";
        //        workSheet.Cells[2, "AA"] = "Nazwa pliku";
        //        workSheet.Cells[2, "AB"] = "Skrót pliku";
        //        workSheet.Range[workSheet.Cells[1, "AA"], workSheet.Cells[1, "AB"]].Merge();
        //        row = 3;
        //        foreach (var biblioteka in SkrotyPlikowProperty)
        //        {
        //            workSheet.Cells[row, "AA"] = biblioteka.nazwa;
        //            workSheet.Cells[row, "AB"] = biblioteka.hash;
        //            row++;
        //        }

        //        workSheet.Cells[1, "AC"] = "Paradygmat";
        //        workSheet.Cells[2, "AC"] = ParadygmatProperty;

        //        workSheet.Cells[1, "AD"] = "Mechanizm wielowatkowosci";
        //        workSheet.Cells[2, "AD"] = MechanizmWielowatkowosciProperty;



        //        // Apply some predefined styles for data to look nicely :)
        //        workSheet.Range["A1"].AutoFormat(Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic1);

        //        excel.DisplayAlerts = false;

        //        if (File.Exists(fileName))
        //        {
        //            excel.Workbooks[0].SaveAs(fileName, Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
        //        }
        //        else
        //        {
        //            workSheet.SaveAs(fileName);
        //        }
        //        excel.DisplayAlerts = true;

        //    }
        //    catch (Exception exception)
        //    {

        //    }
        //    finally
        //    {
        //        if (workSheet != null)
        //            System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);

        //        // Empty variables
        //        workSheet = null;

        //        // Force garbage collector cleaning
        //        GC.Collect();
        //    }


        //}

        public void PlikExcelNew(string nazwaSkoroszytu)
        {
            string fileName = string.Format(@"{0}\MetrykiDane.xlsx", Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
            //string fileName = filePath;

            using (var package = new ExcelPackage(new FileInfo(fileName)))
            {
                string tmpNazwaSkoroszytu = nazwaSkoroszytu;
                int interator = 0;
                
                while (package.Workbook.Worksheets.Select(x => x.Name).Contains(tmpNazwaSkoroszytu))
                {
                    tmpNazwaSkoroszytu = nazwaSkoroszytu + "_" + interator;
                    interator++;
                }
                ExcelWorksheet workSheet = package.Workbook.Worksheets.Add(tmpNazwaSkoroszytu);

                workSheet.Cells["A1"].Value = "Liczba zmiennych";
                workSheet.Cells["A2"].Value = LiczbaAtrybutowProperty;
                workSheet.Cells["B1"].Value = "Liczba metod";
                workSheet.Cells["B2"].Value = LiczbaMetodProperty;

                workSheet.Cells["C1"].Value = "Liczba zmiennych w plikach";
                workSheet.Cells["C2"].Value = "Nazwa pliku";
                workSheet.Cells["D2"].Value = "Liczba zmiennych";
                workSheet.Cells["C1:D1"].Merge = true;

                int row = 3; // start row (in row 1 are header cells)
                foreach (var atrybutyPlikow in LiczbaAtrybutowWKlasach)
                {
                    workSheet.Cells["C" + row].Value = atrybutyPlikow.nazwa;
                    workSheet.Cells["D" + row].Value = atrybutyPlikow.liczba;
                    row++;
                }

                workSheet.Cells["E1"].Value = "Zbior bibliotek";

                row = 2; // start row (in row 1 are header cells)
                foreach (var biblioteka in ZbiorBibliotekProperty)
                {
                    workSheet.Cells["E" + row].Value = biblioteka;
                    row++;
                }

                row = 2;
                workSheet.Cells["F1"].Value = "Zbior wykorzystywanych plikow";
                foreach (var biblioteka in ZbiorWykorzystywanychPlikowProperty)
                {
                    workSheet.Cells["F" + row].Value = biblioteka;
                    row++;
                }

                row = 2;
                workSheet.Cells["G1"].Value = "Zbior wykorzystywanych adresow";
                foreach (var biblioteka in ZbiorWykorzystywanychAdresowProperty)
                {
                    workSheet.Cells["G" + row].Value = biblioteka;
                    row++;
                }

                row = 2;
                workSheet.Cells["H1"].Value = "Zbior wykorzystywanych portow";
                foreach (var biblioteka in ZbiorWykorzystywanychPortowProperty)
                {
                    workSheet.Cells["H" + row].Value = biblioteka;
                    row++;
                }

                workSheet.Cells["I1"].Value = "Liczba plikow";
                workSheet.Cells["I2"].Value = LiczbaPlikowProperty;

                workSheet.Cells["J1"].Value = "Liczba linii kodu";
                workSheet.Cells["J2"].Value = "Nazwa pliku";
                workSheet.Cells["K2"].Value = "Liczba linii kodu";
                workSheet.Cells["J1:K1"].Merge = true;

                row = 3;
                foreach (var biblioteka in LiczbaLiniiKoduProperty)
                {
                    workSheet.Cells["J" + row].Value = biblioteka.nazwa;
                    workSheet.Cells["K" + row].Value = biblioteka.liczbaLinii;
                    row++;
                }

                workSheet.Cells["L1"].Value = "Rozmiary plikow kodow zrodlowych";
                workSheet.Cells["L2"].Value = "Nazwa pliku";
                workSheet.Cells["M2"].Value = "Rozmiar";
                workSheet.Cells["L1:M1"].Merge = true;

                row = 3;
                foreach (var biblioteka in RozmiaryPlikowKodowZrodlowychProperty)
                {
                    workSheet.Cells["L" + row].Value = biblioteka.nazwa;
                    workSheet.Cells["M" + row].Value = biblioteka.rozmiar;
                    row++;
                }

                workSheet.Cells["N1"].Value = "Jezyk programowania";
                workSheet.Cells["N2"].Value = JezykProgramowaniaProperty;

                workSheet.Cells["O1"].Value = "Liczba znakow";
                workSheet.Cells["O2"].Value = LiczbaZnakowProperty;

                workSheet.Cells["P1"].Value = "ListaNazwPlikow";
                row = 2;
                foreach (var biblioteka in ListaNazwPlikow)
                {
                    workSheet.Cells["P" + row].Value = biblioteka;
                    row++;
                }

                workSheet.Cells["Q1"].Value = "Lista nazw katalogow";
                row = 2;
                foreach (var biblioteka in ListaNazwKatalogow)
                {
                    workSheet.Cells["Q" + row].Value = biblioteka;
                    row++;
                }

                workSheet.Cells["R1"].Value = "Liczba plikow o danym rozszerzeniu";
                workSheet.Cells["R2"].Value = "Rozszerzenie";
                workSheet.Cells["S2"].Value = "Liczba plików";
                workSheet.Cells["R1:S1"].Merge = true;

                row = 3;
                foreach (var biblioteka in LiczbaPlikowODanymRozszerzeniuProperty)
                {
                    workSheet.Cells["R" + row].Value = biblioteka.rozszerzenie;
                    workSheet.Cells["S" + row].Value = biblioteka.liczba;
                    row++;
                }

                workSheet.Cells["T1"].Value = "Liczba plikow danego typu";
                workSheet.Cells["T2"].Value = "Typ plików";
                workSheet.Cells["U2"].Value = "Liczba plików";
                workSheet.Cells["T1:U1"].Merge = true;

                row = 3;
                foreach (var biblioteka in LiczbaPlikowDanegoTypuProperty)
                {
                    workSheet.Cells["T" + row].Value = biblioteka.typ;
                    workSheet.Cells["U" + row].Value = biblioteka.liczba;
                    row++;
                }

                workSheet.Cells["V1"].Value = "Mozliwosc wczytywania plikow";
                workSheet.Cells["V2"].Value = MozliwoscWczytywaniaPlikowProperty;

                workSheet.Cells["W1"].Value = "Liczba danych wejsciowych";
                workSheet.Cells["W2"].Value = LiczbaDanychWejsciowychProperty;

                workSheet.Cells["X1"].Value = "Lokalizacja interfejsu";
                workSheet.Cells["X2"].Value = JezykInterfejsuProperty;

                workSheet.Cells["Y1"].Value = "Liczba zmiennych danego typu";
                workSheet.Cells["Y2"].Value = "Typ zmiennej";
                workSheet.Cells["Z2"].Value = "Liczba zmiennych";
                workSheet.Cells["Y1:Z1"].Merge = true;

                row = 3;
                foreach (var biblioteka in LiczbaZmiennychDanegoTypuProperty)
                {
                    workSheet.Cells["Y" + row].Value = biblioteka.typ;
                    workSheet.Cells["Z" + row].Value = biblioteka.liczba;
                    row++;
                }

                workSheet.Cells["AA1"].Value = "Skroty plikow";
                workSheet.Cells["AA2"].Value = "Nazwa pliku";
                workSheet.Cells["AB2"].Value = "Skrót pliku";
                workSheet.Cells["AA1:AB1"].Merge = true;

                row = 3;
                foreach (var biblioteka in SkrotyPlikowProperty)
                {
                    workSheet.Cells["AA" + row].Value = biblioteka.nazwa;
                    workSheet.Cells["AB" + row].Value = biblioteka.hash;
                    row++;
                }

                workSheet.Cells["AC1"].Value = "Paradygmat";
                workSheet.Cells["AC2"].Value = ParadygmatProperty;

                workSheet.Cells["AD1"].Value = "Mechanizm wielowatkowosci";
                workSheet.Cells["AD2"].Value = MechanizmWielowatkowosciProperty;



                // Apply some predefined styles for data to look nicely :)
               
                workSheet.Cells.AutoFitColumns(0);
                //package.SaveAs(new FileInfo(fileName));
                package.Save();

            }

            
        }
    }
}
