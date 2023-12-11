# ReadMe

Sample code of a Program.cs file with all methods called

# Program.cs

    using PDFMonkeyCaller;
    using Entities;
    using System.Text;
    
    internal class Program
    {
        private static string apiSecret = "XxxXXXxxXxXxXxXXx-XxxXXx";
        private static string pdfDestinationPath = @"c:\temp\";
        private static Guid documentId = Guid.Parse("XxxxXXXxxXX-XxXx-XXxXx-XXXxX-XXxxXXXXXXXxx");
        private static Guid documentTemplateId = Guid.Parse("XxxxXXXxxXX-XxXx-XXxXx-XXXxX-XXxxXXXXXXXxx");


        static async Task Main()
        {
            // Create new PDFMonkeyCaller.
            var caller = new PDFMonkeyCaller(apiSecret);

            // Retrieve user info.
            await RetrieveUser(caller);

            // Retrieve Document card.
            await RetrieveDocumentCard(caller, documentId);

            // Retrieve a document from is ID.
            var doc = await caller.GetDocument(documentId);
            
            // Create a simple Document and download the PDF file.
            await CreateDocumentAndDownloadPDFfile(caller, documentTemplateId, pdfDestinationPath);
            
           
        }

        private static async Task CreateDocumentAndDownloadPDFfile(PDFMonkeyCaller caller, Guid documentTemplateId, string downloadPath)
        {
            // Create Document based on Invoice sample Template
            // Create payload this some data.
            var payload = @"
                {            
                    ""billing_company_name"": ""Jane Doe""
                }";

            // Create metadata to set a specific name to the generated PDF.
            var meta = @"
                    {
                        ""_filename"": ""my - document.pdf""
                    }";

            var jsonPayload = new StringContent(payload, Encoding.UTF8, "application/json");
            var jsonMeta = new StringContent(meta, Encoding.UTF8, "application/json");

            // Create a new Document with Template ID and specifying Status
            //      to pending to ask PDFMonkey to generate the PDF file as soon as possible.
            var document = new Document
            {
                DocumentTemplateId = documentTemplateId,
                Payload = payload,
                Meta = meta,
                Status = "pending"
            };

            // Create Document.
            var createdDoc = await caller.CreateDocument(document);


            // Simple synchronous call to retrieve the generated PDF file path.
            DocumentCard card;

            while (true)
            {
                card = await caller.GetDocumentCard(createdDoc.Id);

                if (card.Status is "success" or "failure")
                    break;

                Thread.Sleep(1000);
            }

            // Download the PDF file to a specific path.
            await caller.DownloadPdfFile(card, downloadPath);
        }

        private static async Task RetrieveUser(PDFMonkeyCaller caller)
        {
            // Retrieve user info.
            var user = await caller.GetUser();
        }

        private static async Task RetrieveDocumentCard(PDFMonkeyCaller caller, Guid documentId)
        {
            // Retrieve Document card.
            var docCard = await caller.GetDocumentCard(documentId);
        }
    }
