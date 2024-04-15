using System.ComponentModel.DataAnnotations;

namespace CatGarden.Common
{
    public class Enums
    {
        

        public enum Breed
        {
            Abyssinian,
            [Display(Name = "American Bobtail")]
            American_Bobtail,
            [Display(Name = "American Curl")]
            American_Curl,
            [Display(Name = "American Shorthair")]
            American_Shorthair,
            [Display(Name = "American Wirehair")]
            American_Wirehair,
            [Display(Name = "American Mist")]
            Australian_Mist,
            Balinese,
            Bengal,
            Birman,
            Bombay,
            [Display(Name = "British Shorthair")]
            British_Shorthair,
            Burmese,
            Burmilla,
            Chartreux,
            Chausie,
            [Display(Name = "Cornish Rex")]
            Cornish_Rex,
            Cymric,
            [Display(Name = "Devon Rex")]
            Devon_Rex,
            Donskoy,
            [Display(Name = "Egyptian Mau")]
            Egyptian_Mau,
            [Display(Name = "Exotic Shorthair")]
            Exotic_Shorthair,
            Havana,
            Himalayan,
            [Display(Name = "Japanese Bobtail")]
            Japanese_Bobtail,
            Khaomanee,
            Korat,
            [Display(Name = "Kurilian Bobtail")]
            Kurilian_Bobtail,
            LaPerm,
            Lykoi,
            [Display(Name = "Maine Coon")]
            Maine_Coon,
            Manx,
            [Display(Name = "Mekong Bobtail")]
            Mekong_Bobtail,
            Nebelung,
            [Display(Name = "Norwegian Forest Cat")]
            Norwegian_Forest_Cat,
            Ocicat,
            Oriental,
            Persian,
            Peterbald,
            Ragdoll,
            [Display(Name = "Russian Blue")]
            Russian_Blue,
            Savannah,
            [Display(Name = "Scottish Fold")]
            Scottish_Fold,
            [Display(Name = "Scottish Straight")]
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
            [Display(Name = "Turkish Angora")]
            Turkish_Angora,
            [Display(Name = "Turkish Van")]
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
            [Display(Name = "Sofia-grad")]
            Sofia_grad,
            [Display(Name = "Stara Zagora")]
            Stara_Zagora,
            Targovishte,
            Varna,
            [Display(Name = "Veliko Tarnovo")]
            Veliko_Tarnovo,
            Vidin,
            Vratsa,
            Yambol
        }
    }
}
