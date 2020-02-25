using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentExample
{
    public interface IAgent<Msg>
    {
        void Tell(Msg message);
    }
}
