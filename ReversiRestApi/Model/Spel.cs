using ReversiRestApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReversiRestApi.Model
{
    public class Spel : ISpel
    {
        
        public int ID { get; set;}
        public string Omschrijving { get; set; }
        public string Token {get; set;}
        public string Speler1Token {get; set;}
        public string Speler2Token {get; set;}
        public Kleur[,] Bord {get; set;}
        public Kleur AandeBeurt {get; set;}
        public Spel()
        {
            Bord = new Kleur[8, 8];
            Bord[3, 3] = Kleur.Wit;
            Bord[4, 4] = Kleur.Wit;
            Bord[3, 4] = Kleur.Zwart;
            Bord[4, 3] = Kleur.Zwart;
        }

        public bool Afgelopen()
        {
            for (int i = 0; i < Bord.GetLength(0); i++)
            {
                for (int j = 0; j < Bord.GetLength(1); j++)
                {
                    if (ZetMogelijk(i, j))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool DoeZet(int rijZet, int kolomZet)
        {
            if (ZetMogelijk(rijZet, kolomZet))
            {
                Vervang(rijZet, kolomZet);
                Bord[rijZet, kolomZet] = AandeBeurt;
                VeranderAandebeurt();
                return true;
            }
            return false;
        }

        public Kleur OverwegendeKleur()
        {
            int wit = 0;
            int zwart = 0;
            foreach (var kleur in Bord)
            {
                if (kleur == Kleur.Wit)
                {
                    wit++;
                }
                if (kleur == Kleur.Zwart)
                {
                    zwart++;
                }
            }
            if (wit > zwart)
            {
                return Kleur.Wit;
            }
            if (zwart > wit)
            {
                return Kleur.Zwart;
            }
            return Kleur.Geen;
        }

        public bool Pas()
        {
            VeranderAandebeurt();
            return true;
        }

        public bool ZetMogelijk(int rijZet, int kolomZet)
        {
            if (BinnenBord(rijZet, kolomZet))
            {
                if (Bord[rijZet,kolomZet] == Kleur.Geen)
                {
                    if (TegenstanderNaast(rijZet, kolomZet))
                    {
                        if (InRij(rijZet, kolomZet) || InSchuin(rijZet, kolomZet))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool TegenstanderNaast(int rijZet, int kolomZet)
        {
            bool IsErTegenstander = false;

            //tegenstander standaard wit
            Kleur tegenstander = Kleur.Wit;

            //Check wie de tegenstander is
            if (AandeBeurt == Kleur.Wit){ tegenstander = Kleur.Zwart; }

            //Check of er een tegenstander link boven staat
            if (BinnenBord(rijZet - 1, kolomZet - 1))
            {
                if (Bord[rijZet - 1, kolomZet - 1] == tegenstander) { IsErTegenstander = true; }
            }

            //Check of er een tegenstander boven staat
            if (BinnenBord(rijZet - 1, kolomZet))
            {
                if (Bord[rijZet - 1, kolomZet] == tegenstander) { IsErTegenstander = true; }

            }

            //Check of er een tegenstander rechts boven staat
            if (BinnenBord(rijZet - 1, kolomZet + 1))
            {
                if (Bord[rijZet - 1, kolomZet + 1] == tegenstander) { IsErTegenstander = true; }
            }

            //Check of er een tegenstander link staat
            if (BinnenBord(rijZet, kolomZet - 1))
            {
                if (Bord[rijZet, kolomZet - 1] == tegenstander) { IsErTegenstander = true; }
            }

            //Check of er een tegenstander rechts staat
            if (BinnenBord(rijZet, kolomZet + 1))
            {
                if (Bord[rijZet, kolomZet + 1] == tegenstander) { IsErTegenstander = true; }
            }

            //Check of er een tegenstander link onder staat
            if (BinnenBord(rijZet + 1, kolomZet - 1))
            {
                if (Bord[rijZet + 1, kolomZet - 1] == tegenstander) { IsErTegenstander = true; }
            }

            //Check of er een tegenstander onder staat
            if (BinnenBord(rijZet + 1, kolomZet))
            {
                if (Bord[rijZet + 1, kolomZet] == tegenstander) { IsErTegenstander = true; }
            }

            //Check of er een tegenstander rechts onder staat
            if (BinnenBord(rijZet + 1, kolomZet + 1))
            {
                if (Bord[rijZet + 1, kolomZet + 1] == tegenstander) { IsErTegenstander = true; }
            }

            return IsErTegenstander;
        }

        private bool InRij(int rijZet, int kolomZet)
        {
            // van boven naar beneden
            for (int i = 0; i < Bord.GetLength(0); i++)
            {
                if (Bord[i, kolomZet] == AandeBeurt)
                {
                    return true;
                }
            }
            // val links naar rechts
            for (int j = 0; j < Bord.GetLength(1); j++)
            {
                if (Bord[rijZet, j] == AandeBeurt)
                {
                    return true;
                }
            }
            return false;
        }

        private bool InSchuin(int rijZet, int kolomZet)
        {
            var hoeveelheid = 1;
            // links onder
            while (BinnenBord(rijZet + hoeveelheid, kolomZet - hoeveelheid))
            {
                if (Bord[rijZet + hoeveelheid, kolomZet - hoeveelheid] == AandeBeurt)
                {
                    while (BinnenBord(rijZet + hoeveelheid, kolomZet - hoeveelheid))
                    {
                        return true;
                    }
                }
                hoeveelheid++;
            }
            // links boven
            hoeveelheid = 1;
            while (BinnenBord(rijZet - hoeveelheid, kolomZet - hoeveelheid))
            {
                if (Bord[rijZet - hoeveelheid, kolomZet - hoeveelheid] == AandeBeurt)
                {
                    return true;
                }
                hoeveelheid++;
            }

            // rechts onder
            hoeveelheid = 1;
            while (BinnenBord(rijZet + hoeveelheid, kolomZet + hoeveelheid))
            {
                if (Bord[rijZet + hoeveelheid, kolomZet + hoeveelheid] == AandeBeurt)
                {
                    return true;
                }
                hoeveelheid++;
            }

            // rechts boven
            hoeveelheid = 1;
            while (BinnenBord(rijZet - hoeveelheid, kolomZet + hoeveelheid))
            {
                if (Bord[rijZet - hoeveelheid, kolomZet + hoeveelheid] == AandeBeurt)
                {
                    return true;
                }
                hoeveelheid++;
            }

            return false;
        }

        private bool BinnenBord(int rijZet, int kolomZet)
        {
            return rijZet >= 0 && rijZet <= 7 && kolomZet >= 0 && kolomZet <= 7;
        }

        private void Vervang(int rijZet, int kolomZet)
        {
            var hoeveelheid = 1;
            // van zet naar beneden
            while (BinnenBord(rijZet + hoeveelheid, kolomZet))
            {
                if (Bord[rijZet + hoeveelheid, kolomZet] == AandeBeurt)
                {
                    for (int i = 0; i < hoeveelheid; i++)
                    {
                        Bord[rijZet + i, kolomZet] = AandeBeurt;
                    };
                    break;
                }
                hoeveelheid++;
            }
            // van zet naar links
            hoeveelheid = 1;
            while (BinnenBord(rijZet, kolomZet - hoeveelheid))
            {
                if (Bord[rijZet, kolomZet - hoeveelheid] == AandeBeurt)
                {
                    for (int i = 0; i < hoeveelheid; i++)
                    {
                        Bord[rijZet, kolomZet - i] = AandeBeurt;
                    };
                    break;
                }
                hoeveelheid++;
            }
            // van zet naar boven
            hoeveelheid = 1;
            while (BinnenBord(rijZet - hoeveelheid, kolomZet))
            {
                if (Bord[rijZet - hoeveelheid, kolomZet] == AandeBeurt)
                {
                    for (int i = 0; i < hoeveelheid; i++)
                    {
                        Bord[rijZet - i, kolomZet] = AandeBeurt;
                    };
                    break;
                }
                hoeveelheid++;
            }
            // van zet naar rechts
            hoeveelheid = 1;
            while (BinnenBord(rijZet, kolomZet + hoeveelheid))
            {
                if (Bord[rijZet, kolomZet + hoeveelheid] == AandeBeurt)
                {
                    for (int i = 0; i < hoeveelheid; i++)
                    {
                        Bord[rijZet, kolomZet + i] = AandeBeurt;
                    };
                    break;
                }
                hoeveelheid++;
            }
            // van zet naar rechts boven
            hoeveelheid = 1;
            while (BinnenBord(rijZet - hoeveelheid, kolomZet + hoeveelheid))
            {
                if (Bord[rijZet - hoeveelheid, kolomZet + hoeveelheid] == AandeBeurt)
                {
                    for (int i = 0; i < hoeveelheid; i++)
                    {
                        Bord[rijZet - i, kolomZet + i] = AandeBeurt;
                    };
                    break;
                }
                hoeveelheid++;
            }
            // van zet naar links onder
            hoeveelheid = 1;
            while (BinnenBord(rijZet + hoeveelheid, kolomZet - hoeveelheid))
            {
                if (Bord[rijZet + hoeveelheid, kolomZet - hoeveelheid] == AandeBeurt)
                {
                    for (int i = 0; i < hoeveelheid; i++)
                    {
                        Bord[rijZet + i, kolomZet - i] = AandeBeurt;
                    };
                    break;
                }
                hoeveelheid++;
            }
            // van zet naar links boven
            hoeveelheid = 1;
            while (BinnenBord(rijZet - hoeveelheid, kolomZet - hoeveelheid))
            {
                if (Bord[rijZet - hoeveelheid, kolomZet - hoeveelheid] == AandeBeurt)
                {
                    for (int i = 0; i < hoeveelheid; i++)
                    {
                        Bord[rijZet - i, kolomZet - i] = AandeBeurt;
                    };
                    break;
                }
                hoeveelheid++;
            }
            // van zet naar rechts onder
            hoeveelheid = 1;
            while (BinnenBord(rijZet + hoeveelheid, kolomZet + hoeveelheid))
            {
                if (Bord[rijZet + hoeveelheid, kolomZet + hoeveelheid] == AandeBeurt)
                {
                    for (int i = 0; i < hoeveelheid; i++)
                    {
                        Bord[rijZet + i, kolomZet + i] = AandeBeurt;
                    };
                    break;
                }
                hoeveelheid++;
            }
        }

        private void VeranderAandebeurt()
        {
            //Check wie de tegenstander is
            if (AandeBeurt == Kleur.Wit) { AandeBeurt = Kleur.Zwart; }
            else if (AandeBeurt == Kleur.Zwart) { AandeBeurt = Kleur.Wit; }
        }
    }
}
