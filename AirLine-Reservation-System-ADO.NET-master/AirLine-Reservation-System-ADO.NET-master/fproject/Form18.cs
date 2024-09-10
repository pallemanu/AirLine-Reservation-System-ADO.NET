using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using QRCoder;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace fproject
{
    public partial class Form18 : Form
    {
        public Form18()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Fetch and display data in DataGridView
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\SHAIK MOHAMMAD REHAN\source\repos\fproject\fproject\rehan.mdf"";Integrated Security=True;Connect Timeout=30;";
            string query = @"
                SELECT TOP 1 
                    u.Adharno, 
                    u.Name, 
                    u.Pno, 
                    u.Gmail, 
                    u.State, 
                    u.City, 
                    u.Area, 
                    u.Pincode, 
                    u.Flightno AS FID, 
                    u.Tickets,
                    f.Arlname AS FlightName, 
                    f.Ddate AS DepartureDate,
                    f.Dtime AS DepartureTime, 
                    f.Dcity AS DepartureCity, 
                    f.Dairport AS DepartureAirport,
                    f.Adate AS ArrivalDate,
                    f.Atime AS ArrivalTime, 
                    f.Acity AS ArrivalCity, 
                    f.Aairport AS ArrivalAirport,
                    f.Tprice AS Price,
                    (u.Tickets * f.Tprice) AS TotalCost,
                    u.BookingDate
                FROM 
                    DFlightuser u
                JOIN 
                    Dflights f 
                ON 
                    u.Flightno = f.FID
                ORDER BY 
                    u.BookingDate DESC";  // Fetch the most recent booking

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Check if a row is selected
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string adharno = selectedRow.Cells["Adharno"].Value.ToString();
                string name = selectedRow.Cells["Name"].Value.ToString();
                string email = selectedRow.Cells["Gmail"].Value.ToString();
                string pincode = selectedRow.Cells["Pincode"].Value.ToString();
                string airlineName = selectedRow.Cells["FlightName"].Value.ToString();
                string departureAirport = selectedRow.Cells["DepartureAirport"].Value.ToString();
                string departureTime = selectedRow.Cells["DepartureTime"].Value.ToString();
                string arrivalAirport = selectedRow.Cells["ArrivalAirport"].Value.ToString();
                string arrivalTime = selectedRow.Cells["ArrivalTime"].Value.ToString();
                string totalCost = selectedRow.Cells["TotalCost"].Value.ToString();

                // Generate PDF with ticket details and QR code
                GeneratePDF(adharno, name, email, pincode, airlineName, departureAirport, departureTime, arrivalAirport, arrivalTime, totalCost);
            }
            else
            {
                MessageBox.Show("Please select a row from the DataGridView.");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Your handling code, if necessary
        }

        private void GeneratePDF(string adharno, string name, string email, string pincode, string airlineName, string departureAirport, string departureTime, string arrivalAirport, string arrivalTime, string totalCost)
        {
            string ticketNumber = GenerateTicketNumber();
            string path = @"C:\Users\SHAIK MOHAMMAD REHAN\Downloads\Ticket_" + ticketNumber + ".pdf";

            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
                using (iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 30, 30, 30, 30))
                using (PdfWriter writer = PdfWriter.GetInstance(doc, fs))
                {
                    doc.Open();

                    // Title
                    iTextSharp.text.Font titleFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 24, iTextSharp.text.Font.BOLD, new BaseColor(0, 0, 255)); // Blue
                    iTextSharp.text.Paragraph title = new iTextSharp.text.Paragraph("FLY-HIGH RESERVATIONS", titleFont);
                    title.Alignment = Element.ALIGN_CENTER;
                    doc.Add(title);

                    doc.Add(new iTextSharp.text.Paragraph("\n")); // Line break

                    // Main Table
                    PdfPTable mainTable = new PdfPTable(2);
                    mainTable.WidthPercentage = 100;
                    float[] widths = new float[] { 2f, 1f };
                    mainTable.SetWidths(widths);

                    // Details Table
                    PdfPTable detailsTable = new PdfPTable(2);
                    detailsTable.WidthPercentage = 100;
                    detailsTable.SpacingBefore = 10f;
                    detailsTable.SpacingAfter = 10f;
                    detailsTable.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                    // Add styled details to the details table
                    AddTableCell(detailsTable, " DOMESTIC FLIGHT TICKET", 18, true, new BaseColor(105, 105, 105), 2); // Gray, spanning 2 columns
                    AddTableCell(detailsTable, "Name: ", name);
                    AddTableCell(detailsTable, "Aadhar: ", adharno);
                    AddTableCell(detailsTable, "Email: ", email);
                    AddTableCell(detailsTable, "Pincode: ", pincode);
                    AddTableCell(detailsTable, "Airline: ", airlineName);
                    AddTableCell(detailsTable, "Departure Airport: ", departureAirport);
                    AddTableCell(detailsTable, "Departure Time: ", departureTime);
                    AddTableCell(detailsTable, "Arrival Airport: ", arrivalAirport);
                    AddTableCell(detailsTable, "Arrival Time: ", arrivalTime);
                    AddTableCell(detailsTable, "Total Cost: ", totalCost);
                    AddTableCell(detailsTable, "Ticket Number: ", ticketNumber);

                    // Add the details table to the main table
                    PdfPCell detailsCell = new PdfPCell(detailsTable);
                    detailsCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    mainTable.AddCell(detailsCell);

                    // Generate QR Code
                    string qrContent = $"{name}-{adharno}-{email}-{pincode}-{airlineName}-{departureAirport}-{departureTime}-{arrivalAirport}-{arrivalTime}-{totalCost}-{ticketNumber}";
                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrContent, QRCodeGenerator.ECCLevel.Q);
                    QRCode qrCode = new QRCode(qrCodeData);
                    System.Drawing.Bitmap qrCodeImage = qrCode.GetGraphic(20);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        qrCodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        iTextSharp.text.Image qrCodeItextImage = iTextSharp.text.Image.GetInstance(ms.ToArray());
                        qrCodeItextImage.ScaleToFit(100f, 100f);
                        PdfPCell qrCell = new PdfPCell(qrCodeItextImage);
                        qrCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        qrCell.VerticalAlignment = Element.ALIGN_TOP;
                        qrCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        mainTable.AddCell(qrCell);
                    }

                    doc.Add(mainTable);
                    doc.Close();
                    writer.Close();
                    MessageBox.Show("Ticket PDF generated successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while generating the PDF: " + ex.Message);
            }
        }
        // Helper method to add styled cells to the table
        private void AddTableCell(PdfPTable table, string label, string value)
        {
            AddTableCell(table, label + value, 12, false, BaseColor.BLACK, 1);
        }

        private void AddTableCell(PdfPTable table, string text, int fontSize, bool isBold, BaseColor color, int colspan)
        {
            iTextSharp.text.Font font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, fontSize, isBold ? iTextSharp.text.Font.BOLD : iTextSharp.text.Font.NORMAL, color);
            PdfPCell cell = new PdfPCell(new iTextSharp.text.Phrase(text, font));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell.Colspan = colspan;
            table.AddCell(cell);
        }



        // Helper method to generate a random ticket number
        private string GenerateTicketNumber()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form17 form17 = new Form17();
            form17.ShowDialog();
            this.Close();
        }
    }
}
