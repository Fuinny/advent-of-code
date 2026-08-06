if (!File.Exists("Input.txt"))
{
    Console.WriteLine("Error! No input file was found!");
    return;
}

string[] banks = File.ReadAllLines("Input.txt");

Console.WriteLine($"Total output joltage for part one is: {CalculateMaxJoltage(banks, 2)}");
Console.WriteLine($"Total output joltage for part two is: {CalculateMaxJoltage(banks, 12)}");

ulong CalculateMaxJoltage(string[] lines, int numberOfDigits)
{
    ulong finalJoltage = 0;

    foreach (string line in lines) 
    {
        int lastDigitFoundAt = -1;
        int[] digits = new int[numberOfDigits];

        for (int i = 0; i < numberOfDigits; i++)
        {
            (int Index, int Value) digit = (0, 0);

            /*
             * Set j to one space after the index where the last max digit was found.
             * (numberOfDigits - 1) + i is used to ensure that there always be enough place to create
             * a number of the length that is specified by the numberOfDigits variable.
             */
            for (int j = lastDigitFoundAt + 1; j < line.Length - (numberOfDigits - 1) + i; j++)
            {
                /*
                 * Convert char to int by substracting character representation of digits.
                 * E.g. if line[j] is '5' then '5' - '0' will result into 53 - 48 which is now 5 integer.
                 * This works because in ASCII table characters that represet digits are in contiuous block,
                 * so essetially it calculates the distances between '0' and given character.
                 */
                int currentDigit = line[j] - '0';

                if (currentDigit > digit.Value)
                    digit = (j, currentDigit);
            }

            lastDigitFoundAt = digit.Index;
            digits[i] = (digit.Value);
        }

        string numberString = string.Join("", digits);
        finalJoltage += ulong.Parse(numberString);
    }

    return finalJoltage;
}
