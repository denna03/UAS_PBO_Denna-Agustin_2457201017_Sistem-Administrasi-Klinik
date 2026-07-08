using ClinicAdminApp.Models;
using ClinicAdminApp.Reports.Base;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace ClinicAdminApp.Reports
{
    public class DokterReport : BaseReport
    {
        private readonly List<Dokter> _dokters;

        public DokterReport(List<Dokter> dokters)
        {
            _dokters = dokters;
        }

        public override DocumentMetadata GetMetadata()
            => DocumentMetadata.Default;

        public override void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(25);

                Header(page, "LAPORAN DATA DOKTER");

                page.Content()
                    .Table(table =>
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

                            header.Cell().Text("Spesialis").Bold();

                            header.Cell().Text("Telepon").Bold();
                        });

                        int no = 1;

                        foreach (var item in _dokters)
                        {
                            table.Cell().Text(no.ToString());

                            table.Cell().Text(item.NamaDokter);

                            table.Cell().Text(item.Spesialis);

                            table.Cell().Text(item.NoTelepon);

                            no++;
                        }
                    });

                Footer(page);
            });
        }
    }
}