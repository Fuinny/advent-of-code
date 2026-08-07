if (!File.Exists("Input.txt"))
{
    Console.WriteLine("Error! No input file was found!");
    return;
}

string[] file = File.ReadAllLines("Input.txt");

int totalRows = file.Length;
int totalColumns = file[0].Length;  // Since in puzzle input all strings are the same length, this is fine.

char[,] diagram = new char[totalRows, totalColumns];

for (int row = 0; row < totalRows; row++)
{
    for (int col = 0; col < totalColumns; col++)
    {
        diagram[row, col] = file[row][col];
    }
}

(int Row, int Column)[] possibleDirections = [
    (-1, -1), (-1, 0), (-1, 1),
    (0, -1), (0, 1),
    (1, -1), (1, 0), (1, 1)
];

int firstRolls = 0;
int secondRolls = 0;

for (int row = 0; row < totalRows; row++)
{
    for (int col = 0; col < totalColumns; col++)
    {
        char currentCharacter = diagram[row, col];
        
        if (currentCharacter != '@')
            continue;

        int counter = 0;
        
        foreach (var direction in possibleDirections)
        {
            if (row + direction.Row < 0 || row + direction.Row >= totalRows)
                continue;
            
            if (col + direction.Column < 0 || col + direction.Column >= totalColumns)
                continue;

            if (diagram[row + direction.Row, col + direction.Column] == '@')
                counter++;
        }

        if (counter < 4)
            firstRolls++;
    }
}

bool isFound;

do
{
    isFound = false;
    
    for (int row = 0; row < totalRows; row++)
    {
        for (int col = 0; col < totalColumns; col++)
        {
            char currentCharacter = diagram[row, col];

            if (currentCharacter != '@')
                continue;

            int counter = 0;

            foreach (var direction in possibleDirections)
            {
                if (row + direction.Row < 0 || row + direction.Row >= totalRows)
                    continue;

                if (col + direction.Column < 0 || col + direction.Column >= totalColumns)
                    continue;

                if (diagram[row + direction.Row, col + direction.Column] == '@')
                    counter++;
            }

            if (counter < 4)
            {
                secondRolls++;
                diagram[row, col] = '.';    // This removes paper role ('@') and changes it to empty space ('.').
                isFound = true;
            }
        }
    }
} while (isFound);

Console.WriteLine($"Forklift can access {firstRolls} rolls of paper in part one.");
Console.WriteLine($"Forklift can access {secondRolls} rolls of paper in part two.");