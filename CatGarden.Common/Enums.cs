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
            American_Bobtail,
            American_Bobtail_Shorthair,
            American_Curl,
            American_Curl_Longhair,
            American_Shorthair,
            American_Wirehair,
            Australian_Mist,
            Balinese,
            Bengal,
            Bengal_Longhair,
            Birman,
            Bombay,
            British_Longhair,
            British_Shorthair,
            Burmese,
            Burmilla,
            Burmilla_Longhair,
            Chartreux,
            Chausie,
            Cornish_Rex,
            Cymric,
            Devon_Rex,
            Donskoy,
            Egyptian_Mau,
            Exotic_Shorthair,
            Havana,
            Himalayan,
            Japanese_Bobtail,
            Japanese_Bobtail_Longhair,
            Khaomanee,
            Korat,
            Kurilian_Bobtail,
            Kurilian_Bobtail_Longhair,
            LaPerm,
            LaPerm_Shorthair,
            Lykoi,
            Maine_Coon,
            Manx,
            Mekong_Bobtail,
            Nebelung,
            Norwegian_Forest_Cat,
            Ocicat,
            Oriental_Longhair,
            Oriental_Shorthair,
            Persian,
            Peterbald,
            Ragdoll,
            Russian_Blue,
            Savannah,
            Scottish_Fold,
            Scottish_Straight,
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
            Turkish_Angora,
            Turkish_Van,
            Unspecified
        }

        public enum Gender
        {
            Male,
            Female,
            Unknown 
        }

        public enum CoatColor
        {
            Male,
            Female,
            Unknown
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

        public enum City
        {
            Blagoevgrad,
            Burgas,
            Dobrich,
            Gabrovo,
            Haskovo,
            Kardzhali,
            Kystendil,
            Lovech,
            Montana,
            Pazardzhik,
            Pernik,
            Pleven,
            Plovdiv,
            Razgrad,
            Ruse,
            Shumen,
            Silistra,
            Sliven,
            Smolyan,
            Sofia,
            Sofia_grad,
            Stara_Zagora,
            Targovishte,
            Varna,
            Veliko_Tarnovo,
            Vidin,
            Vratsa,
            Yambol
        }
    }
}
