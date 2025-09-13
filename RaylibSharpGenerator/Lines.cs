namespace RaylibSharp.Generator;

internal sealed class Lines(string[] Lines)
{
    private int index;

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
                _ = NextLine();
            }
        }
        while (CurrentLine.Trim() == "");
    }

    public bool HasNext()
    {
        return index < Lines.Length;
    }

    public bool Until(string v)
    {
        if (CurrentLine.Trim() == v)
        {
            return false;
        }

        return true;
    }

    public string CurrentLine
    {
        get
        {
            return Lines[index]!;
        }
    }

    public void Undo()
    {
        if (index > 0)
        {
            index--;
        }
    }
}
