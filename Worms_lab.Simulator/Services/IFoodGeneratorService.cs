using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worms_lab.Simulator.Services
{
    public interface IFoodGeneratorService
    {
        Food Generate(ReadOnlyState state);

    }
}
