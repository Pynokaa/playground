using System;
using System.Text.RegularExpressions;

namespace Fahrkartenautomat
{
    class Program
    {
        static void Main(string[] args)
        {
            //Deklarationen von Variablen
            int caseSwitch = (int)Vorgang.Bereit;
            string Ausgabe = " ";
            string tempa = " ";
            double Eingabe = 0;
            string temps;
            double tempd = 0;
            double Preis = 0;
            bool check = false;
            string pattern;
            string handler;
            Fahrkartenautomat Automat = new Fahrkartenautomat();

            // Loop über Switch bis Quit eingegeben wird
            while (caseSwitch != (int)Vorgang.Quit)
            {
                switch (caseSwitch)
                {
                    //Fahrkarte kaufen
                    case (int)Vorgang.FahrkarteKaufen:
                        temps = Convert.ToString(Eingabe);
                        Ausgabe = Automat.FahrkarteKaufen(temps);

                        //Fehlerhandling Zweig
                        if(Ausgabe == "Fahrkarte existiert nicht!")
                        {
                            Console.WriteLine(Ausgabe);
                            caseSwitch = (int)Vorgang.Bereit;
                            Console.ReadKey();
                        }
                        //Alles ok Zweig
                        else
                        {
                            Console.WriteLine("Preis: " + Ausgabe);
                            Console.WriteLine("Zum abbrechen bitte cancel eingeben!");
                            temps = Console.ReadLine();

                            //Abbruchbedingung
                            if (temps == "cancel")
                            {
                                caseSwitch = (int)Vorgang.Cancel;
                            }
                            else
                            {
                                caseSwitch = (int)Vorgang.Geldaufnahme;
                            }
                        }
                        break;

                    //Cancel
                    case (int)Vorgang.Cancel:
                        Console.WriteLine("Vorgang abgebrochen!");
                        caseSwitch = (int)Vorgang.Bereit;
                        Console.ReadKey();
                        break;

                    //Bereit
                    case (int)Vorgang.Bereit:
                        Console.WriteLine("Bitte wählen Sie eine Fahrkarte! Karten: 1, 2, 3");
                        Console.WriteLine("Zum Verlassen 4 eingeben!");
                        
                        //Abfangen von Conversion Error
                        try
                        {
                            Eingabe = Convert.ToDouble(Console.ReadLine());
                        }
                        catch (System.FormatException)
                        {
                            Console.WriteLine("Nicht zulässig!");
                            Console.ReadKey();
                            caseSwitch = (int)Vorgang.Bereit;
                            break;
                        }
                        
                        //Abbruchbedingung
                        if(Eingabe == 4)
                        {
                            caseSwitch = (int)Vorgang.Quit;
                        }
                        else
                        {
                            caseSwitch = (int)Vorgang.FahrkarteKaufen;
                        }
                        break;

                    //Geldaufnahme
                    case (int)Vorgang.Geldaufnahme:
                        tempa = Ausgabe;
                        Console.WriteLine("Bitte werfen Sie den Preis von " + Ausgabe + " ein!");
                        Console.WriteLine("Akzeptierte Münzen/Scheine: 0,50 ; 1,00 ; 2,00 ; 5,00 ; 10,00");
                        Console.WriteLine("Zum Verlassen cancel eingeben!");
                        handler = Console.ReadLine();

                        //Abfangen von Conversion Error
                        try
                        {
                            Eingabe = Convert.ToDouble(handler);
                        }
                        catch (System.FormatException)
                        {
                            //Abbruchbedingung
                            if(handler == "cancel")
                            {
                                caseSwitch = (int)Vorgang.Cancel;
                            }
                            else
                            {
                                Console.WriteLine("Nicht zulässig!");
                                Console.ReadKey();
                                caseSwitch = (int)Vorgang.Geldaufnahme;
                            }
                            break;
                        }
                        //Preisberechnung
                        Preis = Convert.ToDouble(Ausgabe);
                        tempa = Automat.Berechnung(Eingabe, Preis);

                        //Fehlerhandling
                        if(tempa == "Münze/Schein wird nicht akzeptiert!")
                        {
                            Console.WriteLine(tempa);
                            caseSwitch = (int)Vorgang.Geldaufnahme;
                            Console.ReadKey();
                            break;
                        }
                        else
                        {
                            tempd = Convert.ToDouble(tempa);
                        }
                        
                        //Loop solange bis Restbetrag = 0 oder Wechselgeld vorhanden
                        while (tempd != 0 && check != true)
                        {
                            pattern = "-";
                            check = tempa.Contains(pattern);
                            if (check == true)
                            {
                               break;
                            }
                            else
                            {
                                Error1:
                                Console.WriteLine("Bitte werfen Sie noch " + tempd.ToString("F2") + " ein!");

                                //Abfangen Conversion Error
                                try
                                {
                                    Eingabe = Convert.ToDouble(Console.ReadLine());
                                }
                                catch (System.FormatException)
                                {
                                    Console.WriteLine("Nicht zulässig!");
                                    goto Error1;
                                }
                                
                                Ausgabe = Automat.Berechnung(Eingabe, tempd);
                            }
                        }

                        //Wenn kein Wechselgeld -> Karten Ausgabe sonst -> Wechselgeldausgabe
                        if(tempd == 0)
                        {
                            caseSwitch = (int)Vorgang.FahrkarteAusgeben;
                        }
                        else
                        {
                            caseSwitch = (int)Vorgang.Wechselgeld;
                        }
                        break;

                    //Ausgabe Wechselgeld
                    case (int)Vorgang.Wechselgeld:
                        temps = Convert.ToString(tempd);
                        temps = temps.Replace("-", "");
                        tempd = Convert.ToDouble(temps);
                        Console.WriteLine("Ihr Wechselgeld beträgt: " + tempd.ToString("F2"));
                        caseSwitch = (int)Vorgang.FahrkarteAusgeben;
                        Console.ReadKey();
                        break;

                    //Ausgabe Fahrkarte
                    case (int)Vorgang.FahrkarteAusgeben:
                        Console.WriteLine("Bitte entnehmen Sie Ihre Fahrkarte!");
                        Console.ReadKey();
                        caseSwitch = (int)Vorgang.Bereit;
                        break;

                    //Quit
                    case (int)Vorgang.Quit:
                        break;

                    //Default
                    default:
                        caseSwitch = (int)Vorgang.Bereit;
                        break;
                }
            }

        }
        //Deklaration Enum
        enum Vorgang
        {
            FahrkarteKaufen = 1,
            Cancel = 2,
            Bereit = 3,
            Geldaufnahme = 4,
            Wechselgeld = 5,
            FahrkarteAusgeben = 6,
            Quit = 7

        }

        public class Fahrkartenautomat
        {
            public string FahrkarteKaufen(string Art)
            {
                int temp;
                temp = Convert.ToInt16(Art);

                if (Art == "1" || Art == "2" || Art == "3")
                {
                    switch (temp)
                    {
                        case 1:
                            return "2,50";

                        case 2:
                            return "3,50";

                        case 3:
                            return "5,00";

                        default:
                            return "Karte existiert nicht!";
                    }
                }
                else
                {
                    return "Fahrkarte existiert nicht!";
                }
            }

            public string Berechnung(double Einwurf, double Preis)
            {
                string Ausgabe;

                const double M050 = 0.50;
                const double M1 = 1.00;
                const double M2 = 2.00;
                const double M5 = 5.00;
                const double M10 = 10.00;

                if (Einwurf == M050 || Einwurf == M1 || Einwurf == M2 || Einwurf == M5 || Einwurf == M10)
                {
                    Preis = Preis - Einwurf;
                    Ausgabe = Convert.ToString(Preis);
                    return Ausgabe;
                }
                else
                {
                    return "Münze/Schein wird nicht akzeptiert!";   
                }
            }
        }

    }
}
