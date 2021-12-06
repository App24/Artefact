namespace Artefact.Items
{
    internal interface IUsable
    {
        bool IsUsable { get; }
        bool OnUse();
    }
}
