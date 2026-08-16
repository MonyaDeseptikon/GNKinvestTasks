using System;
using System.Collections.Generic;
using System.Text;
using iText.IO.Image;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;


namespace ParserWEB
{
    internal class PDFHandler
    {
        ConsoleHandler pdfOrient = new ConsoleHandler();
        internal void SavePDF(List<string> imageFiles, string fileName)
        {
            fileName = System.IO.Path.ChangeExtension(fileName, ".pdf");                
            var savePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);
            var pdfWriter = new PdfWriter(savePath);
            var pdfDoc = new PdfDocument(pdfWriter);
            string orient = pdfOrient.InputConsole("Введите 'альбом' если хотите выбрать альбомную ориентацию документа, " +
                "'книга' - для книжной ориентации");
            Document doc = orient.Equals("альбом") ? new Document(pdfDoc, PageSize.A4.Rotate()) : new Document(pdfDoc);
            
            try
            {
                foreach (string file in imageFiles)
                {
                    doc.Add(new Image(ImageDataFactory.Create(file)));
                    if (file != imageFiles.Last()) { doc.Add(new AreaBreak(AreaBreakType.NEXT_PAGE)); }
                }
            } finally
            {
                doc.Close();
            }

                   
        }       
    }
}
