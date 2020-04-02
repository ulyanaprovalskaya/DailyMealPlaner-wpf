using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;
using DailyMealPlaner.Business_Layer;

namespace DailyMealPlaner.Data_Layer
{
    class PDF
    {
        private static Db db = Db.GetInstance();

        private static PdfPTable table = new PdfPTable(2);  
        private static PdfPTable header = new PdfPTable(1);  
        private static PdfPTable user = new PdfPTable(1);  
        private static PdfPTable ration = new PdfPTable(2);  
        private static PdfPTable mealtime;

        public static void CreatePDF(string filepath)
        {
            var doc = new iTextSharp.text.Document();
            var writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(filepath, System.IO.FileMode.Create));
            doc.Open();

            var baseFont = BaseFont.CreateFont(@"C:\WINDOWS\Fonts\comic.ttf", "CP1251", BaseFont.EMBEDDED);
            var font = new Font(baseFont, 10, Font.NORMAL);
            var headerFont = new Font(baseFont, 30, Font.NORMAL, BaseColor.GREEN);
            var subheaderFont = new Font(baseFont, 15, Font.NORMAL, BaseColor.GREEN);


            WriteHeader(headerFont);
            table.AddCell(WriteImage(Directory.GetCurrentDirectory() + @"/Pictures/forpdf.jpg"));

            WriteUserInfo(subheaderFont, font);
            table.AddCell(WriteImage(Directory.GetCurrentDirectory() + @"/Pictures/Webp.net-resizeimage.jpg"));

            table.TotalWidth = doc.PageSize.Width;
            doc.Add(table);

            doc.Add(new Paragraph(DateTime.Today.ToShortDateString() + "\n", subheaderFont));

            foreach (Mealtime m in db.Mealtimes)
            {
                if(m.Products.Count != 0)
                {
                    doc.Add(new Paragraph(m.Name, subheaderFont));

                    foreach(Product p in m.Products)
                    {
                        doc.Add(new Paragraph(p.Name + ", " + p.Weight + " г", font));
                    }
                }
                WriteMealtime(m, font, subheaderFont);
            }

            doc.Add(new Paragraph("\nИтого ккал: " + db.DailyRation.TotalCalories, subheaderFont));

            doc.Close();

            //open pdf
            //System.Diagnostics.Process.Start("Рацион.pdf");
        }

        private static void WriteHeader(Font headerFont)
        {
            PdfPCell cell = new PdfPCell();
            cell.Phrase = new Phrase("Daily", headerFont);
            cell.Border = 0;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            header.AddCell(cell);

            cell = new PdfPCell();
            cell.Phrase = new Phrase("Meal", headerFont);
            cell.Border = 0;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            header.AddCell(cell);

            cell = new PdfPCell();
            cell.Phrase = new Phrase("Planer", headerFont);
            cell.Border = 0;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            header.AddCell(cell);

            PdfPCell headerCell = new PdfPCell(header);
            headerCell.Border = 0;
            table.AddCell(headerCell);
        }

        private static PdfPCell WriteImage(string filepath)
        {
            PdfPCell cell = new PdfPCell();
            cell.Image = Image.GetInstance(filepath);
            cell.Border = 0;
            return cell;
        }

        private static PdfPCell WriteMealtimeImage(string filepath)
        {
            PdfPCell cell = new PdfPCell();
            cell.Image = Image.GetInstance(filepath);
            cell.Border = 0;
            return cell;
        }

        private static void WriteUserInfo(Font subheaderFont, Font font)
        {
            PdfPCell cell = new PdfPCell();
            cell.Phrase = new Phrase("\nПользователь:", subheaderFont);
            cell.Border = 0;
            user.AddCell(cell);

            cell = new PdfPCell();
            cell.Phrase = new Phrase("Вес: " + db.User.Weight, font);
            cell.Border = 0;
            user.AddCell(cell);

            cell = new PdfPCell();
            cell.Phrase = new Phrase("Рост: " + db.User.Height, font);
            cell.Border = 0;
            user.AddCell(cell);

            cell = new PdfPCell();
            cell.Phrase = new Phrase("Возраст: " + db.User.Age, font);
            cell.Border = 0;
            user.AddCell(cell);

            cell = new PdfPCell();
            cell.Phrase = new Phrase("Ежедневная норма калорий: " + db.User.DailyCaloriesRate, font);
            cell.Border = 0;
            user.AddCell(cell);

            PdfPCell userCell = new PdfPCell(user);
            userCell.Border = 0;
            table.AddCell(userCell);
        }

        private static void WriteMealtime(Mealtime m, Font font, Font subheaderFont)
        {
            if (m.Products.Count != 0)
            {
                PdfPCell cell = new PdfPCell();
                cell.Border = 0;

                if (m.Name == "Завтрак") cell = WriteMealtimeImage(Directory.GetCurrentDirectory() + @"/Pictures/Завтрак.png");
                else if (m.Name == "Обед") cell = WriteMealtimeImage(Directory.GetCurrentDirectory() + @"/Pictures/Обед.png");
                else if (m.Name == "Ужин") cell = WriteMealtimeImage(Directory.GetCurrentDirectory() + @"/Pictures/Ужин.png");
                else cell = WriteMealtimeImage(Directory.GetCurrentDirectory() + @"/Pictures/mealtime.png");

                ration.AddCell(cell);

                mealtime = new PdfPTable(1);

                cell = new PdfPCell();
                cell.Phrase = new Phrase(m.Name, subheaderFont);
                cell.Border = 0;
                mealtime.AddCell(cell);

                foreach (Product p in m.Products)
                {
                    cell = new PdfPCell();
                    cell.Phrase = new Phrase(p.Name + ", " + p.Weight + "г\n", font);
                    cell.Border = 0;
                    mealtime.AddCell(cell);
                }

                PdfPCell mealtimeCell = new PdfPCell(mealtime);
                mealtimeCell.Border = 0;
                ration.AddCell(mealtimeCell);
            }
        }
    }
}
