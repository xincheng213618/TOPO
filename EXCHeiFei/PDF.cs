using iTextSharp.text.pdf;

namespace EXC
{
    public static class PDF
    {
        public static int PDFNum(string FileName)
        {
            int Num = 0;
            try
            {
                PdfReader reader = new PdfReader(FileName);
                Num = reader.NumberOfPages;
                reader.Close();
                reader.Dispose();
            }
            catch
            {
                Num = 0;
            }

            return Num;
        }
    }
}
