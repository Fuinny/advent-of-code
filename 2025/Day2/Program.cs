if (!File.Exists("Input.txt"))
{
    Console.WriteLine("Error! No input file was found!");
    return;
}

string[] idRanges = File.ReadAllText("Input.txt").Split(',');

ulong firstInvalidIdSum = 0;
ulong secondInvalidIdSum = 0;

foreach (string idRange in idRanges)
{
    string[] ids = idRange.Split('-');

    ulong lower = ulong.Parse(ids[0]);
    ulong upper = ulong.Parse(ids[1]);

    for (ulong i = lower; i <= upper; i++)
    {
        string currentId = i.ToString();

        // In part 1 pattern can be only in IDs with even length, skip IDs with odd lenght.
        if (currentId.Length % 2 != 0)
            continue;
        
        bool isPatternFound = true;

        for (int k = 0; k < currentId.Length / 2; k++)
        {
             /*
              * Compare digit k in current ID with digit in the middle with offset of k.
              * For example in number 1212 compare 1 with 1, then 2 with 2.
              * Or in number 2136 compare 2 with 3, then break the loop.
              */
             if (currentId[k] != currentId[currentId.Length / 2 + k])
             {
                 isPatternFound = false;
                 break;
             } 
        }

        if (isPatternFound)
            firstInvalidIdSum += i;
    }

    for (ulong i = lower; i <= upper; i++)
    {
        string currentId = i.ToString();
        
        for (int k = 1; k <= currentId.Length / 2; k++) 
        {
            string currentSequence = currentId.Substring(0, k);

            // In part 2 ID can contain a pattern only when given sequence fully fits into ID.
            if (currentId.Length % currentSequence.Length != 0)
                continue;

            string possiblePattern = "";
            
            // A horrible, inefficient loop.
            for (int j = 0; j < currentId.Length / currentSequence.Length; j++)
                possiblePattern += currentSequence;

            if (currentId == possiblePattern)
            {
                secondInvalidIdSum += i;
                break;
            }
        }
    }
}

Console.WriteLine($"Sum of invalid IDs for part one is: {firstInvalidIdSum}");
Console.WriteLine($"Sum of invalid IDs for part two is: {secondInvalidIdSum}");