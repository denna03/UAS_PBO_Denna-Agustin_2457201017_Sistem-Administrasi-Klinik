using ClinicAdminApp.Models;
using ClinicAdminApp.Reports.Base;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace ClinicAdminApp.Reports
{
    public class PembayaranReport : BaseReport
    {
        private readonly List<Pembayaran> _data;

        public PembayaranReport(List<Pembayaran> data)
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

                Header(page, "LAPORAN PEMBAYARAN");

                page.Content().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(40);
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                    });

                    table.Header(header =>
                    {
                        header.Cell().Text("No").Bold();
                        header.Cell().Text("No Bayar").Bold();
                        header.Cell().Text("Pasien").Bold();
                        header.Cell().Text("Metode").Bold();
                        header.Cell().Text("Total").Bold();
                    });

                    int no = 1;

                    foreach (var item in _data)
                    {
                        table.Cell().Text(no++.ToString());

                        table.Cell().Text(item.NoPembayaran);

                        table.Cell().Text(item.Resep?.Pemeriksaan?.Pendaftaran?.Pasien?.NamaPasien ?? "-");

                        table.Cell().Text(item.MetodePembayaran);

                        table.Cell().Text($"Rp {item.TotalBayar:N0}");
                    }
                });

                Footer(page);
            });
        }
    }
}