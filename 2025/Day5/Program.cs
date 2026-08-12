if (!File.Exists("Input.txt"))
{
    Console.WriteLine("Error! No input file was found!");
    return;
}

bool isDatabaseComplete = false;
List<(ulong lower, ulong upper)> database = [];
List<(ulong lower, ulong upper)> compactedDatabase = [];

ulong freshCount = 0, idCount = 0;

foreach (string line in File.ReadAllLines("Input.txt"))
{
    if (string.IsNullOrEmpty(line))
    {
        isDatabaseComplete = true;
        continue;
    }
    
    if (!isDatabaseComplete)
    {
        string[] limits = line.Split('-');
        ulong lower = ulong.Parse(limits[0]);
        ulong upper = ulong.Parse(limits[1]);
       
        database.Add((lower, upper));
    }
    else
    {
        ulong currentIngredient = ulong.Parse(line);

        foreach (var id in database)
        {
            if (currentIngredient >= id.lower && currentIngredient <= id.upper)
            {
                freshCount++;
                break;
            }
        }
    }
}

// Sort database first to eliminate possibility of extending to the left (lower bound).
database.Sort();

foreach (var range in database)
{
    if (compactedDatabase.Count == 0)
    {
        compactedDatabase.Add(range);
    }
    else
    {
        // Since the list is already sorted by lower bound, only last range check in compacted DB is needed.
        var (lastLower, lastUpper) = compactedDatabase[^1];

        /*
         * If lower bound of current range falls into last range in compacted DB, overlap with last range found.
         * Again, since the list is already sorted, lastUpper cannot be smaller than range.lower. In other words,
         * only extend to the right (upper bound) is possible. +1 is added so ranges like 3-5 and 6-10 would merge
         * into 3-10, otherwise they would stay separate.
         */
        if (range.lower <= lastUpper + 1) 
        {
            // Extend the upper bound only if last known upper bound is smaller than the current one.
            compactedDatabase[^1] = (lastLower, Math.Max(lastUpper, range.upper));
        }
        else
        {
            // If check failed, this is disjoint range.
            compactedDatabase.Add(range);
        }
    }
}

// +1 is added to the sum since ranges are inclusive, so range 3-5 covers 3 IDs.
foreach (var range in compactedDatabase)
    idCount += range.upper - range.lower + 1;

Console.WriteLine($"There're {freshCount} fresh ingredients in part one.");
Console.WriteLine($"There're {idCount} IDs considered to be fresh in part two.");