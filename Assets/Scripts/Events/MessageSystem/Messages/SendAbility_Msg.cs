using Ability;

namespace Events.MessageSystem.Messages
{
    public struct SendAbility_Msg : IMessage
    {
        public IAbility ability;
        public SendAbility_Msg(IAbility ability)
        {
            this.ability = ability;
        }
    }
}