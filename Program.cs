string text;
Console.Write("Enter sentence: ");
text = Console.ReadLine();

var punctuation = text.Where(Char.IsPunctuation).Distinct().ToArray();
var words = text.Split().Select(x => x.Trim(punctuation));

List<string> wordsFromFile = new List<string>();
foreach(string line in File.ReadLines(@"../../../words_list.txt"))
{
    wordsFromFile.Add(line);
}

Console.Write("Looks like you have typos in next words: ");
foreach(var item in words)
{
    if(!wordsFromFile.Contains(item))
    {
        
        Console.Write(" " + item);
    }
}
