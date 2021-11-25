using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worms_lab.Services
{
    public interface IFoodGenerator
    {
        Food Generate(ReadOnlyState state);
    }
}
