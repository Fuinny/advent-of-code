if (!File.Exists("Input.txt"))
{
    Console.WriteLine("Error! No input file was found!");
    return;
}

List<(char Direction, int NumberOfRotations)> rotations = [];

foreach (string line in File.ReadLines("Input.txt"))
{
    /*
     * First character is always either R (right) or L (left).
     * The rest of the string is number of rotations.
     */
    char direction = line[0];
    int numberOfRotations = int.Parse(line.Substring(1));
    
    rotations.Add((direction, numberOfRotations));
}

int dialPosition = 50, firstCounter = 0, secondCounter = 0;

foreach (var rotation in rotations)
{
    bool isDirectionLeft = rotation.Direction == 'L';

    for (int i = 0; i < rotation.NumberOfRotations; i++)
    {
        dialPosition = isDirectionLeft ? dialPosition - 1 : dialPosition + 1;

        // If dial moved past 0, set it to 99.
        if (dialPosition == -1)
            dialPosition = 99;

        // If dial moved past 99, set it to 0.
        if (dialPosition == 100)
            dialPosition = 0;

        // For part 2 count how many times dial pointed to 0 during any rotation.
        if (dialPosition == 0)
            secondCounter++;
    }

    // For part 1 count how many times dial pointed to 0 after any rotation.
    if (dialPosition == 0)
        firstCounter++;
}

Console.WriteLine($"Password for part one is: {firstCounter}");
Console.WriteLine($"Password for part two is: {secondCounter}");