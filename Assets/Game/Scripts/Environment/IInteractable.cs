namespace Game.Scripts.Environment
{
    public interface IInteractable
    {
        public int InteractionCount { get; set; }
        public bool CanInteract { get; set; }
        void Interact();
    }
}
