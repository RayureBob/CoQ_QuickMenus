using XRL;
using XRL.World;
using XRL.World.Parts;

[PlayerMutator]
public class Mutator_QuickMenus : IPlayerMutator
{
    public void mutate(GameObject player)
    {
        player.AddPart<XRL.World.Parts.QuickMenus>();
    }
}