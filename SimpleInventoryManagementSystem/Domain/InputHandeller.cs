namespace SimpleInventoryManagementSystem.Domain;

public class InputHandeller
{
    public static int ExitCond()
    {
        Console.Write("To exit type e or E => ");
        var e = Console.ReadLine() ?? string.Empty;
        if (e.ToLower().Trim().Equals("e")) return -1;

        return 0;
    }

    public static float ReadNumber(string message, Number numberType)
    {
        float number;
        do
        {
            var input = ReadLineInput(message);

            if (numberType == Number.Float)
            {
                if (float.TryParse(input, out number) && number > 0)
                    break;
            }
            else
            {
                if (int.TryParse(input, out var intNumber) && intNumber > 0)
                {
                    number = intNumber;
                    break;
                }
            }

            Console.WriteLine("Invalid input! It must be a number greater than 0. TRY AGAIN");
        } while (true);

        return number;
    }

    public static string ReadString(string message)
    {
        string input;
        do
        {
            input = ReadLineInput(message);
        } while (input.Length <= 0);

        return input;
    }

    public static string ReadLineInput(string message)
    {
        Console.Write(message);
        var input = Console.ReadLine() ?? string.Empty;

        Console.WriteLine();

        if (input.Length <= 0)
        {
            Console.WriteLine("Empty field! TRY AGAIN");
            if (ExitCond() == -1)
                Utilities.Menu();
        }

        return input;
    }
}