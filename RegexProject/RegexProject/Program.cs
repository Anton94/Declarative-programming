using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

/*
 * 
 * Name: Anton Dudov
 * FN:   71488
 * Task 15 from "Course task 1.pdf"
 * 
 * Prints all leafs from given XML file. Each printed leaf has attribute 'attr' 
 * and value to this 'attr' attribute- 'attrValue', also the "text" field of the leaf element has 'attrValue' as word.
*/

namespace RegexProject
{
    public class Program
    {
        // Prints all leafs from given XML file(as string).Each printed leaf has attribute @attr 
        // and value to this @attr attribute- @attrValue, also the "text" field of the leaf element has @attrValue as word.
        public static void solution(string text, string attr, string attrValue)
        {
            RegexOptions options = RegexOptions.Singleline;

            // \s*(\w+)\b                               -> Here matches the leaf name
            // [^>]*\s                                  -> some filler and finishes with \s . The filler must exclude '>' symbol!
            // attr + @"\s*=\s*""" + attrValue + @"""   -> here matches the attr = "value" (with as many spaces as it wants)
            // [^>]*>                                   -> some fillers (other attributes or so) and follows the closing '>' of the opening part of the element
            // Til now I have read <TAGNAME attr1="value1" and so on..> 
            // ([^<]*\b                                 -> fillers and beginning of a word and must exclude the starting symbol of the closing part of the element '<'
            // + attrValue + @"\b                       -> the attribute value as separate word in the "text" field of the leaf element
            // [^<]*)<                                  -> some filler to the end of the text part of the element and after that it's the closing part of the element '</TAGNAME>' (starting with '<' symbol)
            string regexPattern = @"<\s*(\w+)\b[^>]*\s" + attr + @"\s*=\s*""" + attrValue + @"""[^>]*>([^<]*\b" + attrValue + @"\b[^<]*)<";
            
            foreach (Match m in Regex.Matches(text, regexPattern, options))
            {
                Console.WriteLine("Leaf [{0}] has text [{1}]", m.Groups[1].Value, m.Groups[2].Value); // Prints the leaf name and the 'text' field of element
            }
        }

        public static void Main(string[] args)
        {
            string text = System.IO.File.ReadAllText(@"../../../test.xml");

            solution(text, "foo", "flas");
            // No matches tests
            solution(text, "foo", "abc");
            solution(text, "id", "00");
        }
    }
}
