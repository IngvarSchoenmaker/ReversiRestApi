using NUnit.Framework;
using ReversiRestApi.Interfaces;
using ReversiRestApi.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReversiRestApiNUnitTestproject
{
    [TestFixture]
    class SpelRepositoryTest
    {
        [Test]
        public void Toevoegen_Mogelijk_ReturnHoeveelheid()
        {
            //create spelrepository
            SpelRepository spellen = new SpelRepository();
            //create new spel
            Spel newSpel = new Spel();
            //bind info for new spel
            newSpel.Speler1Token = "abcdef";
            newSpel.Token = "test";
            newSpel.Omschrijving = "Potje snel reveri, dus niet lang nadenken";
            //hoeveelheid
            var starthoeveelheid = spellen.GetSpellen().Count;
            //add new spel
            spellen.AddSpel(newSpel);
            //hoeveelheid na toevoegen
            var uiteindelijkehoeveelheid = spellen.GetSpellen().Count;
            Assert.Less(starthoeveelheid, uiteindelijkehoeveelheid);
        }
        [Test]
        public void Vindspel_Mogelijk_ReturnSpel()
        {
            //create spelrepository
            SpelRepository spellen = new SpelRepository();
            //create new spel
            Spel newSpel = new Spel();
            //bind info for new spel
            newSpel.Speler1Token = "abcdef";
            newSpel.Token = "test";
            newSpel.Omschrijving = "Potje snel reveri, dus niet lang nadenken";
            //add new spel
            spellen.AddSpel(newSpel);
            var getSpel = spellen.GetSpel("test");
            Assert.AreEqual(newSpel, getSpel);
        }

    }
}
