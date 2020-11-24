namespace Scenes.Root
{
    public interface IInteractable
    {
        string InteractableName { get; }
        void SwitchInteractable(bool isInteractable);
        void Interact();
        
    }
}