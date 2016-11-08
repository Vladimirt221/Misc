using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Creating_Regions
{
    public struct Region_group {
        public String start;
        public String end;
    }

    public struct Region
    {
        public string name;
        public List<Region_group> groups;
    }

    public struct Region_data
    {
        public string name;
        public List<List<String>> zip_codes;
    }

    class Program
    {
        static Region_data getNextData()
        {
            return new Region_data()
            {
                //    zip "    06108" is not valid
                name = "Region1",
                zip_codes = new List<List<String>> { new List<String> { "   06108", "06109", "06110" },
                new List<String> { "06115" } }
            };
        }
        static bool isValidZip(string s)
        {
            Match match=Regex.Match(s, @"^(\d){5}$");
            if (match.Success) return true;
            return false;
        }
        static Region newRegion()
        {
            Region_data data = getNextData();
            Region result;
            result.name = data.name;
            result.groups = new List<Region_group>();
            Region_group group = new Region_group();
            for (int i=0;i<data.zip_codes.Count;i++)
            {
                group.start = "EMPTY";
                group.end = "     ";
                
                //int k = 0; string 
                for (int j=0;j<data.zip_codes[i].Count;j++)
                {
                    if (!isValidZip(data.zip_codes[i][j]))
                    {
                        System.Console.WriteLine("Invalid zip code: \""+ data.zip_codes[i][j]+"\"");
                        continue;
                    }
                    if (String.Compare(group.start, data.zip_codes[i][j])>0) group.start = data.zip_codes[i][j];
                    if (String.Compare(group.end, data.zip_codes[i][j]) < 0) group.end = data.zip_codes[i][j];
                }
                result.groups.Insert(i, group);
            }
            return result;
        }

        static void Main(string[] args)
        {
            Region region=newRegion();
            System.Console.WriteLine("Name: " + region.name);
            for (int i = 0; i < region.groups.Count; i++)
            {
                System.Console.Write("Group #");
                System.Console.Write(i + 1);
                System.Console.WriteLine(": Start: " + region.groups[i].start + " End: " + region.groups[i].end);
            }
        }
    }
}
