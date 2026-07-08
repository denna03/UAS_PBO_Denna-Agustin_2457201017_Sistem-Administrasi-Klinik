using ClinicAdminApp.Models;
using ClinicAdminApp.Reports.Base;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace ClinicAdminApp.Reports
{
    public class ObatReport : BaseReport
    {
        private readonly List<Obat> _data;

        public ObatReport(List<Obat> data)
        {
            _data = data;
        }

        public override DocumentMetadata GetMetadata()
            => DocumentMetadata.Default;

        public override void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(25);

                Header(page, "LAPORAN DATA OBAT");

                page.Content().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(40);
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                    });

                    table.Header(header =>
                    {
                        header.Cell().Text("No").Bold();
                        header.Cell().Text("Nama Obat").Bold();
                        header.Cell().Text("Harga").Bold();
                        header.Cell().Text("Stok").Bold();
                    });

                    int no = 1;

                    foreach (var item in _data)
                    {
                        table.Cell().Text(no++.ToString());
                        table.Cell().Text(item.NamaObat);
                        table.Cell().Text($"Rp {item.Harga:N0}");
                        table.Cell().Text(item.Stok.ToString());
                    }
                });

                Footer(page);
            });
        }
    }
}