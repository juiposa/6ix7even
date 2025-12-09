using _6ix7even.Agent.Actions;
using _6ix7even.Signature;

namespace _6ix7even.Agent;

public class ActionAgent
{
    private State state;
    private MarkEntity marker;

    public ActionAgent(State state)
    {
        this.state = state;
        marker = new MarkEntity(state);
    }

    public void DoSixSeven(ulong objectId)
    {
        SixSeven.Invoke(objectId, marker);
    }
}