using ClinicAdminApp.Models;
using ClinicAdminApp.Reports.Base;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace ClinicAdminApp.Reports
{
    public class PasienReport : BaseReport
    {
        private readonly List<Pasien> _data;

        public PasienReport(List<Pasien> data)
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

                Header(page, "LAPORAN DATA PASIEN");

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
                        header.Cell().Text("Nama").Bold();
                        header.Cell().Text("JK").Bold();
                        header.Cell().Text("Telepon").Bold();
                    });

                    int no = 1;

                    foreach (var item in _data)
                    {
                        table.Cell().Text(no++.ToString());
                        table.Cell().Text(item.NamaPasien);
                        table.Cell().Text(item.JenisKelamin);
                        table.Cell().Text(item.NoTelepon);
                    }
                });

                Footer(page);
            });
        }
    }
}