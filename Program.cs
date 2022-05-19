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

var punctuation = newText.Where(char.IsPunctuation).Distinct().ToArray();

var words = newText.Split().Select(x => x.Trim(punctuation));
var wordsFromFile = File.ReadLines(@"words_list.txt").ToHashSet();

Console.Write("Looks like you have typos in next words: ");

// if Pudge werse you i woueld eat you
var mistakes = new List<string>();
foreach(var item in words)
{
 if (item is " " or "")
 {
  continue;
 }
    
 if(!wordsFromFile.Contains(item))
 {
  Console.Write("(" + item + ")");
  mistakes.Add(item);
 }
}

const int len = 2;

foreach (var mistake in mistakes)
{
 Console.WriteLine("");
 var optimal = new PriorityQueue<string, int>();
 foreach (var word in wordsFromFile)
 {
  var i = lanOfLox(mistake,word);
  if(i < len)
  {
   optimal.Enqueue(word,i);
  }
 }
 while (optimal.Count > 0)
 {
  Console.Write(optimal.Dequeue() + " ");
 }
}

int lanOfLox(string currentMistake, string currentWord)
{
 var matrix = new int[currentMistake.Length + 1, currentWord.Length + 1];
 
 for (var column = 0; column <= currentWord.Length; column++)
 {
  matrix[0, column] = column;
 }
 for (var row = 0; row <= currentMistake.Length; row++)
 {
  matrix[row, 0] = row;
 }

 for (var i = 1; i <= currentMistake.Length; i++)
 {
  for (var j = 1; j <= currentWord.Length; j++)
  {
   matrix[i, j] = FillPoint(i, j, currentMistake, currentWord, matrix);
  }
 }

 return matrix[currentMistake.Length, currentWord.Length];
}

int FillPoint(int i, int j, string iString, string jString, int[,] matrix)
{
 if (i == 0 & j == 0)
 {
  return 0;
 }
 if (i == 0 & j > 0)
 {
  return j;
 }

 if (j == 0 & i > 0)
 {
  return i;
 }

 var temp = 1;
 if (iString[i - 1] == jString[j - 1])
 {
  temp = 0;
 }

 var left = matrix[i, j - 1] + 1;
 var leftTop = matrix[i - 1, j - 1] + temp;
 var top = matrix[i - 1, j] + 1;
 var ansList = new List<int>{left,top,leftTop};
 return ansList.Min();
}