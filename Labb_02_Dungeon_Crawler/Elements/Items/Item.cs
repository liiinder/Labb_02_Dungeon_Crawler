abstract class Item : LevelElement
{
    public bool Looted { get; set; }
    public abstract string PickUp();
}