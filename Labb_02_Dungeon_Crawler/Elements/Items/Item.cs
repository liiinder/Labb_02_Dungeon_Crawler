public abstract class Item : LevelElement
{
    public bool Looted { get; set; }
    public abstract void PickUp(LevelData level);
}