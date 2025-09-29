using Microsoft.AspNetCore.Mvc;
using JokeAPI.Models;

namespace JokeAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JokesController : ControllerBase
    {
        private static readonly List<Joke> _jokes = new List<Joke>
        {
            new Joke { Id = 1, Text = "Warum nehmen Geister keine Drogen? Weil sie schon high genug sind!", Category = "Wortspiel" },
            new Joke { Id = 2, Text = "Was ist grün und klopft an der Tür? Ein Klopfsalat!", Category = "Kalauer" },
            new Joke { Id = 3, Text = "Warum können Geister so schlecht lügen? Weil man durch sie hindurchsehen kann!", Category = "Wortspiel" },
            new Joke { Id = 4, Text = "Was ist weiß und stört beim Essen? Eine Lawine!", Category = "Absurd" },
            new Joke { Id = 5, Text = "Wie nennt man einen Bumerang, der nicht zurückkommt? Stock!", Category = "Kalauer" },
            new Joke { Id = 6, Text = "Was ist rot und schlecht für die Zähne? Ein Ziegelstein!", Category = "Anti-Witz" },
            new Joke { Id = 7, Text = "Warum sind Fische so schlecht in der Schule? Weil sie unter dem Meeresspiegel leben!", Category = "Wortspiel" },
            new Joke { Id = 8, Text = "Was sagt ein großer Stift zum kleinen Stift? Wachs-mal-stift!", Category = "Wortwitz" },
            new Joke { Id = 9, Text = "Warum gehen Ameisen nicht in die Kirche? Weil sie in-sekten sind!", Category = "Wortspiel" },
            new Joke { Id = 10, Text = "Was ist gelb und kann nicht schwimmen? Ein Bagger!", Category = "Anti-Witz" }
        };

        [HttpGet]
        public ActionResult<Joke> GetRandomJoke()
        {
            var random = new Random();
            var randomJoke = _jokes[random.Next(_jokes.Count)];
            return Ok(randomJoke);
        }

        [HttpGet("export/tsv")]
        public IActionResult ExportToTsv()
        {
            var tsvContent = GenerateTsvContent();
            var fileName = $"jokes_export_{DateTime.Now:yyyyMMdd_HHmmss}.tsv";
            
            return File(
                System.Text.Encoding.UTF8.GetBytes(tsvContent),
                "text/tab-separated-values",
                fileName
            );
        }

        private string GenerateTsvContent()
        {
            var lines = new List<string>();
            
            // Add header
            lines.Add("Id\tText\tCategory");
            
            // Add data rows
            foreach (var joke in _jokes)
            {
                // Escape any tab characters in the text to prevent format issues
                var escapedText = joke.Text.Replace("\t", "    ");
                var escapedCategory = joke.Category.Replace("\t", "    ");
                lines.Add($"{joke.Id}\t{escapedText}\t{escapedCategory}");
            }
            
            return string.Join("\n", lines);
        }
    }