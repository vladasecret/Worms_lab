using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worms_lab.services
{
    class NameGenerator
    {
        private int nameGenerated = 0;
        private static string[] prefix = new string[] {
            "Ael", "Aer", "Af", "Ah", "Al", "Am", "Ama", "An", "Ang", "Ansr", "Ar", 
            "Ar", "Arn", "Aza", "Bael", "Bes", "Cael", "Cal", "Cas", "Cla", "Cor",
            "Cy", "Dae", "Dho", "Dre", "Du", "Eil", "Eir", "El", "Er", "Ev", "Fera",
            "Fi", "Fir", "Fis", "Gael", "Gar", "Gil", "Ha", "Hu", "Ia", "Il", "Ja",
            "Jar", "Ka", "Kan", "Ker", "Keth", "Koeh", "Kor", "Ky", "La", "Laf", "Lam",
            "Lue", "Ly", "Mai", "Mal", "Mara", "My", "Na", "Nai", "Nim", "Nu", "Ny",
            "Py", "Raer", "Re", "Ren", "Rhy", "Ru", "Rua", "Rum", "Rid", "Sae", "Seh",
            "Sel", "Sha", "She", "Si", "Sim", "Sol", "Sum", "Syl" , "Ta", "Tahl", "Tha",
            "Tho", "Ther", "Thro", "Tia", "Tra", "Ty", "Uth", "Ver", "Vil", "Von", "Ya", "Za", "Zy"
                    };
        private static readonly string[] suffix = new string[]
        {
            "ae", "ael", "aer", "aera", "aias", "aia", "ah", "aha", "aith", "aira", "al",
            "ala",  "ali", "am", "ama", "an", "ana",  "ar", "ara",  "ari",  "aro",  "as",
            "avel", "brar",  "dar",  "deth", "dre","drim",  "dul", "ean", "el",  "ela","emar",
            "en", "er",  "ess",  "evar", "fel", "hal",  "har",  "hel",  "ian", "ianna",
            "iat", "ik", "il",  "im", "in",  "ir",  "is", "ith",  "kash",  "ki", "lan",
            "lanna",  "lam",  "lar",  "las", "lian", "lia", "lis",  "lon",  "lyn", "mah",
            "ma",  "mil",  "mus", "nal",  "nes", "nin",  "nis",  "on","onna","or", "oth",
            "que", "quis","rah", "rad", "rail","ria",  "ran",  "reth",  "ro",  "ruil",  "sal",
            "san", "sar", "sel",  "sha",  "spar", "tae",  "tas",  "ten",  "thal", "tha",
            "thar",  "ther",  "thi", "thus", "thas",  "ti",  "tril", "tria",  "ual",  "uath",
            "us", "ua", "van", "vanna", "var", "vara",  "vain",  "via",  "vin",  "wyn", "ya",
            "yr", "yn", "yth", "zair", "zara"
        };

        public string GenerateName()
        {
            int prefixIdx = nameGenerated % prefix.Length;
            int suffixIdx = nameGenerated / prefix.Length % suffix.Length;
            int circle = nameGenerated / (prefix.Length * suffix.Length);
            string name = $"{prefix[prefixIdx]}{suffix[suffixIdx]}{(circle > 0 ? circle : null)}";
            nameGenerated++;
            return name;
        }
    }
}
