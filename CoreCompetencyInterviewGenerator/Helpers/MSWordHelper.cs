using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using InterviewGeneratorBlazorHybrid.Data;
using Microsoft.EntityFrameworkCore;

namespace InterviewGeneratorBlazorHybrid.Helpers
{
    public class MSWordHelper
    {
        private readonly AppDbContextFactory _contextFactory;
        private string _templatePath;
        public MSWordHelper(AppDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _templatePath = Preferences.Get("TemplateDocumentPath", string.Empty);
        }
       
        public MemoryStream GenerateInterviewDoc(int interviewId)
        {
            using var context = _contextFactory.CreateDbContext();
            var interview = context.Interviews
                .Include(i => i.Questions)
                .ThenInclude(q => q.Category)
                .FirstOrDefault(i => i.Id == interviewId);

            string templatePath = _templatePath;
            string outputPath = @"c:\temp\Output Interview.docx"; ;

            // Read the template into a byte[] and initialize a MemoryStream from it.
            // This keeps everything in-memory (no temp file).
            var templateFileBytes = File.ReadAllBytes(templatePath);

            var inMemoryFileBytes = new MemoryStream();
            inMemoryFileBytes.Write(templateFileBytes, 0, templateFileBytes.Length);
            inMemoryFileBytes.Position = 0;

            //var inMemoryFileBytes = new MemoryStream(templateFileBytes);

            using (var doc = WordprocessingDocument.Open(inMemoryFileBytes, true))
            {

                var body = doc.MainDocumentPart!.Document.Body!;
                foreach (var text in body.Descendants<Text>())
                {
                    if (text.Text.Contains("{**InterviewGuideName**}"))
                        text.Text = text.Text.Replace("{**InterviewGuideName**}", interview.InterviewName);
                }

                // Add to the table that is there
                Table table = body.Elements<Table>().FirstOrDefault();
                foreach (var question in interview.Questions)
                {
                    if (table != null)
                    {
                        TableRow newRow = new TableRow(
                            new TableCell(new Paragraph(new Run(new Text($"{question.Text} ({question.Category.Name})")))),
                            new TableCell(new Paragraph(new Run(new Text($""))))

                        );
                        table.AppendChild(newRow);
                    }
                }

                // Reset position so caller can read/serve the stream from the start
                inMemoryFileBytes.Position = 0;
                return inMemoryFileBytes;
            }
        }
    }
}
