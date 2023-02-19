using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ReportCreator {
    public class Report
    {
        private List<Entry> entries = new List<Entry>();

        public void AddEntry(string currentDialogue, string choice, float time)
        {
            entries.Add(new Entry { CurrentDialogue = currentDialogue, Choice = choice, Time = time });
        }

        public void Save(string fileName)
        {
            string path = Application.dataPath + "/Reports/" + fileName;
            string csv = FormatCsv(entries);
            File.WriteAllText(path, csv, Encoding.UTF8);
        }

        private string FormatCsv(List<Entry> entries)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Current Dialogue,Choice,Time In Seconds");
            for (int i = 0; i < entries.Count; i++)
            {
                sb.AppendFormat("{0},{1},{2:F2}", EscapeString(entries[i].CurrentDialogue), EscapeString(entries[i].Choice), entries[i].Time);
                sb.AppendLine();
            }
            return sb.ToString();
        }

        private string EscapeString(string input)
        {
            // If the input string contains commas, enclose it in double quotes
            if (input.Contains(","))
            {
                return "\"" + input.Replace("\"", "\"\"") + "\"";
            }
            else
            {
                return input;
            }
        }


        private class Entry
        {
            public string CurrentDialogue { get; set; }
            public string Choice { get; set; }
            public float Time { get; set; }
        }
    }
}

