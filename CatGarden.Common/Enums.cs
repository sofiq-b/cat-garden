using static CatGarden.Common.Enums;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Text;
using System.Xml;
using System;

namespace CatGarden.Common
{
    public class Enums
    {
        public enum Breed
        {
            Abyssinian,
            AmericanBobtail,
            AmericanBobtailShorthair,
            AmericanCurl,
            AmericanCurlLonghair,
            AmericanShorthair,
            AmericanWirehair,
            AustralianMist,
            Balinese,
            Bengal,
            BengalLonghair,
            Birman,
            Bombay,
            BritishLonghair,
            BritishShorthair,
            Burmese,
            Burmilla,
            BurmillaLonghair,
            Chartreux,
            Chausie,
            CornishRex,
            Cymric,
            DevonRex,
            Donskoy,
            EgyptianMau,
            ExoticShorthair,
            Havana,
            Himalayan,
            JapaneseBobtail,
            JapaneseBobtailLonghair,
            Khaomanee,
            Korat,
            KurilianBobtail,
            KurilianBobtailLonghair,
            LaPerm,
            LaPermShorthair,
            Lykoi,
            MaineCoon,
            Manx,
            MekongBobtail,
            Nebelung,
            NorwegianForestCat,
            Ocicat,
            OrientalLonghair,
            OrientalShorthair,
            Persian,
            Peterbald,
            Ragdoll,
            RussianBlue,
            Savannah,
            ScottishFold,
            ScottishStraight,
            Siamese,
            Siberian,
            Singapura,
            Snowshoe,
            Sokoke,
            Somali,
            Sphynx,
            Thai,
            Tonkinese,
            Toyger,
            TurkishAngora,
            TurkishVan,
            Unspecified
        }

        public enum AvailabilityStatus
        {
            Available,
            Adopted
        }

        public enum ApplicationStatus
        {
            Accepted,
            Rejected,
            Pending
        }
    }
}
