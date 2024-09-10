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
    public partial class Form20 : Form
    {
        public Form20()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Fetch and display data in DataGridView
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\SHAIK MOHAMMAD REHAN\source\repos\fproject\fproject\rehan.mdf"";Integrated Security=True;Connect Timeout=30;";
            string query = @"
    SELECT TOP 1 
        u.PassportNo, 
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
        CONCAT(CONVERT(VARCHAR, f.Ddate, 23), ' ', CONVERT(VARCHAR, f.Dtime, 8)) AS DepartureTime, 
        CONCAT(CONVERT(VARCHAR, f.Adate, 23), ' ', CONVERT(VARCHAR, f.Atime, 8)) AS ArrivalTime, 
        f.Tprice AS Price,
        f.Dcountry AS DepCountry, 
        f.Ddate AS DepDate, 
        f.Dairport AS DepAirport,
        f.Acountry AS ArrCountry, 
        f.Adate AS ArrDate, 
        f.Aairport AS ArrAirport,
        (u.Tickets * f.Tprice) AS TotalCost,
        u.BookingDate
    FROM 
        IFlightuser u
    JOIN 
        IFlights f 
    ON 
        u.Flightno = f.FID
    ORDER BY 
        u.BookingDate DESC";  // Fetch the most recent booking

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while fetching data: " + ex.Message);
                }
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            // Check if a row is selected
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                    string passportNo = selectedRow.Cells["PassportNo"].Value.ToString();
                    string name = selectedRow.Cells["Name"].Value.ToString();
                    string email = selectedRow.Cells["Gmail"].Value.ToString();
                    string pincode = selectedRow.Cells["Pincode"].Value.ToString();
                    string airlineName = selectedRow.Cells["FlightName"].Value.ToString();
                    string departureTime = selectedRow.Cells["DepartureTime"].Value.ToString();
                    string arrivalTime = selectedRow.Cells["ArrivalTime"].Value.ToString();
                    string totalCost = selectedRow.Cells["TotalCost"].Value.ToString();
                    string depCountry = selectedRow.Cells["DepCountry"].Value.ToString();
                    string depDate = selectedRow.Cells["DepDate"].Value.ToString();
                    string depAirport = selectedRow.Cells["DepAirport"].Value.ToString();
                    string arrCountry = selectedRow.Cells["ArrCountry"].Value.ToString();
                    string arrDate = selectedRow.Cells["ArrDate"].Value.ToString();
                    string arrAirport = selectedRow.Cells["ArrAirport"].Value.ToString();

                    // Generate PDF with ticket details and QR code
                    GeneratePDF(passportNo, name, email, pincode, airlineName, departureTime, arrivalTime, totalCost, depCountry, depDate, depAirport, arrCountry, arrDate, arrAirport);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while processing the selected row: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select a row from the DataGridView.");
            }
        }

        private void GeneratePDF(string passportNo, string name, string email, string pincode, string airlineName, string departureTime, string arrivalTime, string totalCost, string depCountry, string depDate, string depAirport, string arrCountry, string arrDate, string arrAirport)
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

                    // Create a title
                    iTextSharp.text.Paragraph title = new iTextSharp.text.Paragraph("FLY-HIGH RESERVATIONS", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 24, iTextSharp.text.Font.BOLD, new BaseColor(0, 0, 255))); // Blue
                    title.Alignment = Element.ALIGN_CENTER;
                    doc.Add(title);

                    // Add a line break
                    doc.Add(new iTextSharp.text.Paragraph("\n"));

                    // Create a table with two columns for the details and QR code
                    PdfPTable table = new PdfPTable(2);
                    table.WidthPercentage = 100;
                    float[] widths = new float[] { 2f, 1f };
                    table.SetWidths(widths);

                    // Create a nested table for the ticket details
                    PdfPTable detailsTable = new PdfPTable(1);
                    detailsTable.WidthPercentage = 100;
                    detailsTable.SpacingBefore = 10f;
                    detailsTable.SpacingAfter = 10f;
                    detailsTable.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                    // Add styled details to the table
                    AddTableCell(detailsTable, "INTERNATIONAL FLIGHT TICKET", 18, true, new BaseColor(105, 105, 105)); // Gray
                    AddTableCell(detailsTable, "Name: ", name);
                    AddTableCell(detailsTable, "Passport No: ", passportNo);
                    AddTableCell(detailsTable, "Email: ", email);
                    AddTableCell(detailsTable, "Pincode: ", pincode);
                    AddTableCell(detailsTable, "Airline: ", airlineName);
                    AddTableCell(detailsTable, "Departure Country: ", depCountry);
                    AddTableCell(detailsTable, "Departure Date: ", depDate);
                    AddTableCell(detailsTable, "Departure Airport: ", depAirport);
                    AddTableCell(detailsTable, "Arrival Country: ", arrCountry);
                    AddTableCell(detailsTable, "Arrival Date: ", arrDate);
                    AddTableCell(detailsTable, "Arrival Airport: ", arrAirport);
                    AddTableCell(detailsTable, "Total Cost: ", totalCost);
                    AddTableCell(detailsTable, "Ticket Number: ", ticketNumber);

                    // Add the details table to the main table
                    PdfPCell detailsCell = new PdfPCell(detailsTable);
                    detailsCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    table.AddCell(detailsCell);

                    // Generate QR Code
                    string qrContent = $"{name}-{passportNo}-{email}-{pincode}-{airlineName}-{departureTime}-{arrivalTime}-{totalCost}-{depCountry}-{depDate}-{depAirport}-{arrCountry}-{arrDate}-{arrAirport}-{ticketNumber}";
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
                        table.AddCell(qrCell);
                    }

                    doc.Add(table);
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

        // Helper method to add cells to the table
        private void AddTableCell(PdfPTable table, string label, string value)
        {
            PdfPCell cell = new PdfPCell(new iTextSharp.text.Phrase(label + value, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12)));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            table.AddCell(cell);
        }

        private void AddTableCell(PdfPTable table, string text, int fontSize, bool isBold, BaseColor color)
        {
            iTextSharp.text.Font font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, fontSize, isBold ? iTextSharp.text.Font.BOLD : iTextSharp.text.Font.NORMAL, color);
            PdfPCell cell = new PdfPCell(new iTextSharp.text.Phrase(text, font));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            table.AddCell(cell);
        }

        private string GenerateTicketNumber()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss");
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void button3_Click(object sender, EventArgs e) 
        {
            Form19 form19 = new Form19();   
            form19.ShowDialog();
            this.Close();
        }

    }
}