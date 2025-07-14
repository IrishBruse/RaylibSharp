namespace RaylibSharp.Generator;

class Lines(string[] Lines)
{
    int index = 0;

    public string? NextLine()
    {
        if (index >= Lines.Length)
        {
            return null;
        }

        string line = CurrentLine;
        index++;
        return line;
    }

    public void SkipEmpty()
    {
        do
        {
            if (CurrentLine.Trim() == "")
            {
                NextLine();
            }
        }
        while (CurrentLine.Trim() == "");
    }

    public bool HasNext()
    {
        return index < Lines.Length;
    }

    public string? CurrentLine
    {
        get
        {
            if (index >= Lines.Length)
            {
                return null;
            }

            return Lines[index];
        }
    }
}
