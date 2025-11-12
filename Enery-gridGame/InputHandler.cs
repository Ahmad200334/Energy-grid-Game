public class InputHandler
{
    // تحويل مفتاح الكيبورد إلى اتجاه (Direction)
    public ConsoleKey ReadKey()
    {
        ConsoleKeyInfo keyInfo = Console.ReadKey(true); // true لمنع ظهور المفتاح في الكونسول
        return keyInfo.Key;
    }
}
