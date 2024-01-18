using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

public class NameConsecutiveNumberEditor : Editor
{
    [MenuItem("Edit/NameConsecutiveNumber", false)]
    static void SetNames()
    {
        Undo.RecordObjects(Selection.objects, "ApplyNameConsecutiveNumber");

        string baseName = string.Empty;
        foreach (var item in Selection.objects.Select((value, index) => new { value, index }))
        {
            if (item.index == 0)
            {
                var fullName = item.value.name;

                baseName = fullName;

                // ññîˆÇÃ(n)ÇçÌèú.
                Regex regex = new Regex(@"\(.+?\)");
                var matches = regex.Matches(fullName);
                if (matches != null)
                {
                    var matchList = new List<string>();
                    foreach (var match in matches)
                    {
                        matchList.Add(match.ToString());
                    }

                    if (matchList.Any())
                    {
                        var lastMatch = matchList.Last();
                        int lastIndex = fullName.LastIndexOf(lastMatch);
                        baseName = fullName.Remove(lastIndex);
                    }
                }

                // ññîˆÇÃãÛîíÇçÌèú.
                for (int i = baseName.Length - 1; i >= 0; i--)
                {
                    var c = baseName[i];
                    if (!char.IsWhiteSpace(c))
                    {
                        if (i != baseName.Length - 1)
                        {
                            baseName = baseName.Remove(i + 1);
                        }
                        break;
                    }
                }

                item.value.name = baseName;
            }
            else
            {
                // ñºëOÇå`ê¨.
                item.value.name = string.Format("{0} _{1}", baseName, item.index);
            }
        }
    }
}

