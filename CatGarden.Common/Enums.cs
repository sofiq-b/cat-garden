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
            American_Curl,
            American_Shorthair,
            American_Wirehair,
            Australian_Mist,
            Balinese,
            Bengal,
            Birman,
            Bombay,
            British_Shorthair,
            Burmese,
            Burmilla,
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
            Khaomanee,
            Korat,
            Kurilian_Bobtail,
            LaPerm,
            Lykoi,
            Maine_Coon,
            Manx,
            Mekong_Bobtail,
            Nebelung,
            Norwegian_Forest_Cat,
            Ocicat,
            Oriental,
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

        public enum CoatLength
        {
            Hairless,
            Short,
            Medium,
            Long
        }

        public enum Color
        {
            Black,
            Tuxedo,
            Brown,
            White,
            Calico,
            Cream,
            Gray,
            Orange,
            Tabby,
            Tortoiseshell
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
