using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worms_lab.Simulator.Services
{
    public class NameGeneratorService
    {
        private int nameGenerated = 0;
        private static readonly string[] prefix = new string[] {
            "Ael", "Aer", "Af", "Ah", "Al", "Am", "Ama", "An", "Ang", "Ansr", "Ar", 
            "Arn", "Aza", "Bael", "Bes", "Cael", "Cal", "Cas", "Cla", "Cor",
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
            "Ae", "Ael", "Aer", "Aera", "Aias", "Aia", "Ah", "Aha", "Aith", "Aira", "Al",
            "Ala",  "Ali", "Am", "Ama", "An", "Ana",  "Ar", "Ara",  "Ari",  "Aro",  "As",
            "Avel", "Brar",  "Dar",  "Deth", "Dre","Drim",  "Dul", "Ean", "El",  "Ela","Emar",
            "En", "Er",  "Ess",  "Evar", "Fel", "Hal",  "Har",  "Hel",  "Ian", "Ianna",
            "Iat", "Ik", "Il",  "Im", "In",  "Ir",  "Is", "Ith",  "Kash",  "Ki", "Lan",
            "Lanna",  "Lam",  "Lar",  "Las", "Lian", "Lia", "Lis",  "Lon",  "Lyn", "Mah",
            "Ma",  "Mil",  "Mus", "Nal",  "Nes", "Nin",  "Nis",  "On","Onna","Or", "Oth",
            "Que", "Quis","Rah", "Rad", "Rail","Ria",  "Ran",  "Reth",  "Ro",  "Ruil",  "Sal",
            "San", "Sar", "Sel",  "Sha",  "Spar", "Tae",  "Tas",  "Ten",  "Thal", "Tha",
            "Thar",  "Ther",  "Thi", "Thus", "Thas",  "Ti",  "Tril", "Tria",  "Ual",  "Uath",
            "Us", "Ua", "Van", "Vanna", "Var", "Vara",  "Vain",  "Via",  "Vin",  "Wyn", "Ya",
            "Yr", "Yn", "Yth", "Zair", "Zara"
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
