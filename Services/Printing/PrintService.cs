using System.Windows;
using System.Windows.Documents;
using System.Windows.Controls;
using System.Windows.Media;
using Sales_Manager.ViewModels.Model;
using Sales_Manager.Models;

namespace Sales_Manager.Services.Printing;

public class PrintService
{
    private readonly string _title;
    private readonly Order _order;
    private readonly IEnumerable<Item> _orderItems;

    public PrintService(IEnumerable<Item> orderItems, Order order, string title = "Order summary")
    {
        _orderItems = orderItems;
        _order = order;
        _title = title;
    }

    public void Print()
    {
        //FlowDocument flowDocument = GenerateFlowDocument();

        //Window window = new Window();
        //FlowDocumentReader documentReader = new FlowDocumentReader
        //{
        //    Document = flowDocument,
        //    Margin = new Thickness(10)
        //};

        //window.Content = documentReader;
        //window.Show();
        PrintDialog printDialog = new();
        if (printDialog.ShowDialog() == true)
        {
            FlowDocument flowDocument = GenerateFlowDocument();

            IDocumentPaginatorSource idpSource = flowDocument;
            printDialog.PrintDocument(idpSource.DocumentPaginator, "Order Print");
        }
    }

    private FlowDocument GenerateFlowDocument()
    {
        FlowDocument flowDocument = new()
        {
            PageWidth = 800,
            PageHeight = 1123,
            FontFamily = new FontFamily("/UI/Fonts/#cabin"),
            FontSize = 12,
            ColumnWidth = 800
        };

        Paragraph titleParagraph = new(new Run(_title))
        {
            FontSize = 24,
            FontWeight = FontWeights.Bold,
            TextAlignment = TextAlignment.Center,
            Margin = new Thickness(0, 0, 0, 20)
        };
        flowDocument.Blocks.Add(titleParagraph);

        Paragraph client= new ()
        {
            FontSize = 16,
            FontWeight = FontWeights.Regular,
            TextAlignment = TextAlignment.Left,
            Margin = new Thickness(0, 0, 0, 20)
        };
        Run clientName = new($"Client: Mr/s {_order.Customer.Name}");
        Run clientPhone = new($"Phone number: {_order.Customer.Phone}");
        Run date = new($"Ordered at: {_order.CreatedAt}");

        client.Inlines.Add(clientName);
        client.Inlines.Add(new LineBreak());
        client.Inlines.Add(clientPhone);
        client.Inlines.Add(new LineBreak());
        client.Inlines.Add(date);

        flowDocument.Blocks.Add(client);

        Table table = CreateOrderTable();
        flowDocument.Blocks.Add(table);

        Paragraph totalParagraph = new(new Run($"Total Amount: {_order.Total:C}"))
        {
            FontSize = 16,
            FontWeight = FontWeights.Bold,
            TextAlignment = TextAlignment.Right,
            Margin = new Thickness(0, 20, 0, 0)
        };
        flowDocument.Blocks.Add(totalParagraph);

        Paragraph netTotalParagraph = new(new Run($"Total Amount: {_order.NetTotal:C}"))
        {
            FontSize = 16,
            FontWeight = FontWeights.Bold,
            TextAlignment = TextAlignment.Right,
            Margin = new Thickness(0, 20, 0, 0)
        };
        flowDocument.Blocks.Add(netTotalParagraph);

        return flowDocument;
    }

    private Table CreateOrderTable()
    {
        Table table = new Table
        {
            CellSpacing = 0,
            BorderThickness = new Thickness(1),
            BorderBrush = Brushes.Black
        };

        table.Columns.Add(new TableColumn { Width = new GridLength(50) });  // ID
        table.Columns.Add(new TableColumn { Width = new GridLength(150) }); // Product
        table.Columns.Add(new TableColumn { Width = new GridLength(100) }); // Quantity
        table.Columns.Add(new TableColumn { Width = new GridLength(100) }); // Discount
        table.Columns.Add(new TableColumn { Width = new GridLength(100) }); // Unit Price
        table.Columns.Add(new TableColumn { Width = new GridLength(100) }); // Total
        table.Columns.Add(new TableColumn { Width = new GridLength(150) }); // Net Amount

        TableRowGroup headerGroup = new();
        TableRow headerRow = new() { Background = Brushes.LightGray };

        headerRow.Cells.Add(CreateCell("ID", true));
        headerRow.Cells.Add(CreateCell("Product", true));
        headerRow.Cells.Add(CreateCell("Quantity", true));
        headerRow.Cells.Add(CreateCell("Discount %", true));
        headerRow.Cells.Add(CreateCell("Unit Price", true));
        headerRow.Cells.Add(CreateCell("Amount", true));
        headerRow.Cells.Add(CreateCell("Net Amount", true));
        headerGroup.Rows.Add(headerRow);
        table.RowGroups.Add(headerGroup);

        TableRowGroup dataGroup = new();
        foreach (var item in _orderItems)
        {
            TableRow dataRow = new();
            dataRow.Cells.Add(CreateCell(item.Id.ToString()));
            dataRow.Cells.Add(CreateCell(item.Product.Name));
            dataRow.Cells.Add(CreateCell(item.Quantity.ToString()));
            dataRow.Cells.Add(CreateCell($"{item.discount}%"));
            dataRow.Cells.Add(CreateCell($"{item.Product.Price:C}"));
            dataRow.Cells.Add(CreateCell($"{item.Total:C}"));
            dataRow.Cells.Add(CreateCell($"{item.NetTotal:C}"));
            dataGroup.Rows.Add(dataRow);
        }
        table.RowGroups.Add(dataGroup);

        return table;
    }

    private TableCell CreateCell(string text, bool isHeader = false)
    {
        return 
            new TableCell(
                new Paragraph(new Run(text))
                {
                    Margin = new Thickness(2),
                    TextAlignment = TextAlignment.Center
                })
            {
                BorderThickness = new Thickness(0.5),
                BorderBrush = Brushes.Black,
                FontWeight = isHeader ? FontWeights.Bold : FontWeights.Normal
            };
    }
}
