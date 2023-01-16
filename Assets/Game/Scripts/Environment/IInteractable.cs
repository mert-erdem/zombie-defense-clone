namespace Game.Scripts.Environment
{
    public interface IInteractable
    {
        public bool CanInteract { get; set; }
        void Interact();
    }
}
