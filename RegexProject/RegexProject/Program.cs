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
        //and value to this @attr attribute- @attrValue, also the "text" field of the leaf element has @attrValue as word.
        public static void solution(string text, string attr, string attrValue)
        {
            // \s*(\w+)\b                               -> Here matches the leaf name
            // .*\s                                     -> some filler and finishes with \s
            // attr + @"\s*=\s*""" + attrValue + @"""   -> here matches the attr = "value" (with as many spaces as it wants)
            // .*>                                      -> some fillers (other attributes or so)
            // (.*\b                                    -> fillers and beginning of a word
            // + attrValue + @"\b                       -> the attribute value as seperate word in the "text" field of the leaf element
            string regexPattern = @"\s*(\w+)\b.*\s" + attr + @"\s*=\s*""" + attrValue + @""".*>(.*\b" + attrValue + @"\b.*)<";
          //  string regexPattern = @"<\s*(\w+)[\s\b].*>";

            foreach (Match m in Regex.Matches(text, regexPattern))
            {
                Console.WriteLine("Leaf [{0}] has text [{1}]", m.Groups[1].Value, m.Groups[2].Value); // Prints the leaf name and the 'text' field of element
              //  Console.WriteLine("{0} {1}", m.Groups[1].Value, m.Groups[2].Value);
            }
        }

        public static void Main(string[] args)
        {
            string text = System.IO.File.ReadAllText(@"../../../test.xml");

            //Console.WriteLine(text);

            solution(text, "foo", "flas");
        }
    }
}
