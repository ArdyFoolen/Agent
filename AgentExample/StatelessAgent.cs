using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace AgentExample
{
    public class StatelessAgent<Msg> : IAgent<Msg>
    {
        private readonly ActionBlock<Msg> actionBlock;

        public StatelessAgent(Action<Msg> process)
        {
            actionBlock = new ActionBlock<Msg>(msg =>
            {
                process(msg);
            });
        }

        public void Tell(Msg message)
            => actionBlock.Post(message);
    }
}
