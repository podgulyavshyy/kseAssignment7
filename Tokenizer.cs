namespace wordsChecker;

public class Tokenizer
{
    public List<char> Tokenize(string expression)
    {
        {
            var answearShit = new List<char>();
            for (var index = 0; index != expression.Length; index++)
            {
                var element = expression[index];
                answearShit.Add(element);
            }

            return answearShit;
        }
    }
}