
using MongoDB.Bson.Serialization.Attributes;

[BsonKnownTypes(
    typeof(Wall),
    typeof(MovingElement),
    typeof(Player),
    typeof(Rat),
    typeof(Snake),
    typeof(Spider),
    typeof(Item),
    typeof(Dagger),
    typeof(Potion),
    typeof(Shield),
    typeof(Torch)
)] // https://www.mongodb.com/docs/drivers/csharp/current/fundamentals/serialization/polymorphic-objects/
public abstract class LevelElement
{
    public bool IsVisable { get; set; }
    public Position Position { get; set; }
    public char Icon { get; protected set; }
    public ConsoleColor Color { get; protected set; }
    public void Draw(bool drawIcon = true)
    {
        Position.SetCursor();
        if (this is Wall) Console.BackgroundColor = ConsoleColor.DarkGray;
        else Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = Color;
        Console.Write((drawIcon) ? Icon : ' ');
        IsVisable = drawIcon;
        Console.ResetColor();
    }
    public void Hide() => Draw(false);
}