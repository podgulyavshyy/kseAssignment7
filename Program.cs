using System.Diagnostics;
using wordsChecker;

string text;
Console.Write("Enter sentence: ");
text = Console.ReadLine();
var tokens = new Tokenizer().Tokenize(text);
var smorc = new List<char>{'!','"','#','$','%','&','(',')','*','+',',','.','/',':',','};

for (var t = 0; t < tokens.Count; t++)
{
    if (smorc.Contains(tokens[t]))
    {
        tokens[t] = ' ';
    }
}

var newText = tokens.Aggregate("", (current, c) => current + c.ToString());

var punctuation = newText.Where(Char.IsPunctuation).Distinct().ToArray();

var words = newText.Split().Select(x => x.Trim(punctuation));
var wordsFromFile = File.ReadLines(@"words_list.txt").ToHashSet();

Console.Write("Looks like you have typos in next words: ");

var sw = new Stopwatch();
sw.Start(); 
//00:00:00.0003336  //hashSet
//00:00:00.1104663  //std

// if pudge were you i would eat you
foreach(var item in words)
{
    if (item is " " or "")
    {
        continue;
    }
    
    if(!wordsFromFile.Contains(item))
    {
        Console.Write("(" + item + ")");
    }
}


sw.Stop();
Console.WriteLine($"Elapsed time: {sw.Elapsed}");
